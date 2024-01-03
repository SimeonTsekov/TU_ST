//
//  ContentView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct ContentView: View {
    @StateObject var router = TabRouter()

    var body: some View {
        TabView(selection: $router.currentTab) {
            ForEach(router.destinations, id: \.rawValue) { destinaton in
                tabView(for: destinaton)
            }
        }
    }

    @ViewBuilder func tabView(for destination: TabDestination) -> some View {
        tabContentView(for: destination)
            .tabItem {
                destination.label(isSelected: destination == router.currentTab)
            }
            .tag(destination)
    }

    @ViewBuilder func tabContentView(for destination: TabDestination) -> some View {
        switch destination {
        case .health:
            HealthNavigationView()
        case .activity:
            ActivityNavigationView()
        case .profile:
            ProfileNavigationView()
        }
    }
}
