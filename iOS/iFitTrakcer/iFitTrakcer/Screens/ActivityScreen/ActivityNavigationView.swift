//
//  ActivityNavigationView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct ActivityNavigationView: View {
    @EnvironmentObject private var healthKitManager: HealthKitManager
    @ObservedObject private var router = ActivityRouter()

    var body: some View {
        NavigationStack(path: $router.path) {
            activityContentView
                // .navigationDestination(for: ActivityDestination.self) { $0 }
                .navigationTitle("Activity")
        }
    }

    var activityContentView: some View {
        ActivityContentView(viewModel: activityViewModel, router: router)
    }

    var activityViewModel: ActivityViewModel {
        ActivityViewModel(healthKitManager: healthKitManager)
    }
}
