//
//  ActivityContentView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct ActivityContentView: View {
    var router: ActivityRouter

    var body: some View {
        List {
            activitySection
        }
    }

    private var activitySection: some View {
        Section(header: Text("Summary")) {
            SimpleListCell(title: "Workouts", value: "-")
            SimpleListCell(title: "DailySteps", value: "3000")
            SimpleListCell(title: "DailyDistance", value: "5 km")
            SimpleListCell(title: "DailyEnergyBurned", value: "100 kcal")
        }
    }
}
