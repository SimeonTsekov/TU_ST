//
//  ProfileViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.12.23.
//

import Foundation

@MainActor
class ProfileViewModel: ObservableObject {
    @Published var userData = UserDataModel()
    var healthKitManager: HealthKitManager

    init(healthKitManager: HealthKitManager) {
        self.healthKitManager = healthKitManager
        loadUserData()
    }

    private func loadUserData() {
        let userData = UserDataModel()

        Task { @MainActor [weak self] in
            guard let self else {
                return
            }

            userData.dateOfBirth = await healthKitManager.loadDateOfBirth()
            userData.biologicalSex = await healthKitManager.loadBiologicalSex()
//            userData.height = await healthKitManager.loadSamples(for: .height, timePredicate: .alltime)

            self.userData = userData
        }
    }
}
