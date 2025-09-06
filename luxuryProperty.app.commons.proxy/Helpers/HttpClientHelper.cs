// ***********************************************************************
// Assembly         : luxuryProperty.app
// Author           : Jhon Steven Pavon Bedoya 
// Created          : 26-01-2025
//
// Last Modified By : Jhon Steven Pavon Bedoya
// Last Modified On : 26-01-2025
// ***********************************************************************
// <copyright file="ResponseException.cs" company="luxuryProperty.app">
//     Copyright (c) Independiente. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Net;
using Newtonsoft.Json;


namespace luxuryProperty.app.commons.proxy.Helpers
{
    /// <summary>
    /// Class HttpClientHelper.
    /// Implements the <see cref="luxuryProperty.app" />
    /// </summary>
    /// <seealso cref="luxuryProperty.app" />
    /// <remarks>Jhon Steven Pavón Bedoya</remarks>
    [ExcludeFromCodeCoverage]
    public class HttpClientHelper : IHttpClientHelper
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientHelper" /> class.
        /// </summary>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public HttpClientHelper()
        {
            _client = new HttpClient();
        }

        /// <summary>
        /// get string as an asynchronous operation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public async Task<string> GetStringAsync(string uri, IDictionary<string, string> headers, string token)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            _client.DefaultRequestHeaders.Accept.Clear();

            if (headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            var response = await _client.SendAsync(requestMessage);

            return await response.Content.ReadAsStringAsync();
        }


        /// <summary>
        /// post put as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="method">The method.</param>
        /// <param name="item">The item.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public async Task<HttpResponseMessage> PostPutAsync<T>(string uri, IDictionary<string, string> headers, HttpMethod method, T item, string token = null)
        {
            return await DoPostPutAsync(method, uri, headers, item, token);
        }

        /// <summary>
        /// delete as an asynchronous operation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="method">The method.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public async Task<HttpResponseMessage> DeleteAsync(string uri, IDictionary<string, string> headers, HttpMethod method, string token = null)
        {
            return await DoDeleteAsync(uri, headers, method, token);
        }

        public async Task<byte[]> GetByteArrayAsync(string uri, IDictionary<string, string> headers, string token)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            _client.DefaultRequestHeaders.Accept.Clear();

            if (headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            var response = await _client.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            return null;
        }

        #region Method Private

        /// <summary>
        /// do post put as an asynchronous operation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method">The method.</param>
        /// <param name="uri">The URI.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="item">The item.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="ArgumentException">Value must be either post or put. - method</exception>
        /// <exception cref="HttpRequestException"></exception>
        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, IDictionary<string, string> headers, T item, string token = null)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            var requestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, MediaTypeNames.Application.Json)
            };

            _client.DefaultRequestHeaders.Accept.Clear();

            if (headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var response = await _client.SendAsync(requestMessage);

            // raise exception if HttpResponseCode 500
            // needed for circuit breaker to track fails

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

        /// <summary>
        /// do delete as an asynchronous operation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="headers">The headers.</param>
        /// <param name="method">The method.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="HttpRequestException"></exception>
        private async Task<HttpResponseMessage> DoDeleteAsync(string uri, IDictionary<string, string> headers, HttpMethod method, string token = null)
        {
            var requestMessage = new HttpRequestMessage(method, uri);

            _client.DefaultRequestHeaders.Accept.Clear();

            if (headers != null && headers.Any())
            {
                foreach (var header in headers)
                {
                    _client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }

        #endregion Method Private
    }
}
