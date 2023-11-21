//
//  ActivityRouter.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import Foundation

class ActivityRouter: ObservableObject {
    @Published var path = [ActivityDestination]()
}
