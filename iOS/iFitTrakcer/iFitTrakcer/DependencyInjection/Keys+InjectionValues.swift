//
//  Keys+InjectionValues.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 4.01.24.
//

import Foundation

private struct HealthKitManagerKey: InjectionKey {
    static var currentValue: HealthKitManager = HealthKitManager()
}

private struct TokenHandlerKey: InjectionKey {
    static var currentValue: TokenHandling = TokenHandler()
}

private struct UserAuthenticatorKey: InjectionKey {
    static var currentValue: UserAuthenticating = UserAuthenticator()
}

extension InjectedValues {
    var tokenHandler: TokenHandling {
        get { Self[TokenHandlerKey.self] }
        set { Self[TokenHandlerKey.self] = newValue }
    }

    var healthKitManager: HealthKitManager {
        get { Self[HealthKitManagerKey.self] }
        set { Self[HealthKitManagerKey.self] = newValue }
    }

    var userAuthenticator: UserAuthenticating {
        get { Self[UserAuthenticatorKey.self] }
        set { Self[UserAuthenticatorKey.self] = newValue }
    }
}
