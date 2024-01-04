//
//  ProfileDestination.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import Foundation
import SwiftUI

enum ProfileDestination {
    case login(ProfileRouter)
    case register(ProfileRouter)
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
        case .login(let router):
            LoginView(viewModel: LoginViewModel(router: router))
        case .register(let router):
            RegisterScreenView(viewModel: RegisterViewModel(router: router))
        }
    }
}
