using KKBookstore.API.Abstractions;
using KKBookstore.Application.Features.Users.ChangePassword;
using KKBookstore.Application.Features.Users.RefreshAccessToken;
using KKBookstore.Application.Features.Users.Register;
using KKBookstore.Application.Features.Users.RequestOtp;
using KKBookstore.Application.Features.Users.SignIn;
using KKBookstore.Application.Features.Users.UpdatePassword;
using KKBookstore.Application.Features.Users.VerifyOtp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers;

[Route("api/auth")]
public class AuthenticationController(ISender sender) : ApiController(sender)
{
    [HttpPost("request-otp")]
    public async Task<IActionResult> InitiateRegistrationAsync(
        [FromBody] RequestOtpCommand command,
        CancellationToken cancellationToken = default
    )
    {
        _ = await Sender.Send(command, cancellationToken);

        return Accepted();
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp(
        [FromBody] VerifyOtpCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
            [FromBody] RegisterCommand command,
            CancellationToken cancellationToken = default
    )
    {
        // Register user
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInAsync(
            [FromBody] SignInCommand command,
            CancellationToken cancellationToken = default
        )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAccessTokenAsync(
        [FromBody] RefreshAccessToken command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost("update-password")]
    public async Task<IActionResult> ResetPasswordAsync(
        [FromBody] UpdatePasswordCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    // need authorize
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePasswordAsync(
        [FromBody] ChangePasswordCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }
}
