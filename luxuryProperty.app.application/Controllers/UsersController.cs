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
using System.Threading.Tasks;

namespace luxuryProperty.app.application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// The integration sap service
        /// </summary>
        private readonly IUsersService _service;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UsersController> _logger;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The browser detector
        /// </summary>
        private readonly IBrowserDetector _browserDetector;

        public UsersController(ILogger<UsersController> logger, IMapper mapper, IBrowserDetector browserDetector, IUsersService service)
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
        [ProducesResponseType(typeof(ResponseService<IEnumerable<UsersDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync(int? page, int? limit)
        {
            _logger.LogInformation(nameof(GetAllAsync));

            var result = await _service.GetAllAsync(page ?? 1, limit ?? 1000, "Id");

            var response = new ResponseService<IEnumerable<UsersDto>>
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
        /// <remarks>Jhon Steven Pavon Bedoya</remarks>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseService<UsersDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation(nameof(GetByIdAsync));

            var result = await _service.GetByIdAsync(id);

            var existResult = result != null;
            var response = new ResponseService<UsersDto>
            {
                Status = existResult,
                Message = !existResult ? GenericEnumerator.Status.Error.ToStringAttribute() : GenericEnumerator.Status.Ok.ToStringAttribute(),
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
        [Produces(MediaTypeNames.Application.Json, Type = typeof(OwnerCreateModel))]
        public async Task<IActionResult> Post([FromBody] UsersCreateModel request)
        {
            _logger.LogInformation(nameof(Post));

            var objRequest = _mapper.Map<UsersDto>(request);
            objRequest.CreationUser = GenericConstant.GENERIC_USER;
            objRequest.CreationDate = DateTime.UtcNow;

            var (status, id) = await _service.Post(objRequest);

            return Ok(new ResponseService<string>
            {
                Status = status,
                Data = status ? id : string.Empty,
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
        [Produces(MediaTypeNames.Application.Json, Type = typeof(OwnerUpdateModel))]
        public async Task<IActionResult> PutAsync(string id, [FromBody] UsersUpdateModel request)
        {
            _logger.LogInformation(nameof(PutAsync));

            if (id != request.Id)
                return BadRequest();

            var objRequest = _mapper.Map<UsersUpdateDto>(request);

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
        public async Task<IActionResult> DeleteAsync(string id, bool active)
        {
            _logger.LogInformation(nameof(DeleteAsync));

            var status = await _service.DeleteAsync(id, active).ConfigureAwait(false);

            var response = new ResponseService<bool>
            {
                Status = status
            };
            return Ok(response);
        }

                /// <summary>
        /// Get by user name as an asynchronous operation.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("Validate/{userName}")]
        [ProducesResponseType(typeof(ResponseService<UsersDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ValidateUser(string userName, string password)
        {
            _logger.LogInformation(nameof(GetByIdAsync));

            var result = await _service.ValidateUser(userName, password);

            var existResult = result;
            var response = new ResponseService<bool>
            {
                Status = existResult,
                Message = !existResult ? GenericEnumerator.Status.Error.ToStringAttribute() : GenericEnumerator.Status.Ok.ToStringAttribute(),
                Data = result
            };

            return Ok(response);
        }


    }
}
