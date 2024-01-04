//
//  LoginScreenView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 12.12.23.
//

import Foundation
import SwiftUI

struct LoginView: View {
    @StateObject var viewModel: LoginViewModel

    var body: some View {
        VStack(alignment: .center, spacing: 16) {
            titleText
            emailInputField
            passwordInputField
            errorMessage

            if viewModel.isLoggingIn {
                ProgressView()
            } else {
                loginButton
                registerButton
            }
        }
        .padding([.leading, .trailing], 16)
    }

    private var titleText: some View {
        Text("Login")
            .font(.system(.largeTitle, design: .rounded))
            .fontWeight(.bold)
    }

    private var emailInputField: some View {
        GenericTextField(placeholder: "Email", text: $viewModel.email)
    }

    private var passwordInputField: some View {
        GenericSecureField(placeholder: "Password", text: $viewModel.password)
    }

    private var loginButton: some View {
        GenericActionButton(label: "Log In") {
            Task {
                await viewModel.logInAction()
            }
        }
        .disabled(viewModel.loginIsDisabled)
    }

    @ViewBuilder
    private var errorMessage: some View {
        if let errorMessage = viewModel.errorMessage {
            Text(errorMessage)
                .foregroundStyle(.red)
                .font(.system(.headline, design: .rounded))
        }
    }

    private var registerButton: some View {
        Button {
            viewModel.pushRegister()
        } label: {
            Text("Don't have an account? Register instead.")
                .foregroundColor(Color.accentColor)
                .font(.system(.headline, design: .rounded))
        }
    }
}
