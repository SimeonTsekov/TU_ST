//
//  NetworkLoader.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 8.01.24.
//

import OSLog
import Foundation

protocol NetworkLoading {
    func uploadActivityData(_ requestBody: ActivityRequestBody) async
    func uploadHealthData(_ requestBody: HealthRequestBody) async
    func downloadRecommendationData(for endpoint: RecommendationEndpoint) async -> String?
}

final class NetworkLoader: NetworkLoading {
    @Injected(\.tokenHandler) private var tokenHandler: TokenHandling

    private let restClient = RESTClient(basePath: basePath)
    private let logger = Logger()

    static private let basePath = "http://localhost:5121/api"

    func uploadActivityData(_ requestBody: ActivityRequestBody) async {
        let endpoint = PostActivityEndpoint(workouts: requestBody.workouts,
                                            dailySteps: requestBody.dailySteps,
                                            dailyDistance: requestBody.dailyDistance,
                                            dailyEnergyBurned: requestBody.dailyEnergyBurned)

        let _: PostActivityEndpoint.ResponseModel? = await authorizeAndExecuteEndpoint(with: endpoint)
    }

    func uploadHealthData(_ requestBody: HealthRequestBody) async {
        let endpoint = PostHealthEndpoint(bodyMass: requestBody.bodyMass,
                                          bmi: requestBody.bmi,
                                          bodyFat: requestBody.bodyFat,
                                          leanBodyMass: requestBody.leanBodyMass)

        let _: PostHealthEndpoint.ResponseModel? = await authorizeAndExecuteEndpoint(with: endpoint)
    }

    func downloadRecommendationData(for endpoint: RecommendationEndpoint) async -> String? {
        let result: RecommendationEndpoint.ResponseModel? = await authorizeAndExecuteEndpoint(with: endpoint)
        return result?.recommendation
    }

    private func authorizeAndExecuteEndpoint<E: Endpoint, R>(with endpoint: E) async -> R? {
        guard let token = tokenHandler.token else {
            self.logger
                .error("[NETWORK] \(E.self) failed with missing token")
            return nil
        }

        let result: R? = await executeEnpoint(with: endpoint, authorization: token)

        return result
    }

    private func executeEnpoint<E: Endpoint, R>(with endpoint: E, 
                                                authorization token: String) async -> R? {
        let result = await restClient.execute(endpoint, authorization: token)

        switch result {
        case .success(let data):
            self.logger
                .error("[NETWORK] \(E.self) successful")
            return data as? R
        case .failure(let error as NSError):
            self.logger
                .error("[NETWORK] \(E.self) failed with error code \(error.code)")
            return nil
        }
    }
}
