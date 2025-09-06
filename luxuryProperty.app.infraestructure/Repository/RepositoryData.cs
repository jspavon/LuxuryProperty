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

using luxuryProperty.app.infraestructure.Context.Interfaces;
using luxuryProperty.app.infraestructure.Pagination;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace luxuryProperty.app.infraestructure.Repository
{
    /// <summary>
    /// Class Repository.
    /// Implements the <see cref="IRepository{TEntity}" />
    /// Implements the <see cref="luxuryProperty.app" />
    /// </summary>
    /// <typeparam name="TEntity">The type of the t entity.</typeparam>
    /// <seealso cref="luxuryProperty.app" />
    /// <seealso cref="IRepository{TEntity}" />

    [ExcludeFromCodeCoverage]
    public class RepositoryData<TEntity> : IRepositoryData<TEntity> where TEntity : class
    {
        /// <summary>
        /// The dbcontext
        /// </summary>
        private readonly IMongoDataContext _mongoDataContext;
        private readonly IMongoCollection<TEntity> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}" /> class.
        /// </summary>
        /// <param name="dbcontext">The dbcontext.</param>
        /// <param name="unitOfWork">The unit of work.</param>       
        public RepositoryData(IMongoDataContext mongoDataContext)
        {
            _mongoDataContext = mongoDataContext;
            _collection = mongoDataContext.GetCollection<TEntity>();
        }

        #region IRepository<TEntity> Members

        #region Read Operations

        public async Task<(IEnumerable<TEntity> Data, long TotalCount)> GetAllAsync(
            FilterDefinition<TEntity> filter = null,
            Expression<Func<TEntity, object>> orderBy = null,
            bool ascending = true,
            int page = 1,
            int pageSize = 10)
        {
            try
            {
                // Crear filtro base
                var baseFilter = filter ?? Builders<TEntity>.Filter.Empty;

                // Obtener conteo total
                var totalCount = await _collection.CountDocumentsAsync(baseFilter);

                // Configurar ordenamiento
                var findOptions = new FindOptions<TEntity>();
                if (orderBy != null)
                {
                    var sortDefinition = ascending ?
                        Builders<TEntity>.Sort.Ascending(orderBy) :
                        Builders<TEntity>.Sort.Descending(orderBy);
                    findOptions.Sort = sortDefinition;
                }

                // Configurar paginación
                findOptions.Skip = (page - 1) * pageSize;
                findOptions.Limit = pageSize;

                // Ejecutar consulta
                var results = await _collection.FindAsync(baseFilter, findOptions);
                var data = await results.ToListAsync();

                return (data, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all entities: {ex.Message}", ex);
            }
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq("_id", id);
                return await _collection.Find(filter).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting entity by ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var resp = await _collection.Find(filter).FirstOrDefaultAsync();
                return resp;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting first entity: {ex.Message}", ex);
            }
        }

        public async Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>> orderBy = null)
        {
            try
            {
                var findOptions = new FindOptions<TEntity>();

                if (orderBy != null)
                {
                    findOptions.Sort = Builders<TEntity>.Sort.Descending(orderBy);
                }
                else
                {
                    findOptions.Sort = Builders<TEntity>.Sort.Descending("CreatedDate");
                }

                var results = await _collection.FindAsync(filter, findOptions);
                return await results.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting last entity: {ex.Message}", ex);
            }
        }

        #endregion
        
        #region Create Operations

    public async Task<bool> InsertAsync(TEntity entity)
    {
        try
        {
            await _collection.InsertOneAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error inserting entity: {ex.Message}", ex);
        }
    }

    public async Task<bool> InsertManyAsync(IEnumerable<TEntity> entities)
    {
        try
        {
            await _collection.InsertManyAsync(entities);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error inserting multiple entities: {ex.Message}", ex);
        }
    }

    #endregion

        #region Update Operations

        public async Task<bool> UpdateAsync(string id, TEntity entity)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq("_id", id);
                var result = await _collection.ReplaceOneAsync(filter, entity);
                
                return result.IsAcknowledged && (result.ModifiedCount > 0 || result.MatchedCount > 0);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating entity {id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateManyAsync(Expression<Func<TEntity, bool>> filter, 
            UpdateDefinition<TEntity> updateDefinition)
        {
            try
            {
                var result = await _collection.UpdateManyAsync(filter, updateDefinition);
                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating multiple entities: {ex.Message}", ex);
            }
        }

        #endregion

        #region Delete Operations

        public async Task<bool> DeleteAsync(string id, bool active = false)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq("_id", id);
                var update = Builders<TEntity>.Update
                    .Set("Deleted", active)
                    .Set("ModificationDate", DateTime.UtcNow)
                    .Set("ModificationUser", "web");

                var result = await _collection.UpdateOneAsync(filter, update);
                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error soft deleting entity {id}: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteManyAsync(Expression<Func<TEntity, bool>> filter, bool active = false)
        {
            try
            {
                var update = Builders<TEntity>.Update
                    .Set("Deleted", active)
                    .Set("ModificationDate", DateTime.UtcNow)
                    .Set("ModificationUser", "web");

                var result = await _collection.UpdateManyAsync(filter, update);
                return result.IsAcknowledged && result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error soft deleting multiple entities: {ex.Message}", ex);
            }
        }

        public async Task<bool> PermanentDeleteAsync(string id)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq("_id", id);
                var result = await _collection.DeleteOneAsync(filter);
                return result.IsAcknowledged && result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error permanently deleting entity {id}: {ex.Message}", ex);
            }
        }

        #endregion

        #region Count Operations

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            try
            {
                if (filter == null)
                    return await _collection.CountDocumentsAsync(Builders<TEntity>.Filter.Empty);
                
                return await _collection.CountDocumentsAsync(filter);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error counting entities: {ex.Message}", ex);
            }
        }

        #endregion



        #endregion IRepository<TEntity> Members
    }
}
