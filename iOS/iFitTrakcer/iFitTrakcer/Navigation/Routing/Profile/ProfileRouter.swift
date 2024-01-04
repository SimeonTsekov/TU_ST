//
//  ProfileRouter.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import Foundation
import SwiftUI

protocol ProfileRouting {
    func pushLogin()
    func pushRegister()
}

class ProfileRouter: ProfileRouting, ObservableObject {
    @Published var path = [ProfileDestination]()

    func pushLogin() {
        path.append(.login(self))
    }

    func pushRegister() {
        path.append(.register(self))
    }

    func popBack() {
        path.removeLast()
    }

    func popAll() {
        path.removeAll()
    }
}
