//
//  HealthViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import Foundation

@MainActor
class HealthViewModel: ObservableObject {
    @Published var weeklyAverageWeight: Double?
    var healthKitManager: HealthKitManager

    init(healthKitManager: HealthKitManager) {
        self.healthKitManager = healthKitManager

        healthKitManager.loadSample { [weak self] sample in
            guard let self else {
                return
            }

            self.weeklyAverageWeight = sample
        }
    }
}
