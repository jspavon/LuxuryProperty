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

using luxuryProperty.app.infraestructure.Entities.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace luxuryProperty.app.infraestructure.Entities
{
    /// <summary>
    /// Class Property.
    /// Implements the <see cref="luxuryProperty.app.infraestructure.Entities" />
    /// </summary>
    /// <seealso cref="luxuryProperty.app.infraestructure.Entities" />
    public class Property : BaseEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement(Order = 0)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
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
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        public virtual Owner Owner { get; set; }
        /// <summary>
        /// Gets or sets the property image.
        /// </summary>
        /// <value>The property image.</value>
        public virtual ICollection<PropertyImage> PropertyImages { get; set; }
        /// <summary>
        /// Gets or sets the property trace.
        /// </summary>
        /// <value>The property trace.</value>
        public virtual ICollection<PropertyTrace> PropertyTraces { get; set; }
    }
}
