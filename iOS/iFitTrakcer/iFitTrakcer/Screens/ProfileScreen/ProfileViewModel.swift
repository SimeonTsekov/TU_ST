//
//  ProfileViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.12.23.
//

import Foundation

@MainActor
class ProfileViewModel: ObservableObject {
    @Injected(\.healthKitManager) private var healthKitManager: HealthKitManager

    @Published var userData = UserDataModel()

    init() {
        loadUserData()
    }

    private func loadUserData() {
        Task { @MainActor [weak self] in
            guard let self else {
                return
            }

            self.userData = await fetchUserData()
        }
    }

    private func fetchUserData() async -> UserDataModel {
        let userData = UserDataModel()

        userData.dateOfBirth = await healthKitManager.loadDateOfBirth()
        userData.biologicalSex = await healthKitManager.loadBiologicalSex()
        userData.height = Int(await getUserHeight() ?? 0)

        return userData
    }

    private func getUserHeight() async -> Double? {
        var heightEntries: [Double]
        heightEntries = await healthKitManager.loadSamples(for: .height, timePredicate: .alltime)
        return heightEntries.last
    }
}
