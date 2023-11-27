//
//  HealthNavigationView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct HealthNavigationView: View {
    @EnvironmentObject private var healthKitManager: HealthKitManager
    @ObservedObject private var router = HealthRouter()

    var body: some View {
        NavigationStack(path: $router.path) {
            healthContentView
//                .navigationDestination(for: HealthDestination.self) { $0 }
                .navigationTitle("Health")
        }
    }

    var healthContentView: some View {
        HealthContentView(viewModel: healthViewModel, router: router)
    }

    var healthViewModel: HealthViewModel {
        HealthViewModel(healthKitManager: healthKitManager)
    }
}
