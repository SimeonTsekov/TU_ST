//
//  LoginEndpoint.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 4.01.24.
//

import Foundation

struct LoginRequestBody {
    let email: String
    let password: String
}

struct LoginEndpoint: POSTEndpoint {
    typealias ResponseModel = UserTokenResponse

    var path: String = "/login"

    let email: String
    let password: String

    enum CodingKeys: String, CodingKey {
        case email, password
    }
}
