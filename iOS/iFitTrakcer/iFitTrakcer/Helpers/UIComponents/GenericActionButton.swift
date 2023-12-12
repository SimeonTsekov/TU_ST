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
                .padding([.leading, .trailing], 16)
                .frame(maxWidth: .infinity, minHeight: 48)
                .foregroundColor(.white)
                .background(Color.accentColor)
                .font(.system(.headline, design: .rounded))
                .fontWeight(.bold)
                .cornerRadius(8)
        }
    }
}
