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
    /// Class OwnerCreateModelValidator.
    /// Implements the <see cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.OwnerCreateModel}" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.OwnerCreateModel}" />
    public class OwnerCreateModelValidator : AbstractValidator<OwnerCreateModel>
    {
        public OwnerCreateModelValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull()
               .MaximumLength(50);

            RuleFor(x => x.Address)
               .NotEmpty()
               .NotNull()
               .MaximumLength(100);
        }
    }

    /// <summary>
    /// Class OwnerUpdateModelValidator.
    /// Implements the <see cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.OwnerUpdateModel}" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{luxuryProperty.app.application.Models.OwnerUpdateModel}" />
    public class OwnerUpdateModelValidator : AbstractValidator<OwnerUpdateModel>
    {
        public OwnerUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
