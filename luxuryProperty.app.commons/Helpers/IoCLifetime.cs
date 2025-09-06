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

namespace luxuryProperty.app.commons.Helpers
{
    /// <summary>
    /// Contains the different lifetime that is supported. By default Transient lifetime is used.
    /// </summary>
    public enum IoCLifetime
    {
        /// <summary>
        /// The transient
        /// </summary>
        Transient,

        /// <summary>
        /// The hierarchical
        /// </summary>
        Hierarchical,

        /// <summary>
        /// The container controlled
        /// </summary>
        ContainerControlled,
        /// <summary>
        /// The singleton
        /// </summary>
        Singleton
    }
}
