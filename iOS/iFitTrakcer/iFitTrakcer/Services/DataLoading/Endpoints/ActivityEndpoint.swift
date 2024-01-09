//
//  ActivityEndpoint.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 8.01.24.
//

import Foundation

struct ActivityRequestBody {
    let workouts: Int
    let dailySteps: Double
    let dailyDistance: Double
    let dailyEnergyBurned: Double
}

struct ActivityResponse: Decodable {
    let id: Int
    let userId: Int
    let workouts: Double
    let dailySteps: Double
    let dailyDistance: Double
    let dailyEnergyBurned: Double
    let createdDate: String
}

struct PostActivityEndpoint: POSTEndpoint {
    typealias ResponseModel = ActivityResponse

    var path: String = "/Activity"

    let workouts: Int
    let dailySteps: Double
    let dailyDistance: Double
    let dailyEnergyBurned: Double

    enum CodingKeys: String, CodingKey {
        case workouts, dailySteps, dailyDistance, dailyEnergyBurned
    }
}
