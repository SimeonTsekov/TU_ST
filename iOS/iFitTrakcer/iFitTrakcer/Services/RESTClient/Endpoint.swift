//
//  Endpoint.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 15.12.23.
//

import Foundation

protocol Endpoint: Encodable {
    associatedtype ResponseModel: Decodable

    var path: String { get }
    var httpMethod: HTTPMethod { get }
    var queryItems: [URLQueryItem]? { get }
//    var httpHeaders: [HTTPHeader] { get }
}

extension Endpoint {
    func request(relativeTo url: URL) -> URLRequest? {
        let extendedUrl = url.appending(path: path)
        var request = URLRequest(url: extendedUrl)

        request.httpMethod = httpMethod.rawValue

        if httpMethod == .POST {
            if let data = try? JSONEncoder().encode(self),
               let jsonString = String(data: data, encoding: .utf8) {
                request.setValue("application/json", forHTTPHeaderField: "Content-Type")
                request.httpBody = jsonString.data(using: .utf8)
            }
        }

        return request
    }
}

protocol GETEndpoint: Endpoint {}

extension GETEndpoint {
    var httpMethod: HTTPMethod { HTTPMethod.GET }
    var queryItems: [URLQueryItem]? { nil }
}

protocol POSTEndpoint: Endpoint {}

extension POSTEndpoint {
    var httpMethod: HTTPMethod { HTTPMethod.POST }
    var queryItems: [URLQueryItem]? { nil }
}

public enum HTTPMethod: String {
    case GET
    case POST
    case DELETE
}

public struct HTTPHeader {
    public enum Key: String {
        case authorization = "Authorization"
    }

    let key: HTTPHeader.Key
    let value: String

    public init(key: HTTPHeader.Key, value: String) {
        self.key = key
        self.value = value
    }
}
