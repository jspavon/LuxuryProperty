// ***********************************************************************
// Assembly         : luxuryProperty.app.infraestructure
// Author           : Jhon Steven Pavon Bedoya 
// Created          : 26-01-2025
//
// Last Modified By : Jhon Steven Pavon Bedoya
// Last Modified On : 26-01-2025
// ***********************************************************************
// <copyright file="MongoDataContext.cs" company="luxuryProperty.app.infraestructure">
//     Copyright (c) Independiente. All rights reserved.
// </copyright>
// <summary>Contexto de datos para MongoDB</summary>
// ***********************************************************************

using luxuryProperty.app.commons.Helpers;
using luxuryProperty.app.infraestructure.Context.Interfaces;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;


namespace luxuryProperty.app.infraestructure.Context
{
    [ExcludeFromCodeCoverage]
    [IoCRegistration(IoCLifetime.Singleton)] // MongoDB funciona mejor como Singleton
    public class MongoDataContext : IMongoDataContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoClient _client;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDataContext"/> class.
        /// </summary>
        /// <param name="configuration">Configuración de la aplicación</param>
        public MongoDataContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDbContext");
            var databaseName = configuration["MongoDbSettings:DatabaseName"]
                             ?? "luxuryPropertyDB"; // nombre por defecto

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }

        /// <summary>
        /// Obtiene una colección de MongoDB
        /// </summary>
        /// <typeparam name="TEntity">Tipo de la entidad</typeparam>
        /// <param name="name">Nombre de la colección (opcional)</param>
        /// <returns>Colección MongoDB</returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>(string name = null) where TEntity : class
        {
            return _database.GetCollection<TEntity>(name ?? typeof(TEntity).Name.ToLower() + "s");
        }

        public async Task<IClientSessionHandle> StartSessionAsync()
        {
            return await _client.StartSessionAsync();
        }

        public async Task CommitTransactionAsync(IClientSessionHandle session)
        {
            if (session != null && session.IsInTransaction)
            {
                await session.CommitTransactionAsync();
            }
        }

        public async Task AbortTransactionAsync(IClientSessionHandle session)
        {
            if (session != null && session.IsInTransaction)
            {
                await session.AbortTransactionAsync();
            }
        }

        public void Dispose()
        {
            // ✅ Ya no hay sesión global que dispose
        }

    }
}