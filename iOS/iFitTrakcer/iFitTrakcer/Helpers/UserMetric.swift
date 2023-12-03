//
//  UserMetric.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import HealthKit
import Foundation

enum UserMetric {
    case height
    case bodyMass
    case bmi
    case bodyFat
    case leanMass
    case steps
    case distance
    case energyExpenditure

    var sampleType: HKSampleType {
        switch self {
        case .height:
            HKQuantityType.quantityType(forIdentifier: .height)!
        case .bodyMass:
            HKQuantityType.quantityType(forIdentifier: .bodyMass)!
        case .bmi:
            HKQuantityType.quantityType(forIdentifier: .bodyMassIndex)!
        case .bodyFat:
            HKQuantityType.quantityType(forIdentifier: .bodyFatPercentage)!
        case .leanMass:
            HKQuantityType.quantityType(forIdentifier: .leanBodyMass)!
        case .steps:
            HKQuantityType.quantityType(forIdentifier: .stepCount)!
        case .distance:
            HKQuantityType.quantityType(forIdentifier: .distanceWalkingRunning)!
        case .energyExpenditure:
            HKQuantityType.quantityType(forIdentifier: .activeEnergyBurned)!
        }
    }

    var unit: HKUnit {
        switch self {
        case .height:
            HKUnit.meterUnit(with: .centi)
        case .bodyMass:
            HKUnit.gramUnit(with: .kilo)
        case .bmi:
            HKUnit.count()
        case .bodyFat:
            HKUnit.percent()
        case .leanMass:
            HKUnit.gramUnit(with: .kilo)
        case .steps:
            HKUnit.count()
        case .distance:
            HKUnit.meterUnit(with: .kilo)
        case .energyExpenditure:
            HKUnit.largeCalorie()
        }
    }
}
