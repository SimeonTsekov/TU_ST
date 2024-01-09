//
//  RegisterViewModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 12.12.23.
//

import Combine
import Foundation

@MainActor
class RegisterViewModel: ObservableObject {
    @Published var username = ""
    @Published var email = ""
    @Published var password = ""
    @Published var confirmPassword = ""

    @Published var errorMessage: String?
    @Published var isRegistering = false

    @Injected(\.userAuthenticator) private var userAuthenticator: UserAuthenticating
    @Injected(\.tokenHandler) private var tokenHandler: TokenHandling

    private let router: ProfileRouter
    private var cancellable: AnyCancellable?

    var registerIsDisabled: Bool {
        username.isEmpty || email.isEmpty || password.isEmpty || confirmPassword.isEmpty
    }

    var emailIsValid: Bool {
        let emailRegex = "[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}"
        return NSPredicate(format: "SELF MATCHES %@", emailRegex)
            .evaluate(with: email)
    }

    var passwordIsValid: Bool {
        let passwordRegex = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$"
        return NSPredicate(format: "SELF MATCHES %@", passwordRegex)
            .evaluate(with: password)
    }

    var passwordMatchesConfirmation: Bool {
        password == confirmPassword
    }

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

    func validateFields() -> Bool {
        guard emailIsValid else {
            errorMessage = "Enter a valid email address"
            return false
        }

        guard passwordIsValid else {
            errorMessage = """
            Password must be at least 8 characters long,
            contain 1 uppercase letter, 1 lowercase letter and 1 number
            """
            return false
        }

        guard passwordMatchesConfirmation else {
            errorMessage = "Passwords do NOT match"
            return false
        }

        errorMessage = nil
        return true
    }

    func registerAction() async {
        isRegistering = true
        await register()
        isRegistering = false
    }

    func register() async {
        let registerRequestBody = RegisterRequestBody(username: username,
                                                      email: email,
                                                      password: password,
                                                      confirmPassword: confirmPassword)
        await userAuthenticator.register(registerRequestBody)
    }
}
