//
//  ProfileNavigationView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct ProfileNavigationView: View {
    @StateObject private var router = ProfileRouter()

    var body: some View {
        NavigationStack(path: $router.path) {
            profileContentView
                .navigationDestination(for: ProfileDestination.self) { $0 }
                .navigationTitle("Profile")
        }
        .environmentObject(router)
    }

    private var profileContentView: some View {
        ProfileContentView(viewModel: profileViewModel, router: router)
    }

    var profileViewModel: ProfileViewModel {
        ProfileViewModel()
    }
}
