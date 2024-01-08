//
//  ActivityEndpoints.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 8.01.24.
//

import Foundation

struct ActivityResponse: Decodable {
    let id: Int
    let userId: Int
    let workouts: Int
    let dailySteps: Int
    let dailyDistance: Int
    let dailyEnergyBurned: Int
    let createdDate: String
}

struct PostActivityEndpoint: POSTEndpoint {
    typealias ResponseModel = ActivityResponse

    var path: String = "/Activity"

    let workouts: Int
    let dailySteps: Int
    let dailyDistance: Int
    let dailyEnergyBurned: Int

    enum CodingKeys: String, CodingKey {
        case workouts, dailySteps, dailyDistance, dailyEnergyBurned
    }
}

struct ActivityRecommendationResponse: Decodable {
    let id: Int
    let recommendation: String
    let createdDate: String
}

struct GetActivityRecommendationEndpoint: GETEndpoint {
    typealias ResponseModel = ActivityRecommendationResponse

    var path: String = "/ActivityRecommendation/generate"
}
