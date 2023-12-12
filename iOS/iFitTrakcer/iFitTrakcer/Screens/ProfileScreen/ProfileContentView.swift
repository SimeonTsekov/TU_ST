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
    @StateObject var viewModel: ProfileViewModel
    let router: ProfileRouting

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
            dateOfBirthPicker
            sexPicker
            heightPicker
//            weightPicker
        }
    }

    private var authButton: some View {
        Button {
            router.pushLogin()
        } label: {
            Text("Log In")
                .foregroundColor(Color.accentColor)
                .font(.system(.headline, design: .rounded))
        }
    }

    private var dateOfBirthPicker: some View {
        DatePicker("Date of Birth",
                   selection: $viewModel.userData.dateOfBirth,
                   displayedComponents: .date)
    }

    private var sexPicker: some View {
        Picker(selection: $viewModel.userData.biologicalSex,
               label: Text("Sex").font(.system(.body, design: .rounded))) {
            ForEach(Sex.allCases, id: \.self) {
                Text($0.rawValue)
            }
        }
    }

    private var heightPicker: some View {
        Picker(selection: $viewModel.userData.height,
               label: Text("Height").font(.system(.body, design: .rounded))) {
            ForEach(0..<300, id: \.self) {
                Text("\($0) cm")
            }
        }
    }
}
