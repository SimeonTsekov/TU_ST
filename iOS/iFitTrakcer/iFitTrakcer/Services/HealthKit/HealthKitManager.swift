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

enum TimePredicate {
    case weekly
    case alltime

    var predicate: NSPredicate? {
        switch self {
        case .weekly:
            let startDate = Calendar.current.date(byAdding: .day, value: -7, to: Date())
            return HKQuery.predicateForSamples(withStart: startDate, end: Date(), options: .strictStartDate)
        case .alltime:
            return nil
        }
    }
}

class HealthKitManager: ObservableObject {
    private let healthStore = HKHealthStore()
    private let logger = Logger()

    var authorizationTypes: Set<HKObjectType> {
        [.characteristicType(forIdentifier: .dateOfBirth)!,
            .characteristicType(forIdentifier: .biologicalSex)!,
            .quantityType(forIdentifier: .height)!,
            .quantityType(forIdentifier: .bodyMass)!,
            .quantityType(forIdentifier: .bodyMassIndex)!,
            .quantityType(forIdentifier: .bodyFatPercentage)!,
            .quantityType(forIdentifier: .leanBodyMass)!,
            .quantityType(forIdentifier: .stepCount)!,
            .quantityType(forIdentifier: .distanceWalkingRunning)!,
            .quantityType(forIdentifier: .activeEnergyBurned)!,
            .categoryType(forIdentifier: .appleStandHour)!,
            .categoryType(forIdentifier: .sleepAnalysis)!,
            .workoutType()]
    }

    func requestAuthorization() async {
        await withCheckedContinuation { continuation in
            requestAuthorization {
                continuation.resume()
            }
        }
    }

    func loadSamples<T>(for metric: UserMetric,
                        timePredicate: TimePredicate = .weekly) async -> T {
        await withCheckedContinuation { continuation in
            loadSamples(metric: metric,
                        timePredicate: timePredicate) { (result: T) in
                continuation.resume(returning: result)
            }
        }
    }

    func loadWorkouts() async -> [HKWorkout] {
        await withCheckedContinuation { continuation in
            loadWorkouts { result in
                continuation.resume(returning: result)
            }
        }
    }

    func loadBiologicalSex() async -> Sex {
        await withCheckedContinuation { continuation in
            loadBiologicalSex { result in
                continuation.resume(returning: result)
            }
        }
    }

    func loadDateOfBirth() async -> Date {
        await withCheckedContinuation { continuation in
            loadDateOfBirth { result in
                continuation.resume(returning: result)
            }
        }
    }

    private func requestAuthorization(completion: @escaping () -> Void) {
        healthStore.requestAuthorization(toShare: nil, read: authorizationTypes) { _, _ in
            completion()
        }
    }

    private func loadSamples<T>(metric: UserMetric,
                                timePredicate: TimePredicate,
                                completion: @escaping (T) -> Void) {
        let query = HKSampleQuery(sampleType: metric.sampleType,
                                  predicate: timePredicate.predicate,
                                  limit: Int(HKObjectQueryNoLimit),
                                  sortDescriptors: nil) { [weak self] (_, results, _) in
            guard let self else {
                return
            }

            guard let samples = results as? [HKQuantitySample] else {
                self.logger
                    .error("[HEALTH KIT LOADING] Error in retrieving \(metric.sampleType)")
                return
            }

            let sampleValues = samples.map { sample in
                sample.quantity.doubleValue(for: metric.unit)
            }

            guard let castSampleValues = sampleValues as? T else {
                self.logger
                    .error("[HEALTH KIT LOADING] Error in casting \(metric.sampleType) to \(T.self)")
                return
            }

            self.logger
                .info("[HEALTH KIT LOADING] Resource of type \(metric.sampleType) loaded successfully")
            completion(castSampleValues)
        }

        healthStore.execute(query)
    }

    private func loadWorkouts(completion: @escaping ([HKWorkout]) -> Void) {
        let query = HKSampleQuery(sampleType: .workoutType(),
                                  predicate: TimePredicate.weekly.predicate,
                                  limit: HKObjectQueryNoLimit,
                                  sortDescriptors: nil) { (_, samples, _) in
            guard let workouts = samples as? [HKWorkout] else {
                self.logger
                    .error("[HEALTH KIT LOADING] Error in retrieving workouts")
                return
            }

            self.logger
                .info("[HEALTH KIT LOADING] Workouts loaded successfully")
            completion(workouts)
        }

        healthStore.execute(query)
    }

    private func loadBiologicalSex(completion: @escaping (Sex) -> Void) {
        do {
            let biologicalSexObject = try healthStore.biologicalSex()

            self.logger
                .info("[HEALTH KIT LOADING] Biological sex loaded successfully")

            switch biologicalSexObject.biologicalSex {
            case .female:
                completion(.female)
            case .male:
                completion(.male)
            case .other,
                    .notSet:
                completion(.unidentified)
            @unknown default:
                completion(.unidentified)
            }
        } catch let error {
            self.logger
                .error("[HEALTH KIT LOADING] Error in retrieving biological sex \(error.localizedDescription)")
        }
    }

    private func loadDateOfBirth(completion: @escaping (Date) -> Void) {
        do {
            let dateOfBirthCOmponents = try healthStore.dateOfBirthComponents()
            guard let dateOfBirth = Calendar.current.date(from: dateOfBirthCOmponents) else {
                self.logger
                    .error("[HEALTH KIT LOADING] Error when loading date of birth")
                return
            }

            self.logger
                .info("[HEALTH KIT LOADING] Date of birth loaded successfully")

            completion(dateOfBirth)

        } catch let error {
            self.logger
                .error("[HEALTH KIT LOADING] Error in retrieving date of birth \(error.localizedDescription)")
        }
    }
}
