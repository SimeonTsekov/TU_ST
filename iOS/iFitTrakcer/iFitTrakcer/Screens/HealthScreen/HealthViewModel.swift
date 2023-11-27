//
//  HealthViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import Foundation

class HealthViewModel: ObservableObject {
    @MainActor @Published var weeklyAverageWeight: Double?
    var healthKitManager: HealthKitManager

    init(healthKitManager: HealthKitManager) {
        self.healthKitManager = healthKitManager

        healthKitManager.loadSample { [weak self] sample in
            guard let self else {
                return
            }

            Task { @MainActor in
                self.weeklyAverageWeight = sample
            }
        }
    }
}
