namespace luxuryProperty.app.application.Models
{
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

    public class PropertyCreateModel
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
    }

    /// <summary>
    /// Class PropertyUpdateDto.
    /// </summary>
    public class PropertyUpdateModel : PropertyCreateModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
    }
}
