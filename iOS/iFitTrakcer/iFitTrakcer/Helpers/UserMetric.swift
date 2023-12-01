//
//  UserMetric.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import HealthKit
import Foundation

enum UserMetric {
    case bodyMass
    case bmi
    case bodyFat
    case leanMass
    case workouts
    case steps
    case distance
    case energyExpenditure

    var sampleType: HKSampleType {
        switch self {
        case .bodyMass:
            HKQuantityType.quantityType(forIdentifier: .bodyMass)!
        case .bmi:
            HKQuantityType.quantityType(forIdentifier: .bodyMassIndex)!
        case .bodyFat:
            HKQuantityType.quantityType(forIdentifier: .bodyFatPercentage)!
        case .leanMass:
            HKQuantityType.quantityType(forIdentifier: .leanBodyMass)!
        case .workouts:
            HKObjectType.workoutType()
        case .steps:
            HKQuantityType.quantityType(forIdentifier: .stepCount)!
        case .distance:
            HKQuantityType.quantityType(forIdentifier: .distanceWalkingRunning)!
        case .energyExpenditure:
            HKQuantityType.quantityType(forIdentifier: .activeEnergyBurned)!
        }
    }

    var unit: HKUnit? {
        switch self {
        case .bodyMass:
            HKUnit.gramUnit(with: .kilo)
        case .bmi:
            HKUnit.count()
        case .bodyFat:
            HKUnit.percent()
        case .leanMass:
            HKUnit.gramUnit(with: .kilo)
        case .workouts:
            nil
        case .steps:
            HKUnit.count()
        case .distance:
            HKUnit.meterUnit(with: .kilo)
        case .energyExpenditure:
            HKUnit.largeCalorie()
        }
    }
}
