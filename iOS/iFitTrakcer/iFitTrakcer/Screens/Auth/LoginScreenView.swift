//
//  LoginScreenView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 12.12.23.
//

import Foundation
import SwiftUI

struct LoginScreenView: View {
    @EnvironmentObject private var profileRouter: ProfileRouter
    @State private var username = ""
    @State private var password = ""

    var body: some View {
        VStack(alignment: .center, spacing: 16) {
            titleText
            usernameInputField
            passwordInputField
            loginButton
        }
        .padding([.leading, .trailing], 16)
    }

    private var titleText: some View {
        Text("Login")
            .font(.system(.largeTitle, design: .rounded))
            .fontWeight(.bold)
    }

    private var usernameInputField: some View {
        TextField("Username", text: $username)
            .padding()
            .background(Color.gray.opacity(0.1))
            .cornerRadius(8)
    }

    private var passwordInputField: some View {
        SecureField("Password", text: $password)
            .padding()
            .background(Color.gray.opacity(0.1))
            .cornerRadius(8)
    }

    private var loginButton: some View {
        Button {
            print("Username: \(username), Password: \(password)")
        } label: {
            Text("Log In")
                .padding()
                .foregroundColor(.white)
                .frame(maxWidth: .infinity)
                .background(Color.accentColor)
                .font(.system(.headline, design: .rounded))
                .fontWeight(.bold)
                .cornerRadius(8)
        }
    }
}
