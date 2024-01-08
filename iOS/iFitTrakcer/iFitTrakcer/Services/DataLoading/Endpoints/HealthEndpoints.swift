//
//  HealthEndpoints.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 8.01.24.
//

import Foundation

struct HealthResponse: Decodable {
    let id: Int
    let userId: Double
    let bodyMass: Double
    let bmi: Double
    let bodyFat: Double
    let leanBodyMass: Double
    let createdDate: String
}

struct PostHealthEndpoint: POSTEndpoint {
    typealias ResponseModel = HealthResponse

    var path: String = "/HealthData"

    let bodyMass: Double
    let bmi: Double
    let bodyFat: Double
    let leanBodyMass: Double

    enum CodingKeys: String, CodingKey {
        case bodyMass, bmi, bodyFat, leanBodyMass
    }
}

struct HealthRecommendationResponse: Decodable {
    let id: Int
    let recommendation: String
    let createdDate: String
}

struct GetHealthRecommendationEndpoint: GETEndpoint {
    typealias ResponseModel = HealthRecommendationResponse

    var path: String = "/HealthRecommendation/generate"
}
