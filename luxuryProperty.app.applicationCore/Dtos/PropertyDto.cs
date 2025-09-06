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

using luxuryProperty.app.infraestructure.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace luxuryProperty.app.applicationCore.Dtos
{
    /// <summary>
    /// Class PropertyDto.
    /// </summary>
    public class PropertyDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        public int Price { get; set; }
        /// <summary>
        /// Gets or sets the code internal.
        /// </summary>
        /// <value>The code internal.</value>
        public int CodeInternal { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>The year.</value>
        public int Year { get; set; }
        /// <summary>
        /// Gets or sets the identifier owner.
        /// </summary>
        /// <value>The identifier owner.</value>
        public string IdOwner { get; set; }
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
        /// <summary>
        /// Gets or sets the property image.
        /// </summary>
        /// <value>The property image.</value>
        public List<PropertyImageDto> PropertyImages { get; set; }
    }

    /// <summary>
    /// Class PropertyUpdateDto.
    /// </summary>
    public class PropertyUpdateDto : PropertyDto
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
