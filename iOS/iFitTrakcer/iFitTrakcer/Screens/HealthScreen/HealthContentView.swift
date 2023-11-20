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
        Text("Health Screen")
    }
}

#Preview {
    HealthContentView(router: HealthRouter())
}
