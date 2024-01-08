//
//  ProfileViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.12.23.
//

import Combine
import Foundation

@MainActor
class ProfileViewModel: ObservableObject {
    @Injected(\.healthKitManager) private var healthKitManager: HealthKitManager
    @Injected(\.tokenHandler) private var tokenHandler: TokenHandling

    @Published var userData: UserDataModel = UserDataModel()
    @Published var isLoggedIn = false

    private var cancellable: AnyCancellable?

    init() {
        Task {
            await loadUserData()
        }

        isLoggedIn = tokenHandler.token != nil

        cancellable = tokenHandler.tokenPublisher
            .receive(on: DispatchQueue.main)
            .sink { [weak self] token in
                guard let self else { return }

                guard token != nil else {
                    self.isLoggedIn = false
                    return
                }

                self.isLoggedIn = true
                Task {
                    await self.loadUserData()
                }
            }
    }

    func logOut() {
        tokenHandler.clearToken()
    }

    private func loadUserData() async {
        userData = await fetchUserData()
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
