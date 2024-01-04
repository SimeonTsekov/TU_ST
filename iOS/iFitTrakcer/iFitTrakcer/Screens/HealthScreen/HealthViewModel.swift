//
//  HealthViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import Foundation

@MainActor
class HealthViewModel: ObservableObject {
    @Published var userHealth = UserHealthModel()
    @Published var userHealthRecommendation: String? = "Sample health recommendation"

    @Injected(\.healthKitManager) private var healthKitManager: HealthKitManager

    var bodyMassEntries: [Double] {
        userHealth.bodyMassEntries
    }

    var bmiEntries: [Double] {
        userHealth.bmiEntries
    }

    var bodyFatEntries: [Double] {
        userHealth.bodyFatEntries
    }

    var leanBodyMassEntries: [Double] {
        userHealth.leanBodyMassEntries
    }

    init() {
        loadHealthData()
    }

    private func loadHealthData() {
        Task { @MainActor [weak self] in
            guard let self else {
                return
            }

            self.userHealth = await fetchHealthData()
        }
    }

    private func fetchHealthData() async -> UserHealthModel {
        let userHealth = UserHealthModel()

        userHealth.bodyMassEntries = await healthKitManager.loadSamples(for: .bodyMass)
        userHealth.bmiEntries = await healthKitManager.loadSamples(for: .bmi)
        userHealth.bodyFatEntries = await healthKitManager.loadSamples(for: .bodyFat)
        userHealth.leanBodyMassEntries = await healthKitManager.loadSamples(for: .leanMass)

        return userHealth
    }
}
