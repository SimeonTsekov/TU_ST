//
//  HealthNavigationView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct HealthNavigationView: View {
    @ObservedObject private var router = HealthRouter()

    var body: some View {
        NavigationStack(path: $router.path) {
            healthContentView
//                .navigationDestination(for: HealthDestination.self) { $0 }
                .navigationTitle("Health")
                .navigationBarTitleDisplayMode(.inline)
        }
    }

    var healthContentView: some View {
        HealthContentView(router: router)
    }
}

#Preview {
    HealthNavigationView()
}
