using KKBookstore.Abstractions;
using KKBookstore.Contracts.Requests.Auth;
using KKBookstore.Contracts.Requests.Users;
using KKBookstore.Features.Authentication;
using KKBookstore.Features.Users.ChangePassword;
using KKBookstore.Features.Users.RefreshAccessToken;
using KKBookstore.Features.Users.Register;
using KKBookstore.Features.Users.RequestOtp;
using KKBookstore.Features.Users.RequestPasswordReset;
using KKBookstore.Features.Users.ResetPassword;
using KKBookstore.Features.Users.SignIn;
using KKBookstore.Features.Users.VerifyOtp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KKBookstore.Controllers;

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

        return result.IsSuccess ? NoContent() : ToActionResult(result);
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

    [HttpPost("request-password-reset")]
    public async Task<IActionResult> RequestPasswordResetAsync(
        [FromBody] RequestPasswordResetCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return NoContent();
    }


    [HttpPost("{userId}/reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(
        int userId,
        [FromBody] ResetPasswordRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var command = new ResetPasswordCommand(userId, request.Token, request.NewPassword);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }

    // need authorize
    [Authorize]
    [HttpPost("{userId}/change-password")]
    public async Task<IActionResult> ChangePasswordAsync(
        int userId,
        [FromBody] ChangePasswordRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var userIdfromToken = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!);

        if (userIdfromToken != userId)
        {
            return BadRequest();
        }

        var command = new ChangePasswordCommand(userId, request.Email, request.CurrentPassword, request.NewPassword);
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("send-sign-up-email")]
    public async Task<IActionResult> SendSignUpEmailAsync(
        [FromBody] SendSignUpEmailCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUpAsync(
        [FromBody] SignUpCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}
