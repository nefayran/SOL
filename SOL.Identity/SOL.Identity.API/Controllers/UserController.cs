using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SOL.Core.Commands;
using SOL.Identity.SOL.Identity.Domain.Commands.User;
using SOL.Identity.SOL.Identity.Infrastructure.Security;

namespace SOL.Identity.SOL.Identity.API.Controllers
{
    [ApiController]
    [Route("/api/v1/user/")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        private readonly JwtValidator _jwt;

        public UserController(
            IMediator mediator,
            ILogger<UserController> logger, JwtValidator jwt)
        {
            _mediator = mediator;
            _logger = logger;
            _jwt = jwt;
        }
        [HttpPost]
        [Route("create")]
        [ProducesResponseType(200, Type = typeof(Result))]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
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
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                _logger.LogInformation("User login successfuly.");
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
        public IActionResult CheckLoginUser([FromBody] CheckLoginUserCommand command)
        {
            //var result = await _mediator.Send(command);
            var result = _jwt.ValidateJwtToken(command.Token);
            return Ok(result);
            //if (result.Success)
            //{
            //    _logger.LogInformation("User login successfuly.");
            //    command.Token = result.Data;
            //    return Ok(command);
            //}

            //_logger.LogError("Error when user login.");
            //return BadRequest(result.Errors);
        }
    }
}
