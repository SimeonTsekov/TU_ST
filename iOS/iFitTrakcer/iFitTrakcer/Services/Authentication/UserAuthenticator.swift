//
//  AuthService.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.01.24.
//

import Foundation
import OSLog

protocol UserAuthenticating {
    func login(_ requestBody: LoginRequestBody) async
    func register(_ requestBody: RegisterRequestBody) async
    func logout()
}

final class UserAuthenticator: UserAuthenticating {
    @Injected(\.tokenHandler) private var tokenHandler: TokenHandling

    private let restClient = RESTClient(basePath: basePath)
    private let logger = Logger()

    static private let basePath = "http://localhost:5121/api/Auth"

    func login(_ requestBody: LoginRequestBody) async {
        let endpoint = LoginEndpoint(email: requestBody.email,
                                     password: requestBody.password)

        await executeAuthEndpoint(with: endpoint)
    }

    func register(_ requestBody: RegisterRequestBody) async {
        let endpoint = RegisterEndpoint(username: requestBody.username,
                                        email: requestBody.email,
                                        password: requestBody.password,
                                        confirmPassword: requestBody.confirmPassword)

        await executeAuthEndpoint(with: endpoint)
    }

    func logout() {
        tokenHandler.clearToken()
    }

    private func executeAuthEndpoint<E: Endpoint>(with endpoint: E) async {
        let result = await restClient.execute(endpoint)

        switch result {
        case .success(let response):
            if let tokenResponse = response as? UserTokenResponse {
                tokenHandler.storeToken(token: tokenResponse.accessToken)
            }
        case .failure(let error as NSError):
            self.logger
                .error("[AUTH] Register failed with error code \(error.code)")
        }
    }
}
