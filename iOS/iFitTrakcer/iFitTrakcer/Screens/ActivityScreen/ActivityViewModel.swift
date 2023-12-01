//
//  ActivityViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 1.12.23.
//

import Foundation
import HealthKit

@MainActor
class ActivityViewModel: ObservableObject {
    @Published var userActivity = UserActivityhModel()
    var healthKitManager: HealthKitManager

    var dailyWorkoutEntries: [HKWorkout] {
        userActivity.workoutEntries
    }

    var dailyStepsEntries: [Double] {
        userActivity.dailyStepsEntries
    }

    var dailyDistanceEntries: [Double] {
        userActivity.dailyDistanceEntries
    }

    var dailyEnergyExpenditureEntries: [Double] {
        userActivity.dailyEnergyExpenditureEntries
    }

    init(healthKitManager: HealthKitManager) {
        self.healthKitManager = healthKitManager
        loadActivityData()
    }

    private func loadActivityData() {
        let userActivity = UserActivityhModel()

        Task { @MainActor [weak self] in
            guard let self else {
                return
            }

            userActivity.workoutEntries = await healthKitManager.loadWorkouts()
            userActivity.dailyStepsEntries = await healthKitManager.loadSamples(for: .steps)
            userActivity.dailyDistanceEntries = await healthKitManager.loadSamples(for: .distance)
            userActivity.dailyEnergyExpenditureEntries = await healthKitManager.loadSamples(for: .energyExpenditure)

            self.userActivity = userActivity
        }
    }
}
