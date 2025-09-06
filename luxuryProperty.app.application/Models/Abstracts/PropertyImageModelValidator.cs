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
    public class PropertyImageCreateModelValidator : AbstractValidator<PropertyImageCreateModel>
    {
        public PropertyImageCreateModelValidator()
        {
            RuleFor(x => x.IdProperty)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.File)
               .NotEmpty()
               .NotNull()
               .MaximumLength(int.MaxValue);
        }
    }

    public class PropertyImageUpdateModelValidator : AbstractValidator<PropertyImageUpdateModel>
    {
        public PropertyImageUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
