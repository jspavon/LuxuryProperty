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

using luxuryProperty.app.infraestructure.Repository;
using MongoDB.Driver;


namespace luxuryProperty.app.infraestructure.UnitOfWork
{
    /// <summary>
    /// Interface IUnitOfWork
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="System.IDisposable" />

    public interface IUnitOfWork : IDisposable
    {
            /// <summary>
        /// Crea un repositorio para la entidad especificada
        /// </summary>
        IRepositoryData<TEntity> CreateRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Inicia una transacción
        /// </summary>
        Task BeginTransaction();

        /// <summary>
        /// Confirma la transacción actual de forma asíncrona
        /// </summary>
        Task CommitTransactionAsync();

        /// <summary>
        /// Deshace la transacción actual de forma asíncrona
        /// </summary>
        Task AbortTransactionAsync();

        /// <summary>
        /// Obtiene una colección de MongoDB
        /// </summary>
        IMongoCollection<TEntity> GetCollection<TEntity>(string name = null) where TEntity : class;
    }
}
