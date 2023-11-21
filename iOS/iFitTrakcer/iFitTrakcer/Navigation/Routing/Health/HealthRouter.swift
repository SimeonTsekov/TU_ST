//
//  HealthRouter.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import Foundation

class HealthRouter: ObservableObject {
    @Published var path = [HealthDestination]()
}
