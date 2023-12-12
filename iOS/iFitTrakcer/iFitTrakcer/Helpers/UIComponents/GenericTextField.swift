//
//  GenericTextField.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 12.12.23.
//

import SwiftUI

struct GenericTextField: View {
    var placeholder: String
    var text: Binding<String>

    var body: some View {
        TextField(placeholder, text: text)
            .padding()
            .background(Color.gray.opacity(0.1))
            .cornerRadius(8)
    }
}
