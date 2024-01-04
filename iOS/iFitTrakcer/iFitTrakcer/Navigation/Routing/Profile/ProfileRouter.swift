//
//  ProfileRouter.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 20.11.23.
//

import Foundation
import SwiftUI

protocol ProfileRouting {
    func pushLogin(with user: User)
    func pushRegister()
}

class ProfileRouter: ProfileRouting, ObservableObject {
    @Published var path = [ProfileDestination]()

    func pushLogin(with user: User) {
        path.append(.login(user, self))
    }

    func pushRegister() {
        path.append(.register)
    }

    func popBack() {
        path.removeLast()
    }
}
