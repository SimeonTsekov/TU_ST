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

    var sampleType: HKQuantityType {
        switch self {
        case .bodyMass:
            HKQuantityType.quantityType(forIdentifier: .bodyMass)!
        }
    }

    var unit: HKUnit {
        switch self {
        case .bodyMass:
            HKUnit.gramUnit(with: .kilo)
        }
    }
}
