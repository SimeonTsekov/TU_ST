//
//  RESTClient.swift
//  iFitTrakcer
//
//  Created by Simeon Tsekov on 15.12.23.
//

import Foundation

class RESTClient {
    private var baseURL: URL
    private let session: URLSession

    init(basePath: String,
         timeoutInterval: TimeInterval = 60) {
        guard let url = URL(string: basePath) else {
            fatalError("Couldn't create base url")
        }

        baseURL = url
        session = URLSession(configuration: .default)
    }

    func call<T: Endpoint>(_ endpoint: T,
                           authorization token: String? = nil,
                           completion completionHandler: @escaping (Result<T.ResponseModel, Error>) -> Void) {
        guard let request = endpoint.request(relativeTo: baseURL, authorization: token) else {
            completionHandler(.failure(NSError(domain: baseURL.absoluteString,
                                               code: -1,
                                               userInfo: ["Message": "Couldn't create URLRequest"])))
            return
        }

        session.dataTask(with: request) { [weak self] data, response, error in
            guard let self = self else {
                completionHandler(.failure(NSError(domain: "",
                                                   code: -1111,
                                                   userInfo: ["Message": "self is nil"])))
                return
            }

            switch self.validateResponse(data: data, response: response, error: error) {
            case let .failure(error):
                completionHandler(.failure(error))
            case let .success(data):
                do {
                    let result = try JSONDecoder().decode(T.ResponseModel.self, from: data)
                    completionHandler(.success(result))
                } catch {
                    completionHandler(.failure(error))
                }
            }
        }
        .resume()
    }

    private func validateResponse(data: Data?,
                                  response: URLResponse?,
                                  error: Error?) -> Result<Data, Error> {
        guard let httpResponse = response as? HTTPURLResponse else {
            return .failure(NSError(domain: baseURL.absoluteString,
                                    code: 400,
                                    userInfo: ["Message": "Response is not HTTPURLResponse"]))
        }

        guard 200...399 ~= httpResponse.statusCode else {
            return .failure(NSError(domain: baseURL.absoluteString,
                                    code: httpResponse.statusCode,
                                    userInfo: ["Message": "Server Error"]))
        }

        guard let data = data else {
            return .failure(NSError(domain: baseURL.absoluteString,
                                    code: httpResponse.statusCode,
                                    userInfo: ["Message": "Missing Data"]))
        }
        return .success(data)
    }
}
