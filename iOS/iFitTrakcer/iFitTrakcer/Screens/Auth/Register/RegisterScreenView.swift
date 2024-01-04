//
//  RegisterScreenView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 12.12.23.
//

import Foundation
import SwiftUI

struct RegisterScreenView: View {
    @EnvironmentObject private var profileRouter: ProfileRouter
    @StateObject var viewModel: RegisterViewModel

    var body: some View {
        VStack(alignment: .center, spacing: 16) {
            titleText
            usernameInputField
            emailInputField
            passwordInputField
            confirmPasswordInputField
            errorMessage

            if viewModel.isRegistering {
                ProgressView()
            } else {
                registerButton
            }
        }
        .padding([.leading, .trailing], 16)
    }

    private var titleText: some View {
        Text("Register")
            .font(.system(.largeTitle, design: .rounded))
            .fontWeight(.bold)
    }

    private var usernameInputField: some View {
        GenericTextField(placeholder: "Username", text: $viewModel.username)
    }

    private var emailInputField: some View {
        GenericTextField(placeholder: "Email", text: $viewModel.email)
    }

    private var passwordInputField: some View {
        GenericSecureField(placeholder: "Password", text: $viewModel.password)
    }

    private var confirmPasswordInputField: some View {
        GenericSecureField(placeholder: "Confirm Password", text: $viewModel.confirmPassword)
    }

    @ViewBuilder
    private var errorMessage: some View {
        if let error = viewModel.errorMessage {
            Text(error)
                .foregroundColor(.red)
                .font(.system(.caption, design: .rounded))
        }
    }

    private var registerButton: some View {
        GenericActionButton(label: "Register") {
            Task {
                if viewModel.validateFields() {
                    await viewModel.registerAction()
                }
            }
        }
        .disabled(viewModel.registerIsDisabled)
    }
}
