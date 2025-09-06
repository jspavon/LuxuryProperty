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

    public class PropertyController : ControllerBase
    {
        /// <summary>
        /// The integration sap service
        /// </summary>
        private readonly IPropertyService _service;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<PropertyController> _logger;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The browser detector
        /// </summary>
        private readonly IBrowserDetector _browserDetector;


        public PropertyController(ILogger<PropertyController> logger, IMapper mapper, IBrowserDetector browserDetector, IPropertyService service)
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
        [ProducesResponseType(typeof(ResponseService<IEnumerable<PropertyUpdateDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(int? page, int? limit, string? name, string? address, int? minPrice, int? maxPrice)
        {
            _logger.LogInformation(nameof(GetAllAsync));
            var result = await _service.GetAllAsync(
                page ?? 1,
                limit ?? 1000,
                name ?? string.Empty,
                address ?? string.Empty,
                minPrice ?? 0,
                maxPrice ?? 0
            );

            var response = new ResponseService<List<PropertyUpdateDto>>
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
        [ProducesResponseType(typeof(ResponseService<PropertyUpdateDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation(nameof(GetByIdAsync));

            var result = await _service.GetByIdAsync(id);

            var existResult = result != null;
            var response = new ResponseService<PropertyUpdateDto>
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
        [Produces(MediaTypeNames.Application.Json, Type = typeof(PropertyCreateModel))]
        public async Task<IActionResult> Post([FromBody] PropertyCreateModel request)
        {
            _logger.LogInformation(nameof(Post));

            var objRequest = _mapper.Map<PropertyDto>(request);
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
        [Produces(MediaTypeNames.Application.Json, Type = typeof(PropertyUpdateModel))]
        public async Task<IActionResult> PutAsync(string id, [FromBody] PropertyUpdateModel request)
        {
            _logger.LogInformation(nameof(PutAsync));

            if (id != request.Id)
                return BadRequest();

            var objRequest = _mapper.Map<PropertyUpdateDto>(request);

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

    }
}
