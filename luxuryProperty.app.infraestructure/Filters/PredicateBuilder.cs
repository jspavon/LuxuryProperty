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

using System.Linq.Expressions;

public static class PredicateBuilder
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
    {
        var parameter = Expression.Parameter(typeof(T));

        var combined = Expression.AndAlso(
            Expression.Invoke(expr1, parameter),
            Expression.Invoke(expr2, parameter));

        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }
}
