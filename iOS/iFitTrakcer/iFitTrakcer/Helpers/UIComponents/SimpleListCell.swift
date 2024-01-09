//
//  SimpleListCell.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 26.11.23.
//

import SwiftUI

struct SimpleListCell: View {
    @Environment(\.colorScheme) var colorScheme

    var title: String
    var value: String

    var body: some View {
        HStack {
            cellContent
            Spacer()
        }
        .frame(maxWidth: .infinity)
    }

    var cellContent: some View {
        VStack(alignment: .leading, spacing: 8) {
            Text(title)
                .foregroundStyle(Color.accentColor)
                .font(.system(.headline, design: .rounded))
            Text(value)
                .font(.system(.body, design: .rounded))
        }
    }
}
