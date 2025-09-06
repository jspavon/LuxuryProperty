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

using FluentValidation;

namespace luxuryProperty.app.application.Models.Abstracts
{
    /// <summary>
    /// Class UsersCreateModelValidator.
    /// Implements the <see cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.UsersCreateModel}" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.UsersCreateModel}" />
    public class UsersCreateModelValidator : AbstractValidator<UsersCreateModel>
    {
        public UsersCreateModelValidator()
        {
            RuleFor(x => x.FullName)
               .NotEmpty()
               .NotNull()
               .MaximumLength(100);

            RuleFor(x => x.User)
               .NotEmpty()
               .NotNull()
               .MaximumLength(50);

            RuleFor(x => x.Role)
             .NotEmpty()
             .NotNull()
             .MaximumLength(50);

            RuleFor(x => x.Password)
             .NotEmpty()
             .NotNull()
             .MaximumLength(50);

            RuleFor(x => x.Active)
                .NotNull();
                
        }
    }

    /// <summary>
    /// Class UsersUpdateModelValidator.
    /// Implements the <see cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.UsersUpdateModel}" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.UsersUpdateModel}" />
    public class UsersUpdateModelValidator : AbstractValidator<UsersUpdateModel>
    {
        public UsersUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
