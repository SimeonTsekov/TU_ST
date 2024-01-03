//
//  GenericSecureField.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 12.12.23.
//

import SwiftUI

struct GenericSecureField: View {
    @State private var isSecure: Bool = true

    var placeholder: String
    var text: Binding<String>

    var body: some View {
        HStack {
            textField
            visibilityButton
        }
        .padding([.leading, .trailing], 16)
        .frame(minHeight: 48)
        .background(Color.gray.opacity(0.1))
        .cornerRadius(8)
    }

    @ViewBuilder
    private var textField: some View {
        if isSecure {
            SecureField(placeholder, text: text)
        } else {
            TextField(placeholder, text: text)
        }
    }

    private var visibilityButton: some View {
        Button {
            isSecure.toggle()
        } label: {
            Image(systemName: isSecure ? "eye.slash" : "eye")
                .foregroundColor(.accentColor)
        }
    }
}
