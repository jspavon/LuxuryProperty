// ***********************************************************************
// Assembly         : luxuryProperty
// Author           : Jhon Steven Pavon Bedoya 
// Created          : 26-01-2025
//
// Last Modified By : Jhon Steven Pavon Bedoya
// Last Modified On : 26-01-2025
// ***********************************************************************
// <copyright file="ResponseException.cs" company="luxuryProperty">
//     Copyright (c) Independiente. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace luxuryProperty.app.infraestructure.Context
{
    /// <summary>
    /// The connection factory to host connection details.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Gets a value indicating whether this instance is ready.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ready; otherwise, <c>false</c>.
        /// </value>
        bool IsReady { get; }

        /// <summary>
        /// Gets the SQL connection configuration.
        /// </summary>
        /// <value>
        /// The SQL connection configuration.
        /// </value>
        string SqlConnectionConfig { get; }

        /// <summary>
        /// Gets the no SQL connection string.
        /// </summary>
        /// <value>
        /// The no SQL connection string.
        /// </value>
        string NoSqlConnectionString { get; }

        /// <summary>
        /// Initializes the specified storage connection string.
        /// </summary>
        /// <param name="storageConnectionString">The storage connection string.</param>
        void SetupStorageConnection(string storageConnectionString);

        /// <summary>
        /// Setups the SQL configuration.
        /// </summary>
        /// <param name="sqlConnectionConfig">The SQL connection configuration.</param>
        void SetupSqlConfig(string sqlConnectionConfig);
    }
}
