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
    /// <summary>
    /// Class UsersCreateModel.
    /// </summary>
    public class UsersCreateModel
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public string User { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string FullName { get; set; }
        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        /// <value>The Password.</value>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the Role.
        /// </summary>
        /// <value>The Role.</value>
        public string Role { get; set; }
        /// <summary>
        /// Gets or sets the Active.
        /// </summary>
        /// <value>The Active.</value>
        public bool Active { get; set; }
    }

    public class UsersUpdateModel 
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }
    }
}
