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

using luxuryProperty.app.commons.proxy.Helpers;
using Microsoft.Extensions.DependencyInjection;


namespace luxuryProperty.app.commons.proxy.ServiceCollections
{
    /// <summary>
    /// Class Extensions.
    /// </summary>
    /// <remarks>Jhon Steven Pavón Bedoya</remarks>
    public static class Extensions
    {
        /// <summary>
        /// Adds the proxy HTTP.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public static IServiceCollection AddProxyHttp(this IServiceCollection services)
        {
            services.AddSingleton<IHttpClientHelper, HttpClientHelper>();
            return services;
        }
    }
}
