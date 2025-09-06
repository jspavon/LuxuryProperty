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
    public class UsersDto
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
    }

    public class UsersUpdateDto : UsersDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

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
