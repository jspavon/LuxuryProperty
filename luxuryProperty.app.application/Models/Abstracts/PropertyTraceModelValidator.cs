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
    public class PropertyTraceCreateModelValidator : AbstractValidator<PropertyTraceCreateModel>
    {
        public PropertyTraceCreateModelValidator()
        {
            RuleFor(x => x.DateSale)
                .NotEmpty()
                .Must(BeAValidDate);

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Value)
                .NotNull()
                .NotEqual(0);

            RuleFor(x => x.Tax)
                .NotNull()
                .NotEqual(0);

            RuleFor(x => x.IdProperty)
                .NotNull();
        }


        private bool BeAValidDate(DateTime date)
        {
            var result = !date.Equals(default(DateTime));
            return result;
        }
    }

    public class PropertyTraceUpdateModelValidator : AbstractValidator<PropertyTraceUpdateModel>
    {
        public PropertyTraceUpdateModelValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();
        }
    }

}
