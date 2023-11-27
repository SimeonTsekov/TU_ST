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

    var sampleType: HKQuantityType {
        switch self {
        case .bodyMass:
            HKQuantityType.quantityType(forIdentifier: .bodyMass)!
        case .bmi:
            HKQuantityType.quantityType(forIdentifier: .bodyMassIndex)!
        case .bodyFat:
            HKQuantityType.quantityType(forIdentifier: .bodyFatPercentage)!
        case .leanMass:
            HKQuantityType.quantityType(forIdentifier: .leanBodyMass)!
        }
    }

    var unit: HKUnit {
        switch self {
        case .bodyMass:
            HKUnit.gramUnit(with: .kilo)
        case .bmi:
            HKUnit.count()
        case .bodyFat:
            HKUnit.percent()
        case .leanMass:
            HKUnit.gramUnit(with: .kilo)
        }
    }
}
