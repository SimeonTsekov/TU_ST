//
//  File.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 4.01.24.
//

import Combine
import Foundation
import SwiftKeychainWrapper

protocol TokenHandling {
    var token: String? { get }
    var tokenPublisher: PassthroughSubject<String?, Never> { get }

    func storeToken(token: String)
    func retrieveToken()
    func clearToken()
}

class TokenHandler: TokenHandling, ObservableObject {
    private(set) var token: String?
    private let tokenKey = "AuthToken"

    var tokenPublisher = PassthroughSubject<String?, Never>()

    init() {
        retrieveToken()
    }

    func storeToken(token: String) {
        self.token = token
        tokenPublisher.send(token)
        KeychainWrapper.standard.set(token, forKey: tokenKey)
    }

    func retrieveToken() {
        token = KeychainWrapper.standard.string(forKey: tokenKey)
    }

    func clearToken() {
        self.token = nil
        tokenPublisher.send(token)
        KeychainWrapper.standard.removeObject(forKey: tokenKey)
    }
}
