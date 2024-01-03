//
//  LoginViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.01.24.
//

import Foundation

class LoginViewModel: ObservableObject {
    @Published var email = ""
    @Published var password = ""
    @Published var errorMessage: String?
    @Published var isLoggingIn = false

    private let router: ProfileRouter
    private let userAuthenticator: UserAuthenticating

    init(userAuthenticator: UserAuthenticating, router: ProfileRouter) {
        self.userAuthenticator = userAuthenticator
        self.router = router
    }

    var loginIsDisabled: Bool {
        email.isEmpty || password.isEmpty
    }

    func logInAction() async {
        isLoggingIn = true
        await logIn()
        isLoggingIn = false
    }

    private func logIn() async {
        let success = await userAuthenticator.login(email: email, password: password)
        if success {
            router.popBack()
        } else {
            errorMessage = "Something went wrong"
        }
    }

    func pushRegister() {
        router.pushRegister()
    }
}
