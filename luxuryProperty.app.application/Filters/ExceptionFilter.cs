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

using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using System.Net;
using Newtonsoft.Json;
using luxuryProperty.app.commons.Dtos;


namespace luxuryProperty.app.application.Filters
{
    [ExcludeFromCodeCoverage]
    public sealed class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<ExceptionFilter> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionFilter" /> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="ArgumentNullException">configuration
        /// or
        /// logger</exception>
        /// <remarks>Elkin Vasquez Isenia</remarks>
        public ExceptionFilter(ILogger<ExceptionFilter> logger, IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        /// <remarks>Elkin Vasquez Isenia</remarks>
        public void OnException(ExceptionContext context)
        {
            var message = JsonConvert.SerializeObject(MessageError(context.Exception), Newtonsoft.Json.Formatting.Indented);
            var objResponseService = new ResponseService<string>
            {
                Status = false,
                Message = HttpStatusCode.InternalServerError.ToString(),
                Data = message
            };


            _logger.LogError(context.Exception, message);


            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(objResponseService, Newtonsoft.Json.Formatting.Indented));
        }

        /// <summary>
        /// Messages the error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="level">The level.</param>
        /// <returns>List&lt;ResponseException&gt;.</returns>
        /// <remarks>Elkin Vasquez Isenia</remarks>
        private static List<ResponseException> MessageError(Exception ex, int level = 30)
        {
            var listError = new List<ResponseException>();
            var counter = 1;
            while (ex != null && counter <= level)
            {
                listError.Add(new ResponseException
                {
                    ErrorLevel = counter.ToString(),
                    ErrorMessage = ex.Message,
                    ErrorSource = ex.Source,
                    ErrorStackTrace = ex.StackTrace,
                    ErrorTargetSite = ex.TargetSite?.ToString(),
                    ErrorData = ex.Data
                });
                ex = ex.InnerException;
                counter++;
            }
            return listError;
        }
    }
}
