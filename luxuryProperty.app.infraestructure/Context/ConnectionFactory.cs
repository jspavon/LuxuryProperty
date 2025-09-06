// ***********************************************************************
// Assembly         : luxuryProperty.app.commons
// Author           : Jhon Steven Pavon Bedoya 
// Created          : 26-01-2025
//
// Last Modified By : Jhon Steven Pavon Bedoya
// Last Modified On : 26-01-2025
// ***********************************************************************
// <copyright file="ResponseException.cs" company="luxuryProperty.app.commons">
//     Copyright (c) Independiente. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using luxuryProperty.app.commons.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace luxuryProperty.app.infraestructure.Context.Interfaces
{
    /// <summary>
    /// The connection factory.
    /// </summary>
    /// <seealso cref="Ecp.True.DataAccess.Interfaces.IConnectionFactory" />
    [IoCRegistration(IoCLifetime.ContainerControlled)]
    public class ConnectionFactory : IConnectionFactory
    {
        /// <summary>
        /// The connection configuration.
        /// </summary>
        private IDictionary<string, object> connectionConfiguration;

        /// <inheritdoc/>
        public string SqlConnectionConfig => this.GetValue<string>(nameof(this.SqlConnectionConfig));

        /// <inheritdoc/>
        public string NoSqlConnectionString => this.GetValue<string>(nameof(this.NoSqlConnectionString));

        /// <inheritdoc/>
        public bool IsReady => this.connectionConfiguration != null;

        /// <inheritdoc/>
        public void SetupSqlConfig(string sqlConnectionConfig)
        {
            this.connectionConfiguration ??= new Dictionary<string, object>();

            var key = nameof(this.SqlConnectionConfig);
            if (this.connectionConfiguration.ContainsKey(key))
            {
                this.connectionConfiguration[key] = sqlConnectionConfig;
            }
            else
            {
                this.connectionConfiguration.Add(key, sqlConnectionConfig);
            }
        }

        /// <inheritdoc/>
        public void SetupStorageConnection(string storageConnectionString)
        {
            this.connectionConfiguration ??= new Dictionary<string, object>();

            var key = nameof(this.NoSqlConnectionString);
            if (this.connectionConfiguration.ContainsKey(key))
            {
                this.connectionConfiguration[key] = storageConnectionString;
            }
            else
            {
                this.connectionConfiguration.Add(key, storageConnectionString);
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The configuration value.</returns>
        private TValue GetValue<TValue>(string key)
        {
            if (this.connectionConfiguration != null && this.connectionConfiguration.ContainsKey(key))
            {
                return (TValue)this.connectionConfiguration[key];
            }

            return default(TValue);
        }
    }
}
