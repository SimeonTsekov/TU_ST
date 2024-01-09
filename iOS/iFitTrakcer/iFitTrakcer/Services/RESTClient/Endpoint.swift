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
    var httpHeaders: [HTTPHeader] { get }
}

extension Endpoint {
    func request(relativeTo url: URL,
                 authorization token: String?) -> URLRequest {
        let extendedUrl = url.appending(path: path)
        var request = URLRequest(url: extendedUrl)

        request.httpMethod = httpMethod.rawValue

        if let token {
            request.addValue("Bearer \(String(describing: token))",
                             forHTTPHeaderField: HTTPHeader.Key.authorization.rawValue)
        }

        for header in httpHeaders {
            request.addValue(header.value, forHTTPHeaderField: header.key.rawValue)
        }

        if httpMethod == .POST {
            if let data = try? JSONEncoder().encode(self),
               let jsonString = String(data: data, encoding: .utf8) {
                request.httpBody = jsonString.data(using: .utf8)
            }
        }

        return request
    }
}

protocol GETEndpoint: Endpoint {}

extension GETEndpoint {
    var contentType: HTTPHeader.ContentType {
        .json
    }

    var httpMethod: HTTPMethod {
        HTTPMethod.GET
    }

    var httpHeaders: [HTTPHeader] {
        [HTTPHeader(key: .contentType, value: contentType.rawValue)]
    }
}

protocol POSTEndpoint: Endpoint {}

extension POSTEndpoint {
    var contentType: HTTPHeader.ContentType {
        .json
    }

    var httpMethod: HTTPMethod {
        HTTPMethod.POST
    }

    var httpHeaders: [HTTPHeader] {
        [HTTPHeader(key: .contentType, value: contentType.rawValue)]
    }
}

public enum HTTPMethod: String {
    case GET
    case POST
    case DELETE
}

public struct HTTPHeader {
    public enum Key: String {
        case contentType = "Content-Type"
        case authorization = "Authorization"
    }

    public enum ContentType: String {
        case all = "*/*"
        case json = "application/json"
    }

    let key: HTTPHeader.Key
    let value: String

    public init(key: HTTPHeader.Key, value: String) {
        self.key = key
        self.value = value
    }
}
