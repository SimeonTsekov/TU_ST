//
//  HealthKitManager.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import Foundation
import HealthKit
import OSLog

enum HealthKitError: Error {
    case queryError(String?)
    case castError
}

class HealthKitManager: ObservableObject {
    let healthStore = HKHealthStore()
    let logger = Logger()

    var authorizationTypes: Set<HKObjectType> {
        [HKObjectType.characteristicType(forIdentifier: .dateOfBirth)!,
            HKObjectType.characteristicType(forIdentifier: .biologicalSex)!,
            HKObjectType.quantityType(forIdentifier: .height)!,
            HKObjectType.quantityType(forIdentifier: .bodyMass)!,
            HKObjectType.quantityType(forIdentifier: .bodyMassIndex)!,
            HKObjectType.quantityType(forIdentifier: .bodyFatPercentage)!,
            HKObjectType.quantityType(forIdentifier: .leanBodyMass)!,
            HKObjectType.quantityType(forIdentifier: .stepCount)!,
            HKObjectType.quantityType(forIdentifier: .distanceWalkingRunning)!,
            HKObjectType.quantityType(forIdentifier: .activeEnergyBurned)!,
            HKObjectType.categoryType(forIdentifier: .appleStandHour)!,
            HKObjectType.categoryType(forIdentifier: .sleepAnalysis)!]
    }

    var weeklyPredicate: NSPredicate {
        let startDate = Calendar.current.date(byAdding: .day, value: -7, to: Date())
        return HKQuery.predicateForSamples(withStart: startDate, end: Date(), options: .strictStartDate)
    }

    func requestAuthorization() {
        healthStore.requestAuthorization(toShare: nil, read: authorizationTypes) { _, _ in
            return
        }
    }

    func loadSamples<T>(for metric: UserMetric) async -> T {
        await withCheckedContinuation { continuation in
            loadSamples(metric: metric) { (result: T) in
                continuation.resume(returning: result)
            }
        }
    }

    private func loadSamples<T>(metric: UserMetric,
                                completion: @escaping (T) -> Void) {
        let query = HKSampleQuery(sampleType: metric.sampleType,
                                  predicate: weeklyPredicate,
                                  limit: Int(HKObjectQueryNoLimit),
                                  sortDescriptors: nil) { [weak self] (_, results, _) in
            guard let self else {
                return
            }

            guard let samples = results as? [HKQuantitySample] else {
                self.logger.error("[HEALTH KIT LOADING] Error in retrieving \(metric.sampleType)")
                return
            }

            let sampleValues = samples.map { sample in
                sample.quantity.doubleValue(for: metric.unit)
            }

            guard let castSampleValues = sampleValues as? T else {
                self.logger.error("[HEALTH KIT LOADING] Error in casting \(sampleValues.self) to \(T.self)")
                return
            }

            self.logger.info("[HEALTH KIT LOADING] Resource of type \(metric.sampleType) loaded successfully")
            completion(castSampleValues)
        }

        healthStore.execute(query)
    }
}
