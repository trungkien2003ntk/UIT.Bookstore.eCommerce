using KKBookstore.API.Extensions;
using KKBookstore.Application.Users.Commands.CreateUser;
using KKBookstore.Application.Users.Commands.RefreshAccessToken;
using KKBookstore.Application.Users.Commands.SignIn;
using KKBookstore.Application.Users.Queries.GetUser;
using KKBookstore.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("users")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> GetUsersAsync(
            CancellationToken cancellationToken = default
        )
        {
            
            return Ok();
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserAsync(
            [FromRoute]int id,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(new GetUserQuery(id), cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
        }

        // write an endpoint for register account, using email, password and userroles
        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAsync(
            [FromBody]CreateUserCommand createUserCommand,
            CancellationToken cancellationToken = default
        )
        {
            // Implement account registration logic here
            var result = await _mediator.Send(createUserCommand, cancellationToken);

            return result.IsSuccess ?
                CreatedAtAction(
                    nameof(GetUserAsync),
                    new { id = result.Value }
                ) : 
                result.ToActionResult();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignInAsync(
            [FromBody]SignInCommand signInCommand,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(signInCommand, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessTokenAsync(
            [FromBody]RefreshAccessTokenCommand refreshAccessTokenCommand,
            CancellationToken cancellationToken = default
        )
        {
            var result = await _mediator.Send(refreshAccessTokenCommand, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
        }
    }
}
