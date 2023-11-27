//
//  UserHealthModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import Foundation

class UserHealthModel: ObservableObject {
    @Published var bodyMassEntries: [Double] = []
    @Published var bmiEntries: [Double] = []
    @Published var bodyFatEntries: [Double] = []
    @Published var leanBodyMassEntries: [Double] = []
}
