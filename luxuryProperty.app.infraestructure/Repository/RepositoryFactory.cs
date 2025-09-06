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

using luxuryProperty.app.infraestructure.Context;
using luxuryProperty.app.infraestructure.Context.Interfaces;


namespace luxuryProperty.app.infraestructure.Repository
{
    /// <summary>
    /// The repository factory.
    /// </summary>
    /// <seealso cref="Ecp.True.DataAccess.Interfaces.IRepositoryFactory" />
    public class RepositoryFactory : IRepositoryFactory
    {
        /// <summary>
        /// The data context.
        /// </summary>
        private readonly IMongoDataContext dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryFactory" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="businessContext">The business context.</param>
        public RepositoryFactory(IMongoDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        /// <inheritdoc/>
        public IRepositoryData<TEntity> CreateRepository<TEntity>()
            where TEntity : class
        {
            return new RepositoryData<TEntity>(this.dataContext);
        }
    }
}
