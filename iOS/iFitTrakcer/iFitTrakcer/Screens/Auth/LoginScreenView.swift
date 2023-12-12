//
//  LoginScreenView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 12.12.23.
//

import Foundation
import SwiftUI

struct LoginScreenView: View {
    @EnvironmentObject private var router: ProfileRouter
    @State private var username = ""
    @State private var password = ""

    var loginIsDisabled: Bool {
        username.isEmpty || password.isEmpty
    }

    var body: some View {
        VStack(alignment: .center, spacing: 16) {
            titleText
            usernameInputField
            passwordInputField
            loginButton
            registerButton
        }
        .padding([.leading, .trailing], 16)
    }

    private var titleText: some View {
        Text("Login")
            .font(.system(.largeTitle, design: .rounded))
            .fontWeight(.bold)
    }

    private var usernameInputField: some View {
        GenericTextField(placeholder: "Username", text: $username)
    }

    private var passwordInputField: some View {
        GenericSecureField(placeholder: "Password", text: $password)
    }

    private var loginButton: some View {
        GenericActionButton(label: "Log In") {
            return
        }
        .disabled(loginIsDisabled)
    }

    private var registerButton: some View {
        Button {
            router.pushRegister()
        } label: {
            Text("Don't have an account? Register instead.")
                .foregroundColor(Color.accentColor)
                .font(.system(.headline, design: .rounded))
        }
    }
}
