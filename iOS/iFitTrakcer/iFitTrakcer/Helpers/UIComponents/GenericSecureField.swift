//
//  GenericSecureField.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 12.12.23.
//

import SwiftUI

struct GenericSecureField: View {
    var placeholder: String
    var text: Binding<String>

    var body: some View {
        SecureField(placeholder, text: text)
            .padding()
            .background(Color.gray.opacity(0.1))
            .cornerRadius(8)
    }
}
