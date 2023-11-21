//
//  ActivityDestination.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import Foundation

enum ActivityDestination {
}

extension ActivityDestination: Hashable {
    func hash(into hasher: inout Hasher) {
        hasher.combine(self.hashValue)
    }

    static func == (lhs: ActivityDestination, rhs: ActivityDestination) -> Bool {
        return true
    }
}
