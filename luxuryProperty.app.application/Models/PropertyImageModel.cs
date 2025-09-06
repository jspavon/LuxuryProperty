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

namespace luxuryProperty.app.application.Models
{
    public class PropertyImageCreateModel
    {
        /// <summary>
        /// Gets or sets the identifier property.
        /// </summary>
        /// <value>The identifier property.</value>
        public string IdProperty { get; set; }
        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
        public string File { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PropertyImage"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }
    }

    /// <summary>
    /// Class PropertyImageUpdateDto.
    /// </summary>
    public class PropertyImageUpdateModel : PropertyImageCreateModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
    }
}
