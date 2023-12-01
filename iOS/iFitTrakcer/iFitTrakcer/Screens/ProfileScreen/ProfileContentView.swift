//
//  ProfileContentView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

enum Sex: String, CaseIterable {
    case male = "Male"
    case female = "Female"
    case unidentified = "Unidentified"
}

struct ProfileContentView: View {
    let router: ProfileRouter

    @State private var sex: Sex = .unidentified
    @State private var height: Int = 0
    @State private var weight: Int = 0

    var body: some View {
        List {
            accountSection
            dataSection
        }
    }

    private var accountSection: some View {
        Section(header: Text("Account")) {
            authButton
        }
    }

    private var dataSection: some View {
        Section(header: Text("Data")) {
            sexPicker
            heightPicker
            weightPicker
        }
    }

    private var authButton: some View {
        Button {
            print("Auth button tapped")
        } label: {
            Text("Login")
                .foregroundColor(Color.accentColor)
                .font(.system(.headline, design: .rounded))
        }
    }

    private var sexPicker: some View {
        Picker(selection: $sex, label: Text("Sex").font(.system(.body, design: .rounded))) {
            ForEach(Sex.allCases, id: \.self) {
                Text($0.rawValue)
            }
        }
    }

    private var heightPicker: some View {
        Picker(selection: $height, label: Text("Height").font(.system(.body, design: .rounded))) {
            ForEach(0..<300, id: \.self) {
                Text("\($0) cm")
            }
        }
    }

    private var weightPicker: some View {
        Picker(selection: $weight, label: Text("Weight").font(.system(.body, design: .rounded))) {
            ForEach(0..<300, id: \.self) {
                Text("\($0) kg")
            }
        }
    }
}
