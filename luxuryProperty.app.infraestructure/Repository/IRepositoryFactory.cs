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

namespace luxuryProperty.app.infraestructure.Repository
{
    /// <summary>
    /// The repository factory.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>
        /// The repository.
        /// </returns>
        IRepositoryData<TEntity> CreateRepository<TEntity>()
            where TEntity : class;
    }
}
