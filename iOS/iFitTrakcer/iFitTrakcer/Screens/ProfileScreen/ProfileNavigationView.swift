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
            profileView
//                .navigationDestination(for: ProfileDestination.self) { $0 }
                .navigationTitle("Profile")
        }
        .environmentObject(router)
    }

    private var profileView: some View {
        ProfileContentView(router: router)
    }
}
