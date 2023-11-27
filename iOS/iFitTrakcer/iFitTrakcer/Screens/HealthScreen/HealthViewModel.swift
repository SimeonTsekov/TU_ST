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

        loadHealthData()
    }

    private func loadHealthData() {
        Task { @MainActor in
            weeklyAverageWeight = await healthKitManager.loadSample(for: .bodyMass)
        }
    }
}
