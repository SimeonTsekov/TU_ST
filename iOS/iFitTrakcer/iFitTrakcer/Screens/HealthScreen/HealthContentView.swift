//
//  HealthContentView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct HealthContentView: View {
    @StateObject var viewModel: HealthViewModel
    var router: HealthRouter

    var body: some View {
        List {
            healthSection
        }
    }

    private var healthSection: some View {
        Section(header: Text("Summary")) {
            if let weeklyAverageWeight = viewModel.weeklyAverageWeight {
                SimpleListCell(title: "Body Mass", value: String(format: "%.2f", weeklyAverageWeight))
            }
            SimpleListCell(title: "BMI", value: "25")
            SimpleListCell(title: "Body Fat", value: "25%")
            SimpleListCell(title: "Lean Body Mass", value: "63")
            SimpleListCell(title: "Sleep Time", value: "6 hours")
        }
    }
}
