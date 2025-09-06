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

namespace luxuryProperty.app.applicationCore.Dtos
{
    /// <summary>
    /// Class PropertyImageDto.
    /// </summary>
    public class PropertyImageDto
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
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>The creation date.</value>
        public DateTime? CreationDate { get; set; }
        /// <summary>
        /// Gets or sets the creation user.
        /// </summary>
        /// <value>The creation user.</value>        
        public string CreationUser { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PropertyDto"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        public bool Deleted { get; set; }
    }

    /// <summary>
    /// Class PropertyImageUpdateDto.
    /// </summary>
    public class PropertyImageUpdateDto : PropertyImageDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the modification date.
        /// </summary>
        /// <value>The modification date.</value>
        public DateTime? ModificationDate { get; set; }
        /// <summary>
        /// Gets or sets the modification user.
        /// </summary>
        /// <value>The modification user.</value>
        public string ModificationUser { get; set; }
    }
}
