//
//  File.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 4.01.24.
//

import Foundation
import SwiftKeychainWrapper

protocol TokenHandling {
    var token: String? { get }

    func storeToken(token: String)
    func retrieveToken()
    func clearToken()
}

class TokenHandler: TokenHandling, ObservableObject {
    private(set) var token: String?

    private let tokenKey = "AuthToken"

    init() {
        retrieveToken()
    }

    func storeToken(token: String) {
        self.token = token
        KeychainWrapper.standard.set(token, forKey: tokenKey)
    }

    func retrieveToken() {
        token = KeychainWrapper.standard.string(forKey: tokenKey)
    }

    func clearToken() {
        self.token = nil
        KeychainWrapper.standard.removeObject(forKey: tokenKey)
    }
}
