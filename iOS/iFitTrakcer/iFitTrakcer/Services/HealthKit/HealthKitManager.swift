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

    func loadSample<T>(for metric: UserMetric) async -> T {
        await withCheckedContinuation { continuation in
            loadSample(metric: metric) { (result: T) in
                continuation.resume(returning: result)
            }
        }
    }

    private func loadSample<T>(metric: UserMetric,
                               completion: @escaping (T) -> Void) {
        let startDate = Calendar.current.date(byAdding: .day, value: -7, to: Date())
        let predicate = HKQuery.predicateForSamples(withStart: startDate, end: Date(), options: .strictStartDate)

        let query = HKSampleQuery(sampleType: metric.sampleType,
                                  predicate: predicate,
                                  limit: Int(HKObjectQueryNoLimit),
                                  sortDescriptors: nil) { [weak self] (_, results, _) in
            guard let self else {
                return
            }

            guard let samples = results as? [HKQuantitySample] else {
                self.logger.error("Error in retrieving \(metric.sampleType)")
                return
            }

            let resourceSum = samples.reduce(0, {$0 + $1.quantity.doubleValue(for: metric.unit)})
            let weeklyAverageResource = resourceSum / Double(samples.count)

            guard let castResource = weeklyAverageResource as? T else {
                self.logger.error("Error in casting \(weeklyAverageResource.self) to \(T.self)")
                return
            }

            self.logger.info("Resource of type \(metric.sampleType) loaded successfully")
            completion(castResource)
        }

        healthStore.execute(query)
    }

    func requestAuthorization() {
        let typesToRead: Set<HKObjectType> = [
            HKObjectType.characteristicType(forIdentifier: .dateOfBirth)!,
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
            HKObjectType.categoryType(forIdentifier: .sleepAnalysis)!
        ]

        healthStore.requestAuthorization(toShare: nil, read: typesToRead) { _, _ in
            return
        }
    }
}
