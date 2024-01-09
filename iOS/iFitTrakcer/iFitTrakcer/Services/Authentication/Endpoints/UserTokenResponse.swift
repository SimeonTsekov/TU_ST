//
//  UserTokenResponse.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 9.01.24.
//

import Foundation

struct User: Decodable {
    let id: Int
    let username: String
    let email: String
    let password: String
    let age: Int
    let height: Int
    let sex: Sex
    let createdDate: String
}

struct UserTokenResponse: Decodable {
    let accessToken: String
    let user: User
}
