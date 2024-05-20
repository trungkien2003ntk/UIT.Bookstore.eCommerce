using KKBookstore.API.Extensions;
using KKBookstore.Application.Users.Commands.Register;
using KKBookstore.Application.Users.Commands.InitiateRegistration;
using KKBookstore.Application.Users.Commands.RefreshAccessToken;
using KKBookstore.Application.Users.Commands.SignIn;
using KKBookstore.Application.Users.Commands.VerifyOtp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KKBookstore.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController(
    IMediator mediator
) : ControllerBase
{
    [HttpPost("request-otp")]
    public async Task<IActionResult> InitiateRegistrationAsync(
        [FromBody] InitiateRegistrationCommand initiateRegistrationCommand,
        CancellationToken cancellationToken = default
    )
    {
        var result = await mediator.Send(initiateRegistrationCommand, cancellationToken);

        return result.IsSuccess? Ok() : result.ToActionResult();
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp(
        [FromBody] VerifyOtpCommand verifyOtpCommand,
        CancellationToken cancellationToken = default
    )
    {
        var result = await mediator.Send(verifyOtpCommand, cancellationToken);

        return result.IsSuccess? Ok(result.Value) : result.ToActionResult();
    }

    [Authorize]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
            [FromBody] RegisterCommand createUserCommand,
            CancellationToken cancellationToken = default
    )
    {
        // Register user
        var result = await mediator.Send(createUserCommand, cancellationToken);

        return result.IsSuccess ?
            CreatedAtAction(
                nameof(UsersController.GetUserAsync),
                new { id = result.Value.Id },
                result.Value
            ) :
            result.ToActionResult();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignInAsync(
            [FromBody] SignInCommand signInCommand,
            CancellationToken cancellationToken = default
        )
    {
        var result = await mediator.Send(signInCommand, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAccessTokenAsync(
        [FromBody] RefreshAccessTokenCommand refreshAccessTokenCommand,
        CancellationToken cancellationToken = default
    )
    {
        var result = await mediator.Send(refreshAccessTokenCommand, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToActionResult();
    }
}
