//
//  RESTClient.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 15.12.23.
//

import Foundation

class RESTClient {
    private typealias Response = (data: Data, urlResponse: URLResponse)

    private var baseURL: URL
    private let session: URLSession

    init(basePath: String,
         timeoutInterval: TimeInterval = 60) {
        guard let url = URL(string: basePath) else {
            fatalError("Couldn't create base url")
        }

        baseURL = url

        let sessionConfiguration = URLSessionConfiguration.default
        sessionConfiguration.timeoutIntervalForRequest = timeoutInterval
        session = URLSession(configuration: sessionConfiguration)
    }

    func execute<T: Endpoint>(_ endpoint: T,
                              authorization token: String? = nil) async -> Result<T.ResponseModel, Error> {
        let request = endpoint.request(relativeTo: baseURL, authorization: token)

        do {
            let response: Response = try await session.data(for: request)
            let result: Result<T.ResponseModel, Error> = handleResponse(response: response)
            return result
        } catch {
            return .failure(error)
        }
    }

    private func handleResponse<T: Decodable>(response: Response) -> Result<T, Error> {
        let validatedResponse = validateResponse(data: response.data,
                                                 urlResponse: response.urlResponse)

        switch validatedResponse {
        case let .failure(error):
            return .failure(error)
        case let .success(data):
            do {
                let result = try JSONDecoder().decode(T.self, from: data)
                return .success(result)
            } catch {
                return .failure(error)
            }
        }
    }

    private func validateResponse(data: Data,
                                  urlResponse: URLResponse) -> Result<Data, Error> {
        guard let httpResponse = urlResponse as? HTTPURLResponse else {
            return .failure(NSError(domain: baseURL.absoluteString,
                                    code: 400,
                                    userInfo: ["Message": "Response is not HTTPURLResponse"]))
        }

        guard 200...399 ~= httpResponse.statusCode else {
            return .failure(NSError(domain: baseURL.absoluteString,
                                    code: httpResponse.statusCode,
                                    userInfo: ["Message": "Server Error"]))
        }

        return .success(data)
    }
}
