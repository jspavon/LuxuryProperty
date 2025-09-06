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

using luxuryProperty.app.commons.Configuration;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace luxuryProperty.app.application.Swagger
{

    /// <summary>
    /// Class SwaggerConfig.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Extensions
    {

        /// <summary>
        /// Adds the registration.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection.</returns>
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        public static IServiceCollection AddRegistrationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{Assembly.GetExecutingAssembly().GetName().Version}",
                    new OpenApiInfo
                    {
                        Title = "luxuryProperty.app",
                        Version = $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")} - v{Assembly.GetExecutingAssembly().GetName().Version}",
                        Description = "Design a DDD (Domain-Driven Design) Microservice, Identity ASP.NET Core / .NET Framework CORE 8.0",
                        Contact = new OpenApiContact
                        {
                            Name = CommonConfiguration.ContactName,
                            Email = CommonConfiguration.Email
                        }
                    });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            
            

            services.AddMvcCore().AddApiExplorer();
            return services;
        }

        /// <summary>
        /// Adds the registration.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>IApplicationBuilder.</returns>
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        public static IApplicationBuilder AddRegistrationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{Assembly.GetExecutingAssembly().GetName().Version}/swagger.json", $"luxuryProperty.app (v{Assembly.GetExecutingAssembly().GetName().Version})");
                c.RoutePrefix = string.Empty;
            });
            return app;
        }
    }
}
