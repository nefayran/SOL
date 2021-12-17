using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOL.Core.Commands;
using SOL.Identity.Application.Interfaces;
using SOL.Identity.Application.User;
using SOL.Identity.Domain.Commands.User;
using SOL.Identity.Infrastructure.Security;

namespace SOL.Identity.API.Controllers
{
    [ApiController]
    [Route("/api/v1/user/")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly JwtValidator _jwt;
        private readonly IUserQueries _userQueries;

        public UserController(
            IMediator mediator,
            ILogger<UserController> logger, JwtValidator jwt,
            IUserQueries userQueries)
        {
            _mediator = mediator;
            _logger = logger;
            _jwt = jwt;
            _userQueries = userQueries;
        }
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200, Type = typeof(Result))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                _logger.LogInformation("New user added.");
                return Ok(command);
            }

            _logger.LogError("Error when try add new user.");
            return BadRequest(result.Errors);
        }
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200, Type = typeof(Result))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                _logger.LogInformation("User login successful.");
                // TODO: Rework with query.
                command.Token = result.Data;
                return Ok(command);
            }

            _logger.LogError("Error when user login.");
            return BadRequest(result.Errors);
        }
        [HttpPost]
        [Route("check")]
        [ProducesResponseType(200, Type = typeof(Result))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [Authorize]
        public IActionResult CheckLoginUser([FromBody] CheckLoginUserCommand command)
        {
            // TODO: Rework with command.
            var result = _jwt.ValidateJwtToken(command.Token);
            return Ok(result);
        }
        [HttpGet]
        [Route("fetch")]
        [Authorize]
        public IActionResult FetchUser([FromQuery] string email)
        {
            return Ok(_userQueries.GetByEmailAsync(email));
        }
    }
}
