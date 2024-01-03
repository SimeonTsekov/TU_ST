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
            .quantityType(forIdentifier: .height)!
        case .bodyMass:
            .quantityType(forIdentifier: .bodyMass)!
        case .bmi:
            .quantityType(forIdentifier: .bodyMassIndex)!
        case .bodyFat:
            .quantityType(forIdentifier: .bodyFatPercentage)!
        case .leanMass:
            .quantityType(forIdentifier: .leanBodyMass)!
        case .steps:
            .quantityType(forIdentifier: .stepCount)!
        case .distance:
            .quantityType(forIdentifier: .distanceWalkingRunning)!
        case .energyExpenditure:
            .quantityType(forIdentifier: .activeEnergyBurned)!
        }
    }

    var unit: HKUnit {
        switch self {
        case .height:
            .meterUnit(with: .centi)
        case .bodyMass:
            .gramUnit(with: .kilo)
        case .bmi:
            .count()
        case .bodyFat:
            .percent()
        case .leanMass:
            .gramUnit(with: .kilo)
        case .steps:
            .count()
        case .distance:
            .meterUnit(with: .kilo)
        case .energyExpenditure:
            .largeCalorie()
        }
    }
}
