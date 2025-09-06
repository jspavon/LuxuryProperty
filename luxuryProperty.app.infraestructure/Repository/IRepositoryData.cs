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

using System.Linq.Expressions;
using MongoDB.Driver;

namespace luxuryProperty.app.infraestructure.Repository
{
    /// <summary>
    /// Interface IRepository
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    public interface IRepositoryData<TEntity> where TEntity : class
    {
        #region Read Operations
        /// <summary>
        /// Obtiene todos los elementos de forma paginada
        /// </summary>
        Task<(IEnumerable<TEntity> Data, long TotalCount)> GetAllAsync(
            FilterDefinition<TEntity> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            bool ascending = true,
            int page = 1,
            int pageSize = 10);

        /// <summary>
        /// Obtiene un elemento por su ID
        /// </summary>
        Task<TEntity> GetByIdAsync(string id);

        /// <summary>
        /// Obtiene el primer elemento que cumpla con el filtro
        /// </summary>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Obtiene el último elemento que cumpla con el filtro
        /// </summary>
        Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> orderBy = null);

        #endregion

        #region Create Operations

        /// <summary>
        /// Inserta una entidad
        /// </summary>
        Task<bool> InsertAsync(TEntity entity);

        /// <summary>
        /// Inserta múltiples entidades
        /// </summary>
        Task<bool> InsertManyAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Update Operations

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        Task<bool> UpdateAsync(string id, TEntity entity);

        /// <summary>
        /// Actualiza múltiples entidades
        /// </summary>
        Task<bool> UpdateManyAsync(Expression<Func<TEntity, bool>> filter,
            UpdateDefinition<TEntity> updateDefinition);

        #endregion

        #region Delete Operations

        /// <summary>
        /// Eliminado lógico de una entidad
        /// </summary>
        Task<bool> DeleteAsync(string id, bool active = false);

        /// <summary>
        /// Eliminado lógico de múltiples entidades
        /// </summary>
        Task<bool> DeleteManyAsync(Expression<Func<TEntity, bool>> filter, bool active = false);

        /// <summary>
        /// Eliminado físico permanente
        /// </summary>
        Task<bool> PermanentDeleteAsync(string id);

        #endregion

        #region Count Operations

        /// <summary>
        /// Cuenta elementos que cumplan con el filtro
        /// </summary>
        Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        #endregion
    }
}
