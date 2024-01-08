//
//  ActivityContentView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct ActivityContentView: View {
    @StateObject var viewModel: ActivityViewModel
    var router: ActivityRouter

    var body: some View {
        if viewModel.isLoadingUserActivity {
            ProgressView()
        } else {
            List {
                activitySection
            }
        }
    }

    @ViewBuilder
    private var activitySection: some View {
        Section(header: Text("7-Day Summary")) {
            if let dailyWorkoutEntries = viewModel.dailyWorkoutEntries?.count {
                SimpleListCell(title: "Workouts",
                               value: String(dailyWorkoutEntries))
            }
            if let weeklyAverageStepEntries = viewModel.dailyStepsEntries?.weeklyAverage() {
                SimpleListCell(title: "Steps",
                               value: String(format: "%.2f", weeklyAverageStepEntries))
            }
            if let weeklyAverageDistanceEntries = viewModel.dailyDistanceEntries?.weeklyAverage() {
                SimpleListCell(title: "Distance",
                               value: String(format: "%.2f", weeklyAverageDistanceEntries) + " km")
            }
            if let weeklyAverageEnergyExpenditureEntries = viewModel.dailyEnergyExpenditureEntries?.weeklyAverage() {
                SimpleListCell(title: "Energy Burned",
                               value: String(format: "%.2f", weeklyAverageEnergyExpenditureEntries) + " kcal")
            }
        }

        if let userActivityRecommendation = viewModel.userActivityRecommendation {
            Section(header: Text("Recommendation")) {
                SimpleListCell(title: "AI Recommends",
                               value: userActivityRecommendation)
            }
        }
    }
}
