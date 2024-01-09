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
    @Published var userActivity: UserActivityhModel?
    @Published var userActivityRecommendation: String?
    @Published var isLoadingUserActivity = true

    @Injected(\.healthKitManager) private var healthKitManager: HealthKitManager
    @Injected(\.networkLoader) private var networkLoader: NetworkLoading

    var dailyWorkoutEntries: [HKWorkout]? {
        userActivity?.workoutEntries
    }

    var dailyStepsEntries: [Double]? {
        userActivity?.dailyStepsEntries
    }

    var dailyDistanceEntries: [Double]? {
        userActivity?.dailyDistanceEntries
    }

    var dailyEnergyExpenditureEntries: [Double]? {
        userActivity?.dailyEnergyExpenditureEntries
    }

    func initialiseData() async {
        await loadHealthKitActivityData()
        await uploadActivityDataAndGenerateReccomendations()
    }

    private func uploadActivityDataAndGenerateReccomendations() async {
        await uploadActivityData()
        await downloadActivityRecommendationData()
    }

    private func uploadActivityData() async {
        let activityRequestBody = ActivityRequestBody(workouts: dailyWorkoutEntries?.count ?? 0,
                                                      dailySteps: dailyStepsEntries?.average() ?? 0,
                                                      dailyDistance: dailyDistanceEntries?.average() ?? 0,
                                                      dailyEnergyBurned: dailyEnergyExpenditureEntries?.average() ?? 0)
        await networkLoader.uploadActivityData(activityRequestBody)
    }

    private func downloadActivityRecommendationData() async {
        let result = await networkLoader.downloadRecommendationData(for: .activity)
        userActivityRecommendation = result
    }

    private func loadHealthKitActivityData() async {
        userActivity = await fetchHealthKitActivityData()
        isLoadingUserActivity = false
    }

    private func fetchHealthKitActivityData() async -> UserActivityhModel {
        let userActivity = UserActivityhModel()

        userActivity.workoutEntries = await healthKitManager.loadWorkouts()
        userActivity.dailyStepsEntries = await healthKitManager.loadSamples(for: .steps)
        userActivity.dailyDistanceEntries = await healthKitManager.loadSamples(for: .distance)
        userActivity.dailyEnergyExpenditureEntries = await healthKitManager.loadSamples(for: .energyExpenditure)

        return userActivity
    }
}
