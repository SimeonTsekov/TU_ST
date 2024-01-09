//
//  RegistrationEnpoint.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 9.01.24.
//

import Foundation

struct RegisterRequestBody {
    let username: String
    let email: String
    let password: String
    let confirmPassword: String
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
