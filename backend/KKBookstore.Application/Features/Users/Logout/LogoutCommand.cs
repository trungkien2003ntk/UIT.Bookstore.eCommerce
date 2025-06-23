using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Users.Logout;

public record LogoutCommand(string Token) : IRequest<Result>;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly ITokenBlacklistService _tokenBlacklistService;
    private readonly ILogger<LogoutCommandHandler> _logger;

    public LogoutCommandHandler(
        ITokenBlacklistService tokenBlacklistService,
        ILogger<LogoutCommandHandler> logger)
    {
        _tokenBlacklistService = tokenBlacklistService;
        _logger = logger;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return Result.Failure(Error.Validation("Token.Required", "Token is required"));
            }

            var result = await _tokenBlacklistService.BlacklistTokenAsync(request.Token, cancellationToken);

            if (result.IsSuccess)
            {
                _logger.LogInformation("User successfully logged out and token blacklisted");
            }
            else
            {
                _logger.LogWarning("Failed to blacklist token during logout: {Error}", result.Error.Description);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout process");
            return Result.Failure(Error.Failure("Logout.Failed", "An error occurred during logout"));
        }
    }
}
