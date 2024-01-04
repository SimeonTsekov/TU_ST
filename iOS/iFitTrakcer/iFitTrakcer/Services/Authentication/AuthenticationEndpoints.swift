//
//  AuthenticationEndpoints.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 4.01.24.
//

import Foundation

struct UserTokenResponse: Decodable {
    let token: String
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

struct RegisterEndpoint: POSTEndpoint {
    typealias ResponseModel = UserTokenResponse

    var path: String = "/register"

    let username: String
    let email: String
    let password: String
    let confirmPassword: String

    enum CodingKeys: String, CodingKey {
        case username, email, password, confirmPassword
    }
}
