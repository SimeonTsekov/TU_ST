//
//  UserDataModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 3.12.23.
//

import Foundation

class UserDataModel: ObservableObject {
    @Published var biologicalSex: Sex = .unidentified
    @Published var height: Double = 0
    @Published var dateOfBirth: Date = Date.now
}
