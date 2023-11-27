//
//  HealthViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import HealthKit
import Foundation

class HealthViewModel: ObservableObject {
    @MainActor @Published var weeklyAverageWeight: Double?
    var healthKitManager: HealthKitManager

    init(healthKitManager: HealthKitManager) {
        self.healthKitManager = healthKitManager

        healthKitManager.loadSample(sampleType: HKQuantityType.quantityType(forIdentifier: .bodyMass)!,
                                    unit: HKUnit.gramUnit(with: .kilo)) { [weak self] (result: Result<Double, HealthKitError>) in
            guard let self else {
                return
            }

            Task { @MainActor in
                switch result {
                case .success(let value):
                    self.weeklyAverageWeight = value
                case .failure(_):
                    return
                }
            }
        }
    }
}
