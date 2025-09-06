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

using luxuryProperty.app.infraestructure.Context.Interfaces;
using luxuryProperty.app.infraestructure.Repository;
using MongoDB.Driver;

namespace luxuryProperty.app.infraestructure.UnitOfWork
{
    /// <summary>
    /// Class UnitOfWork.
    /// Implements the <see cref="IUnitOfWork" />
    /// Implements the <see cref="IUnitOfWork" />
    /// Implements the <see cref="luxuryProperty.app" />
    /// </summary>
    /// <seealso cref="luxuryProperty.app" />
    /// <seealso cref="IUnitOfWork" />
    /// <seealso cref="IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {

        #region Attributes

        /// <summary>
        /// The context
        /// </summary>
        private readonly IMongoDataContext _mongoDataContext;

        /// <summary>
        /// The repository factory.
        /// </summary>
        private readonly IRepositoryFactory _repositoryFactory;

        /// <summary>
        /// The Client session handler
        /// </summary>
        private IClientSessionHandle _session;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        public UnitOfWork(IMongoDataContext mongoDataContext, IRepositoryFactory repositoryFactory)
        {
            _mongoDataContext = mongoDataContext;
            _repositoryFactory = repositoryFactory;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Crea un repositorio para la entidad especificada
        /// </summary>
        public IRepositoryData<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            return _repositoryFactory.CreateRepository<TEntity>();
        }




        /// <summary>
        /// Obtiene una colección de MongoDB
        /// </summary>
        public IMongoCollection<TEntity> GetCollection<TEntity>(string name = null) where TEntity : class
        {
            return _mongoDataContext.GetCollection<TEntity>(name);
        }

        public async Task BeginTransaction()
        {
            _session = await _mongoDataContext.StartSessionAsync();
            _session.StartTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            await _mongoDataContext.CommitTransactionAsync(_session);
            _session?.Dispose();
            _session = null;
        }

        public async Task AbortTransactionAsync()
        {
            await _mongoDataContext.AbortTransactionAsync(_session);
            _session?.Dispose();
            _session = null;
        }


        /// <summary>
        /// Libera recursos del contexto
        /// </summary>
        public void Dispose()
        {
            _session?.Dispose();
            _mongoDataContext?.Dispose();
        }
        #endregion
    }
}
