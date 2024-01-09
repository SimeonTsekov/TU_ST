//
//  AuthService.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.01.24.
//

import Foundation
import OSLog

protocol UserAuthenticating {
    func login(email: String,
               password: String) async
    func register(username: String,
                  email: String,
                  password: String,
                  confirmPassword: String) async
    func logout()
}

final class UserAuthenticator: UserAuthenticating {
    @Injected(\.tokenHandler) private var tokenHandler: TokenHandling

    private let restClient = RESTClient(basePath: basePath)
    private let logger = Logger()

    static private let basePath = "http://localhost:5121/api/Auth"


    func login(email: String, password: String) async {
        let endpoint = LoginEndpoint(email: email, password: password)
        let result = await restClient.execute(endpoint)

        switch result {
        case .success(let response):
            tokenHandler.storeToken(token: response.accessToken)
        case .failure(let error as NSError):
            self.logger
                .error("[AUTH] Register failed with error code \(error.code)")
        }
    }

    func register(username: String, email: String, password: String, confirmPassword: String) async {
        let endpoint = RegisterEndpoint(username: username,
                                        email: email,
                                        password: password,
                                        confirmPassword: confirmPassword)
        let result = await restClient.execute(endpoint)

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
}
