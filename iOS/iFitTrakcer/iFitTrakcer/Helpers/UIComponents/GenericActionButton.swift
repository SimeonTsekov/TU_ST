//
//  GenericActionButton.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 12.12.23.
//

import SwiftUI

struct GenericActionButton: View {
    let label: String
    let action: () -> Void

    var body: some View {
        Button {
            action()
        } label: {
            Text(label)
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

#Preview {
    GenericActionButton(label: "Register") {
        return
    }
}
