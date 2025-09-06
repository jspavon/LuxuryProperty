using AutoMapper;
using luxuryProperty.app.application.Models;
using luxuryProperty.app.applicationCore.Dtos;
using luxuryProperty.app.applicationCore.Interfaces;
using luxuryProperty.app.commons.Constants;
using luxuryProperty.app.commons.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shyjus.BrowserDetection;
using System.Net.Mime;
using System.Net;

namespace luxuryProperty.app.application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        /// <summary>
        /// The integration IJWTServices service
        /// </summary>
        private readonly IJWTServices _service;

        /// <summary>
        /// The integration IUsersService service
        /// </summary>
        private readonly IUsersService _usersServices;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<JWTController> _logger;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The browser detector
        /// </summary>
        private readonly IBrowserDetector _browserDetector;

        public JWTController(ILogger<JWTController> logger, IMapper mapper, IBrowserDetector browserDetector, IJWTServices service, IUsersService usersService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _browserDetector = browserDetector ?? throw new ArgumentNullException(nameof(browserDetector));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _usersServices = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        /// <summary>
        /// Posts the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost("GenerateToken")]
        [DisableRequestSizeLimit]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseService<string>), (int)HttpStatusCode.Created)]
        [Produces(MediaTypeNames.Application.Json, Type = typeof(JWTModel))]
        public IActionResult GenerateToken([FromBody] JWTModel request)
        {
            _logger.LogInformation(nameof(GenerateToken));

            bool validateUser = _usersServices.ValidateUser(request.username, request.password).Result;
            if (!validateUser) return Ok(new ResponseService<string>
            {
                Status = !validateUser ? false : true,
                Data = !validateUser ? "Invalid User" : string.Empty
            });

            string token = _service.GenerateToken(request.username, "dev").Result;

            return Ok(new ResponseService<string>
            {
                Status = string.IsNullOrEmpty(token) ? false : true,
                Data = string.IsNullOrEmpty(token) ? string.Empty : token
            });
        }
    }
}
