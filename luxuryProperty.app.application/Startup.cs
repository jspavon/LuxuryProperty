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

using luxuryProperty.app.application.Filters;
using luxuryProperty.app.application.Swagger;
using luxuryProperty.app.applicationCore.Interfaces;
using luxuryProperty.app.applicationCore.Services;
using luxuryProperty.app.commons.Configuration;
using luxuryProperty.app.infraestructure.Context.Interfaces;
using luxuryProperty.app.infraestructure.Context;
using luxuryProperty.app.infraestructure.Repository;
using luxuryProperty.app.infraestructure.UnitOfWork;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.AspNetCore;
using luxuryProperty.app.application.Models.Abstracts;
using luxuryProperty.app.application.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using System.Net;


namespace luxuryProperty.app.application
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(Microsoft.Extensions.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSettings = Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            //services.AddAutoMapper();
            services.AddAutoMapper(typeof(Startup));

            #region Register (dependency injection)

            // DataContext of Database.
            //services.AddDbContext<IDataContext, CoreContext>();

            services.AddScoped<ILogger, Logger<MongoDataContext>>();

            // CustomerRepository await UnitofWork parameter ctor explicit
            services.AddTransient<IRepositoryFactory, RepositoryFactory>();

            //Services
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IPropertyImageService, PropertyImageService>();
            services.AddScoped<IPropertyTraceService, PropertyTraceService>();
            services.AddScoped<IJWTServices, JWTServices>();


            // Infrastructure                       
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepositoryData<>), typeof(RepositoryData<>));
            services.AddScoped<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<IMongoDataContext, MongoDataContext>();

            #endregion

            ConfigureCorsService(ref services);

            services.AddDataProtection();

            services.AddRegistrationSwagger();

            services.AddBrowserDetection();

            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                options.Events = new JwtBearerEvents {
                    OnAuthenticationFailed = context =>
                    {
                        return Task.CompletedTask;
                    },

                    OnChallenge = context =>
                    {
                        context.HandleResponse(); // Evita que el middleware añada el error automáticamente
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync("{\"message\": \"Authorization failed\"}");
                    }
                };
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OwnerCreateModelValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureSecurityApp(ref app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.AddRegistrationSwagger();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            ConfigureCorsApp(ref app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        /// <summary>
        /// Configure CORS Service.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <remarks>Elkin Vasquez Isenia</remarks>
        private void ConfigureCorsService(ref IServiceCollection services)
        {
            // Enables CORS and httpoptions
            services.AddCors(options =>
            {
                options.AddPolicy(CommonConfiguration.EnableCorsName, builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.WithHeaders(CommonConfiguration.Authorization, CommonConfiguration.Accept, CommonConfiguration.ContentType, CommonConfiguration.Origin);
                    builder.SetIsOriginAllowed((_) => true);
                });
            });
            services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);
        }

        /// <summary>
        /// Configure Cors App.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <remarks>Elkin Vasquez Isenia</remarks>
        private void ConfigureCorsApp(ref IApplicationBuilder app)
        {
            app.UseCors(CommonConfiguration.EnableCorsName);
        }

        /// <summary>
        /// Configures the security application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <remarks>Elkin Vasquez Isenia</remarks>
        private void ConfigureSecurityApp(ref IApplicationBuilder app)
        {
            app.UseHsts(options => options.MaxAge(365).IncludeSubdomains().Preload());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXfo(options => options.SameOrigin());
            app.UseCsp(opts =>
            {
                opts.BlockAllMixedContent();
                opts.StyleSources(s => s.Self());
                opts.StyleSources(s => s.UnsafeInline());
                opts.FontSources(s => s.Self());
                opts.FormActions(s => s.Self());
                opts.FrameAncestors(s => s.Self());
                opts.ImageSources(s => s.Self());
            });
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opts => opts.NoReferrer());
            app.UseStaticFiles();
        }
    }
}
