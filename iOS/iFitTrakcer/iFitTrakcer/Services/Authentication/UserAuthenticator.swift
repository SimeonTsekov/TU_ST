//
//  AuthService.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.01.24.
//

import Foundation
import OSLog

protocol UserAuthenticating {
    func login(email: String, password: String) async
    func register(username: String, email: String, password: String, confirmPassword: String) async
    func logout()
}

final class UserAuthenticator: UserAuthenticating {
    @Injected(\.tokenHandler) private var tokenHandler: TokenHandling

    private let restClient = RESTClient(basePath: basePath)
    private let logger = Logger()

    static private let basePath = "http://localhost:5121/api/Auth"

    // MARK: Public

    func login(email: String, password: String) async {
        let loginEndpoint = LoginEndpoint(email: email, password: password)
        let result = await executeLoginRequest(loginEndpoint: loginEndpoint)

        switch result {
        case .success(let response):
            tokenHandler.storeToken(token: response.accessToken)
        case .failure(let error as NSError):
            self.logger
                .error("[AUTH] Register failed with error code \(error.code)")
        }
    }

    func register(username: String, email: String, password: String, confirmPassword: String) async {
        let registerEndpoint = RegisterEndpoint(username: username,
                                                email: email,
                                                password: password,
                                                confirmPassword: confirmPassword)
        let result = await executeRegisterRequest(registerEndpoint: registerEndpoint)

        switch result {
        case .success(let response):
            tokenHandler.storeToken(token: response.accessToken)
        case .failure(let error as NSError):
            self.logger
                .error("[AUTH] Register failed with error code \(error.code)")
        }
    }

    func logout() {
        tokenHandler.clearToken()
    }

    // MARK: Private

    private func executeLoginRequest(loginEndpoint: LoginEndpoint) async -> Result<LoginEndpoint.ResponseModel, Error> {

        let result = await withCheckedContinuation { continuation in
            restClient.call(loginEndpoint) { result in
                continuation.resume(returning: result)
            }
        }

        return result
    }

    private func executeRegisterRequest(registerEndpoint: RegisterEndpoint) async
    -> Result<RegisterEndpoint.ResponseModel, Error> {
        let result = await withCheckedContinuation { continuation in
            restClient.call(registerEndpoint) { result in
                continuation.resume(returning: result)
            }
        }

        return result
    }
}
