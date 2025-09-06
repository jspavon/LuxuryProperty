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
    /// Class PropertyCreateModelValidator.
    /// Implements the <see cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.PropertyCreateModel}" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.PropertyCreateModel}" />
    public class PropertyCreateModelValidator : AbstractValidator<PropertyCreateModel>
    {
        public PropertyCreateModelValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull()
               .MaximumLength(100);

            RuleFor(x => x.Address)
               .NotEmpty()
               .NotNull()
               .MaximumLength(100);

            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .NotEqual(0);

            RuleFor(x => x.CodeInternal)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Year)
                .NotNull()
                .NotEmpty()
                .NotEqual(0);

            RuleFor(x => x.IdOwner)
            .NotNull()
            .NotEmpty();
        }
    }

    /// <summary>
    /// Class PropertyUpdateModelValidator.
    /// Implements the <see cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.PropertyUpdateModel}" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.PropertyUpdateModel}" />
    public class PropertyUpdateModelValidator : AbstractValidator<PropertyUpdateModel>
    {
        public PropertyUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();;
        }
    }
}
