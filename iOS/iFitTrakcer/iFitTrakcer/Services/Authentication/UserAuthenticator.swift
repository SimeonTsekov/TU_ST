//
//  AuthService.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.01.24.
//

import Foundation
import OSLog

protocol UserAuthenticating {
    func login(email: String, password: String) async -> Bool
    func register(username: String, email: String, password: String, confirmPassword: String) async -> Bool
    func logout()
}

final class UserAuthenticator: UserAuthenticating {
    @Injected(\.tokenHandler) private var tokenHandler: TokenHandling

    private let restClient = RESTClient(basePath: basePath)
    private let logger = Logger()

    static private let basePath = "http://localhost:5121/api/Auth"

    // MARK: Public

    func login(email: String, password: String) async -> Bool {
        let result = await executeLoginRequest(email: email, password: password)

        switch result {
        case .success(let response):
            tokenHandler.storeToken(token: response.token)
            return true
        case .failure:
            return false
        }
    }

    func register(username: String, email: String, password: String, confirmPassword: String) async -> Bool {
        let result = await executeRegisterRequest(username: username,
                                                  email: email,
                                                  password: password,
                                                  confirmPassword: confirmPassword)

        switch result {
        case .success(let response):
            tokenHandler.storeToken(token: response.token)
            return true
        case .failure:
            return false
        }
    }

    func logout() {
        tokenHandler.clearToken()
    }

    // MARK: Private

    private func executeLoginRequest(email: String,
                                     password: String) async -> Result<LoginEndpoint.ResponseModel, Error> {
        let loginEndpoint = LoginEndpoint(email: email, password: password)

        let result = await withCheckedContinuation { continuation in
            restClient.call(loginEndpoint) { result in
                continuation.resume(returning: result)
            }
        }

        return result
    }

    private func executeRegisterRequest(username: String,
                                        email: String,
                                        password: String,
                                        confirmPassword: String) async
    -> Result<RegisterEndpoint.ResponseModel, Error> {
        let registerEndpoint = RegisterEndpoint(username: username,
                                                email: email,
                                                password: password,
                                                confirmPassword: confirmPassword)

        let result = await withCheckedContinuation { continuation in
            restClient.call(registerEndpoint) { result in
                continuation.resume(returning: result)
            }
        }

        return result
    }
}
