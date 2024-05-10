using KKBookstore.API.Extensions;
using KKBookstore.Application.Users.CreateUser;
using KKBookstore.Application.Users.RefreshAccessToken;
using KKBookstore.Application.Users.SignIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        // write an endpoint for register account, using email, password and userroles
        [HttpPost("register")]
        public async Task<IActionResult> CreateUserAsync(
            [FromBody]CreateUserCommand createUserCommand,
            CancellationToken cancellationToken = default
        )
        {
            // Implement account registration logic here
            var result = await _mediator.Send(createUserCommand, cancellationToken);

            return result.IsSuccess? Created() : result.ToActionResult();
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
