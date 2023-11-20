//
//  TabDestination.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import Foundation
import SwiftUI

enum TabDestination: String, CaseIterable, Hashable {
    case health
    case activity
    case profile

    func label(isSelected: Bool) -> some View {
        return Label {
            Text(title)
        } icon: {
            icon(isSelected: isSelected)
        }
    }

    private var title: String {
        switch self {
        case .health:
            return "Health"
        case .activity:
            return "Activity"
        case .profile:
            return "Profile"
        }
    }

    private func icon(isSelected: Bool) -> some View {
        switch self {
        case .health:
            return Image(systemName: isSelected ?
                         Icon.healthTabSelected.rawValue :
                            Icon.healthTabUnselected.rawValue)
        case .activity:
            return Image(systemName: isSelected ?
                         Icon.activityTabSelected.rawValue :
                            Icon.activityTabSelected.rawValue)
        case .profile:
            return Image(systemName: isSelected ?
                         Icon.profileTabSelected.rawValue :
                            Icon.profileTabUnselected.rawValue)
        }
    }
}
