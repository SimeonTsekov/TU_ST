//
//  HealthContentView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct HealthContentView: View {
    var router: HealthRouter

    var body: some View {
        List {
            healthSection
        }
    }

    private var healthSection: some View {
        Section(header: Text("Summary")) {
            SimpleListCell(title: "Body Mass", value: "90")
            SimpleListCell(title: "BMI", value: "25")
            SimpleListCell(title: "Body Fat", value: "25%")
            SimpleListCell(title: "Lean Body Mass", value: "63")
            SimpleListCell(title: "Sleep Time", value: "6 hours")
        }
    }
}
