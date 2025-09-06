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

using luxuryProperty.app.applicationCore.Dtos;

namespace luxuryProperty.app.applicationCore.Interfaces
{
    /// <summary>
    /// Interface IPropertyService/
    /// </summary>
    public interface IPropertyService
    {
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;PropertyDto&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        Task<PropertyUpdateDto> GetByIdAsync(string id);
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>Task&lt;IEnumerable&lt;PropertyDto&gt;&gt;.</returns>
        Task<List<PropertyUpdateDto>> GetAllAsync(int page, int limit, string name, string address, int minPrice, int maxPrice);
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>System.ValueTuple&lt;System.Boolean, System.Int32&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        Task<(bool status, string id)> Post(PropertyDto entity);
        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        Task<bool> PutAsync(string id, PropertyUpdateDto entity);
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        Task<bool> DeleteAsync(string id);
    }
}
