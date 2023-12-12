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
    @State private var username = ""
    @State private var email = ""
    @State private var password = ""
    @State private var confirmPassword = ""

    var body: some View {
        VStack(alignment: .center, spacing: 16) {
            titleText
            usernameInputField
            emailInputField
            passwordInputField
            confirmPasswordInputField
            registerButton
        }
        .padding([.leading, .trailing], 16)
    }

    private var titleText: some View {
        Text("Register")
            .font(.system(.largeTitle, design: .rounded))
            .fontWeight(.bold)
    }

    private var usernameInputField: some View {
        GenericTextField(placeholder: "Username", text: $username)
    }

    private var emailInputField: some View {
        GenericTextField(placeholder: "Email", text: $email)
    }

    private var passwordInputField: some View {
        GenericSecureField(placeholder: "Password", text: $password)
    }

    private var confirmPasswordInputField: some View {
        GenericSecureField(placeholder: "Confirm Password", text: $confirmPassword)
    }

    private var registerButton: some View {
        GenericActionButton(label: "Register") {
            return
        }
    }
}
