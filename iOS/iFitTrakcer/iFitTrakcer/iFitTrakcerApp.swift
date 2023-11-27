//
//  iFitTrakcerApp.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

@main
struct IFitTrakcerApp: App {
    var healthKitManager = HealthKitManager()

    var body: some Scene {
        WindowGroup {
            ContentView()
                .onAppear {
                    healthKitManager.requestAuthorization()
                }
                .environmentObject(healthKitManager)
        }
    }
}
