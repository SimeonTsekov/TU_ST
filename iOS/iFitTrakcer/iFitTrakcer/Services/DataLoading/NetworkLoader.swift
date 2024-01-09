//
//  NetworkLoader.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 8.01.24.
//

import OSLog
import Foundation

protocol NetworkLoading {
    func uploadActivityData(workouts: Int,
                            dailySteps: Int,
                            dailyDistance: Int,
                            dailyEnergyBurned: Int) async

    func uploadHealthData(bodyMass: Double,
                          bmi: Double,
                          bodyFat: Double,
                          leanBodyMass: Double) async

    func downloadActivityRecommendationData() async -> String?
    func downloadHealthRecommendationData() async -> String?
}

final class NetworkLoader: NetworkLoading {
    @Injected(\.tokenHandler) private var tokenHandler: TokenHandling

    private let restClient = RESTClient(basePath: basePath)
    private let logger = Logger()

    static private let basePath = "http://localhost:5121/api"

    // MARK: Public

    func uploadActivityData(workouts: Int,
                            dailySteps: Int,
                            dailyDistance: Int,
                            dailyEnergyBurned: Int) async {
        guard let token = tokenHandler.token else {
            self.logger
                .error("[NETWORK] Activity upload failed with missing token")
            return
        }

        let endpoint = PostActivityEndpoint(workouts: workouts,
                                            dailySteps: dailySteps,
                                            dailyDistance: dailyDistance,
                                            dailyEnergyBurned: dailyEnergyBurned)

        let result = await restClient.execute(endpoint, authorization: token)

        switch result {
        case .success:
            self.logger
                .error("[NETWORK] Activity upload successful")
        case .failure(let error as NSError):
            self.logger
                .error("[NETWORK] Activity upload failed with error code \(error.code)")
        }
    }

    func downloadActivityRecommendationData() async -> String? {
        guard let token = tokenHandler.token else {
            self.logger
                .error("[NETWORK] Activity recommendation download failed with missing token")
            return nil
        }

        let endpoint = GetActivityRecommendationEndpoint()

        let result = await restClient.execute(endpoint, authorization: token)

        switch result {
        case .success(let response):
            self.logger
                .error("[NETWORK] Activity recommendation download successful")
            return response.recommendation
        case .failure(let error as NSError):
            self.logger
                .error("[NETWORK] Activity recommendation download failed with error code \(error.code)")
            return nil
        }
    }

    func uploadHealthData(bodyMass: Double,
                          bmi: Double,
                          bodyFat: Double,
                          leanBodyMass: Double) async {
        guard let token = tokenHandler.token else {
            self.logger
                .error("[NETWORK] Health upload failed with missing token")
            return
        }

        let endpoint = PostHealthEndpoint(bodyMass: bodyMass,
                                          bmi: bmi,
                                          bodyFat: bodyFat,
                                          leanBodyMass: leanBodyMass)

        let result = await restClient.execute(endpoint, authorization: token)

        switch result {
        case .success:
            self.logger
                .error("[NETWORK] Health upload successful")
        case .failure(let error as NSError):
            self.logger
                .error("[NETWORK] Health upload failed with error code \(error.code)")
        }
    }

    func downloadHealthRecommendationData() async -> String? {
        guard let token = tokenHandler.token else {
            self.logger
                .error("[NETWORK] Health recommendation download failed with missing token")
            return nil
        }

        let endpoint = GetHealthRecommendationEndpoint()

        let result = await restClient.execute(endpoint, authorization: token)

        switch result {
        case .success(let response):
            self.logger
                .error("[NETWORK] Health recommendation download successful")
            return response.recommendation
        case .failure(let error as NSError):
            self.logger
                .error("[NETWORK] Health recommendation download failed with error code \(error.code)")
            return nil
        }
    }
}
