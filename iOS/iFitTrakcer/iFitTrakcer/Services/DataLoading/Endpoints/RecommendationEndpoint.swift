//
//  RecommendationEndpoint.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 9.01.24.
//

import Foundation

struct RecommendationResponse: Decodable {
    let id: Int
    let recommendation: String
    let createdDate: String
}

enum RecommendationEndpoint: GETEndpoint {
    typealias ResponseModel = RecommendationResponse

    case health
    case activity

    var logId: String {
        switch self {
        case .health:
            "health"
        case .activity:
            "activity"
        }
    }

    var path: String {
        switch self {
        case .health:
            "/HealthRecommendation/generate"
        case .activity:
            "/ActivityRecommendation/generate"
        }
    }
}
