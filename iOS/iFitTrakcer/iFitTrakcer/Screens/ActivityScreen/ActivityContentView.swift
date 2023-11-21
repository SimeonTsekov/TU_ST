//
//  ActivityContentView.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import SwiftUI

struct ActivityContentView: View {
    var router: ActivityRouter

    var body: some View {
        Text("Activity Screen")
    }
}

#Preview {
    ActivityContentView(router: ActivityRouter())
}
