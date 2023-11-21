//
//  TabRouter.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import Combine
import Foundation
import SwiftUI

class TabRouter: ObservableObject {
    @Published var currentTab: TabDestination = .health

    let destinations = TabDestination.allCases
}
