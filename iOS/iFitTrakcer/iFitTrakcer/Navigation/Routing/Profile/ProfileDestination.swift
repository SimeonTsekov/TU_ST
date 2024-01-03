//
//  ProfileDestination.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import Foundation
import SwiftUI

enum ProfileDestination {
    case login(User)
    case register
}

extension ProfileDestination: Hashable {
    func hash(into hasher: inout Hasher) {
        hasher.combine(self.hashValue)
    }

    static func == (lhs: ProfileDestination, rhs: ProfileDestination) -> Bool {
        switch (lhs, rhs) {
        case(.login, .login):
            return true
        case(.register, .register):
            return true
        default:
            return false
        }
    }
}

extension ProfileDestination: View {
    var body: some View {
        switch self {
        case .login(let user):
            LoginView(viewModel: loginViewModel(with: user))
        case .register:
            RegisterScreenView()
        }
    }

    private func loginViewModel(with user: User) -> LoginViewModel {
        let userAuthenticator = UserAuthenticator(user: user)
        return LoginViewModel(userAuthenticator: userAuthenticator)
    }
}
