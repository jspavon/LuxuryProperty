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

using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;


namespace luxuryProperty.app.commons.proxy.Configuration
{
    /// <summary>
    /// Class Extensions.
    /// </summary>
    /// <remarks>Jhon Steven Pavón Bedoya</remarks>
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {
        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <typeparam name="TModel">The type of the t model.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <param name="section">The section.</param>
        /// <returns>TModel.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
    }
}
