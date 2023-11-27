//
//  HealthKitManager.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import Foundation
import HealthKit

// Define errors needed
enum HealthKitError: Error {
    case queryError(String?)
    case castError
}

class HealthKitManager: ObservableObject {
    var healthStore = HKHealthStore()

    func loadSample<T>(sampleType: HKQuantityType,
                       unit: HKUnit,
                       completion: @escaping (Result<T, HealthKitError>) -> Void) {
        let startDate = Calendar.current.date(byAdding: .day, value: -7, to: Date())
        let predicate = HKQuery.predicateForSamples(withStart: startDate, end: Date(), options: .strictStartDate)

        let query = HKSampleQuery(sampleType: sampleType,
                                  predicate: predicate,
                                  limit: Int(HKObjectQueryNoLimit),
                                  sortDescriptors: nil) { (query, results, error) in
            guard let samples = results as? [HKQuantitySample] else {
                completion(.failure(.queryError(error?.localizedDescription)))
                return
            }

            let resourceSum = samples.reduce(0, {$0 + $1.quantity.doubleValue(for: unit)})
            let weeklyAverageResource = resourceSum / Double(samples.count)

            guard let castResource = weeklyAverageResource as? T else {
                completion(.failure(.castError))
                return
            }

            completion(.success(castResource))
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

    // Define structs to hold the fetched data
struct UserData {
    var dateOfBirth: Date?
    var biologicalSex: HKBiologicalSexObject?
    var height: HKQuantitySample?
    var weight: HKQuantitySample?
}

struct HealthData {
    var bodyMassIndex: HKQuantitySample?
    var bodyFatPercentage: HKQuantitySample?
    var leanBodyMass: HKQuantitySample?
//    var sleepAnalysis: [HKCategorySample]?
}

struct ActivityData {
    var stepCount: HKQuantitySample?
    var distance: HKQuantitySample?
    var activeEnergyBurned: HKQuantitySample?
    var workouts: [HKWorkout]?
}
