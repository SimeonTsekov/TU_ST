//
//  HealthViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import Foundation

@MainActor
class HealthViewModel: ObservableObject {
    @Published var userHealth: UserHealthModel?
    @Published var userHealthRecommendation: String?
    @Published var isLoadingUserHealth = true

    @Injected(\.healthKitManager) private var healthKitManager: HealthKitManager
    @Injected(\.networkLoader) private var networkLoader: NetworkLoading

    var bodyMassEntries: [Double]? {
        userHealth?.bodyMassEntries
    }

    var bmiEntries: [Double]? {
        userHealth?.bmiEntries
    }

    var bodyFatEntries: [Double]? {
        userHealth?.bodyFatEntries
    }

    var leanBodyMassEntries: [Double]? {
        userHealth?.leanBodyMassEntries
    }

    func initialiseData() async {
        await loadHealthKitHealthData()
        await uploadHealthDataAndGenerateReccomendations()
    }

    private func uploadHealthDataAndGenerateReccomendations() async {
        await uploadHealthData()
        await downloadHealthRecommendationData()
    }

    private func uploadHealthData() async {
        let healthRequestBody = HealthRequestBody(bodyMass: bodyMassEntries?.average() ?? 0,
                                                  bmi: bmiEntries?.average() ?? 0,
                                                  bodyFat: bodyFatEntries?.average() ?? 0,
                                                  leanBodyMass: leanBodyMassEntries?.average() ?? 0)
        await networkLoader.uploadHealthData(healthRequestBody)
    }

    private func downloadHealthRecommendationData() async {
        let result = await networkLoader.downloadRecommendationData(for: .health)
        userHealthRecommendation = result
    }

    private func loadHealthKitHealthData() async {
        userHealth = await fetchHealthData()
        isLoadingUserHealth = false
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
