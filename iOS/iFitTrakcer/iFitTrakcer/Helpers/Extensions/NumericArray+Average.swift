//
//  NumericArray+Average.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 27.11.23.
//

import Foundation

extension Collection where Element: Numeric {
    func average() -> Double? {
        guard isEmpty == false,
              let sum = reduce(0, +) as? Double else {
            return nil
        }

        return sum / Double(count)
    }

    func weeklyAverage() -> Double? {
        guard isEmpty == false,
              let sum = reduce(0, +) as? Double else {
            return nil
        }

        return sum / Double(7)
    }
}
