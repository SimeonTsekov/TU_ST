//
//  UserActivityModel.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 1.12.23.
//

import Foundation
import HealthKit

class UserActivityhModel: ObservableObject {
    @Published var workoutEntries: [HKWorkout] = []
    @Published var dailyStepsEntries: [Double] = []
    @Published var dailyDistanceEntries: [Double] = []
    @Published var dailyEnergyExpenditureEntries: [Double] = []
}
