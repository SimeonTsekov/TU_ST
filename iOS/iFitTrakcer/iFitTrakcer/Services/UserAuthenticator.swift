//
//  AuthService.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.01.24.
//

import Foundation
import OSLog
import SwiftKeychainWrapper

class User: ObservableObject {
    var token: String?

    static let tokenKey = "AuthToken"

    func storeToken(token: String) {
        self.token = token
        KeychainWrapper.standard.set(token, forKey: User.tokenKey)
    }

    func retrieveToken() {
        token = KeychainWrapper.standard.string(forKey: User.tokenKey)
    }
}

protocol UserAuthenticating {
    func login(email: String, password: String) async -> Bool
    func register()
    func logout()
}

class UserAuthenticator: UserAuthenticating {
    let user: User
    let restClient: RESTClient

    private let logger = Logger()
    private let basePath = "http://localhost:5121/api/Auth"

    init(user: User) {
        self.user = user
        self.restClient = RESTClient(basePath: basePath)
    }

    func login(email: String, password: String) async -> Bool {
        let loginEndpoint = LoginEndpoint(email: email, password: password)

        let result = await withCheckedContinuation { continuation in
            restClient.call(loginEndpoint) { result in
                continuation.resume(returning: result)
            }
        }

        switch result {
        case .success(let response):
            user.storeToken(token: response.token)
            return true
        case .failure:
            return false
        }
    }

    func register() {
    }

    func logout() {
        user.token = nil
    }
}

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
