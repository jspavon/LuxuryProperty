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

using AutoMapper;
using luxuryProperty.app.application.Models;
using luxuryProperty.app.applicationCore.Dtos;
using luxuryProperty.app.applicationCore.Interfaces;
using luxuryProperty.app.commons.Constants;
using luxuryProperty.app.commons.Dtos;
using luxuryProperty.app.commons.Enums;
using luxuryProperty.app.commons.Enums.Exts;
using Microsoft.AspNetCore.Mvc;
using Shyjus.BrowserDetection;
using System.Net.Mime;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace luxuryProperty.app.application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PropertyImageController : ControllerBase
    {
        /// <summary>
        /// The integration sap service
        /// </summary>
        private readonly IPropertyImageService _service;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PropertyImageController> _logger;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The browser detector
        /// </summary>
        private readonly IBrowserDetector _browserDetector;


        public PropertyImageController(ILogger<PropertyImageController> logger, IMapper mapper, IBrowserDetector browserDetector, IPropertyImageService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _browserDetector = browserDetector ?? throw new ArgumentNullException(nameof(browserDetector));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }



        /// <summary>
        /// get all as an asynchronous operation.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{page:int}/{limit:int}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseService<IEnumerable<PropertyImageDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(int? page, int? limit)
        {
            _logger.LogInformation(nameof(GetAllAsync));

            var result = await _service.GetAllAsync(page ?? 1, limit ?? 1000, "Id").ConfigureAwait(false);
            var response = new ResponseService<IEnumerable<PropertyImageDto>>
            {
                Status = result.Count > 0,
                Message = result.Count > 0 ? GenericEnumerator.Status.Ok.ToStringAttribute() : GenericEnumerator.Status.Error.ToStringAttribute(),
                Data = result
            };
            return Ok(response);
        }

        /// <summary>
        /// Get by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        /// <remarks>Elkin Vasquez Isenia</remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseService<PropertyImageDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation(nameof(GetByIdAsync));

            var result = await _service.GetByIdAsync(id);

            var existResult = result != null;
            var response = new ResponseService<PropertyImageDto>
            {
                Status = existResult,
                Message = existResult ? GenericEnumerator.Status.Ok.ToStringAttribute() : GenericEnumerator.Status.Error.ToStringAttribute(),
                Data = result
            };

            return Ok(response);
        }

        /// <summary>
        /// Posts the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseService<string>), (int)HttpStatusCode.Created)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(PropertyImageCreateModel))]
        public async Task<IActionResult> Post([FromBody] PropertyImageCreateModel request)
        {
            _logger.LogInformation(nameof(Post));

            var objRequest = _mapper.Map<PropertyImageDto>(request);
            objRequest.CreationUser = GenericConstant.GENERIC_USER;
            objRequest.CreationDate = DateTime.Now;

            var (status, id) = await _service.Post(objRequest);

            return Ok(new ResponseService<string>
            {
                Status = status,
                Data = status ? id : string.Empty
            });
        }

        /// <summary>
        /// put as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseService<bool>), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(PropertyImageUpdateModel))]
        public async Task<IActionResult> PutAsync(string id, [FromBody] PropertyImageUpdateModel request)
        {
            _logger.LogInformation(nameof(PutAsync));

            if (id != request.Id)
                return BadRequest();

            var objRequest = _mapper.Map<PropertyImageUpdateDto>(request);

            objRequest.ModificationUser = GenericConstant.GENERIC_USER;
            objRequest.ModificationDate = DateTime.Now;

            var status = await _service.PutAsync(id, objRequest).ConfigureAwait(false);

            var response = new ResponseService<bool>
            {
                Status = status
            };

            return Ok(response);
        }

        /// <summary>
        /// delete as an asynchronous operation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ResponseService<bool>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            _logger.LogInformation(nameof(DeleteAsync));

            var status = await _service.DeleteAsync(id).ConfigureAwait(false);

            var response = new ResponseService<bool>
            {
                Status = status
            };
            return Ok(response);
        }

        
        /// <summary>
        /// get by property identifier as an asynchronous operation.
        /// </summary>
        /// <param name="idProperty"></param>
        /// <returns></returns>
        [HttpGet("GetByPropertyId/{idProperty}")]
        [ProducesResponseType(typeof(ResponseService<PropertyImageDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByPropertyIdAsync(string idProperty)
        {
            _logger.LogInformation(nameof(GetByPropertyIdAsync));

            var result = await _service.GetByPropertyIdAsync(idProperty);

            var existResult = result != null;
            var response = new ResponseService<PropertyImageDto>
            {
                Status = existResult,
                Message = existResult ? GenericEnumerator.Status.Ok.ToStringAttribute() : GenericEnumerator.Status.Error.ToStringAttribute(),
                Data = result
            };

            return Ok(response);
        }
    }
}
