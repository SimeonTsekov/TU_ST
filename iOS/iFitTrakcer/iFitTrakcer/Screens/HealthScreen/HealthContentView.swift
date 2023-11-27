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
        Section(header: Text("7-Day Summary")) {
            if let weeklyAverageWeight = viewModel.bodyMassEntries.average() {
                SimpleListCell(title: "Body Mass", value: String(format: "%.2f", weeklyAverageWeight) + " kg")
            }
            if let weeklyAverageBMI = viewModel.bmiEntries.average() {
                SimpleListCell(title: "BMI", value: String(format: "%.2f", weeklyAverageBMI))
            }
            if let weeklyAveragebodyFat = viewModel.bodyFatEntries.average() {
                SimpleListCell(title: "Body Fat", value: String(format: "%.2f", weeklyAveragebodyFat * 100) + "%")
            }
            if let weeklyAverageLeanMass = viewModel.leanBodyMassEntries.average() {
                SimpleListCell(title: "Lean Mass", value: String(format: "%.2f", weeklyAverageLeanMass) + " kg")
            }
            SimpleListCell(title: "Sleep Time", value: "6 hours")
        }
    }
}
