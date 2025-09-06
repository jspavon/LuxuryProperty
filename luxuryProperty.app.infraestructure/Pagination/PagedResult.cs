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

using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace luxuryProperty.app.infraestructure.Pagination
{
    /// <summary>
    /// Class PagedResult.
    /// Implements the <see cref="System.Collections.Generic.List{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.List{T}" />
    /// <remarks>Jhon Steven Pavon Bedoya</remarks>
    [ExcludeFromCodeCoverage]
    public class PagedResult<T> : List<T>
    {
        /// <summary>
        /// Gets the current page.
        /// </summary>
        /// <value>The current page.</value>
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        public int CurrentPage { get; }

        /// <summary>
        /// Gets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        public int TotalPages { get; }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        public int PageSize { get; }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        /// <value>The total count.</value>
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        public int TotalCount { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagedResult{T}" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="count">The count.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        public PagedResult(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        /// <summary>
        /// to paged list as an asynchronous operation.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="ascending">if set to <c>true</c> [ascending].</param>
        /// <returns>Task&lt;PagedResult&lt;T&gt;&gt;.</returns>
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        public static async Task<PagedResult<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize, string orderBy, bool ascending = true)
        {
            var count = source.Count();
            var items = await source.OrderByPropertyOrField(orderBy, ascending).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<T>(items, count, pageNumber, pageSize);
        }
    }
}
