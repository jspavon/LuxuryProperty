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
    /// Class PropertyTrace.
    /// Implements the <see cref="luxuryProperty.app.infraestructure.Entities" />
    /// </summary>
    /// <seealso cref="luxuryProperty.app.infraestructure.Entities" />
    public class PropertyTrace : BaseEntity
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
        /// Gets or sets the date sale.
        /// </summary>
        /// <value>The date sale.</value>
        public DateTime DateSale { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public decimal Value { get; set; }
        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>The tax.</value>
        public decimal Tax { get; set; }
        /// <summary>
        /// Gets or sets the identifier property.
        /// </summary>
        /// <value>The identifier property.</value>
        public int IdProperty { get; set; }
        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>The property.</value>
        public virtual Property Property { get; set; }
    }
}
