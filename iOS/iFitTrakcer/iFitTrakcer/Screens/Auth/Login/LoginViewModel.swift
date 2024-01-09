//
//  LoginViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.01.24.
//

import Combine
import Foundation

@MainActor
class LoginViewModel: ObservableObject {
    @Published var email = ""
    @Published var password = ""
    @Published var errorMessage: String?
    @Published var isLoggingIn = false

    @Injected(\.userAuthenticator) private var userAuthenticator: UserAuthenticating
    @Injected(\.tokenHandler) private var tokenHandler: TokenHandling

    private let router: ProfileRouter
    private var cancellable: AnyCancellable?

    init(router: ProfileRouter) {
        self.router = router

        cancellable = tokenHandler.tokenPublisher
            .receive(on: DispatchQueue.main)
            .sink { [weak self] token in
                guard let self else { return }

                guard token != nil else {
                    self.errorMessage = "Something went wrong"
                    return
                }

                self.router.popBack()
            }
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
        let loginRequestBody = LoginRequestBody(email: email, password: password)
        await userAuthenticator.login(loginRequestBody)
    }

    func pushRegister() {
        router.pushRegister()
    }
}
