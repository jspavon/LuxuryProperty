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
    /// Interface IPropertyImageService/
    /// </summary>
    public interface IPropertyImageService
    {
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;PropertyImageDto&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        Task<PropertyImageDto> GetByIdAsync(string id);
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>Task&lt;IEnumerable&lt;PropertyImageDto&gt;&gt;.</returns>
        Task<List<PropertyImageDto>> GetAllAsync(int page, int limit, string orderBy, bool ascending = true);
        /// <summary>
        /// Posts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>System.ValueTuple&lt;System.Boolean, System.Int32&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        Task<(bool status, string id)> Post(PropertyImageDto entity);
        /// <summary>
        /// Puts the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        Task<bool> PutAsync(string id, PropertyImageUpdateDto entity);
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        /// <remarks>Jhon Steven Pavón Bedoya</remarks>
        Task<bool> DeleteAsync(string id);

        /// <summary>
        /// get by property identifier as an asynchronous operation.
        /// </summary>
        /// <param name="idProperty"></param>
        /// <returns></returns>
        Task<PropertyImageDto> GetByPropertyIdAsync(string idProperty);
    }
}
