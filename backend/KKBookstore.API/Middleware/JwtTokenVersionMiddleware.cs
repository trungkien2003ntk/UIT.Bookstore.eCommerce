using KKBookstore.Common.Interfaces;
using System.Security.Claims;

namespace KKBookstore.Middleware;

public class JwtTokenVersionMiddleware(RequestDelegate next, ILogger<JwtTokenVersionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<JwtTokenVersionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, ITokenVersionService tokenVersionService)
    {
        // Skip validation for non-authenticated requests or specific endpoints
        if (!context.User.Identity?.IsAuthenticated == true || ShouldSkipValidation(context))
        {
            await _next(context);
            return;
        }

        try
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? context.User.FindFirst("sub")?.Value;
            var tokenVersionClaim = context.User.FindFirst("ver")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(tokenVersionClaim))
            {
                _logger.LogWarning("Missing user ID or token version claim in JWT");
                await WriteUnauthorizedResponse(context, "Invalid token claims");
                return;
            }

            if (!int.TryParse(userIdClaim, out var userId))
            {
                _logger.LogWarning("Invalid user ID format in JWT: {UserIdClaim}", userIdClaim);
                await WriteUnauthorizedResponse(context, "Invalid user ID format");
                return;
            }

            var validationResult = await tokenVersionService.ValidateTokenVersionAsync(userId, tokenVersionClaim);
            if (validationResult.IsFailure)
            {
                _logger.LogWarning("Token version validation failed for user {UserId}: {Error}", userId, validationResult.Error.Description);
                await WriteUnauthorizedResponse(context, "Token validation failed");
                return;
            }

            if (!validationResult.Value)
            {
                _logger.LogInformation("Token version is invalid for user {UserId}, token has been invalidated", userId);
                await WriteUnauthorizedResponse(context, "Token has been invalidated");
                return;
            }

            // Token version is valid, continue with the request
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogError(ex, "Error in JWT token version validation middleware");
            await WriteUnauthorizedResponse(context, "Token validation error");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in JWT token version validation middleware");
            await _next(context);
        }
    }


    private static bool ShouldSkipValidation(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLowerInvariant();

        // Skip validation for authentication endpoints
        var skipPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "/api/authentication/signin",
            "/api/authentication/signup",
            "/api/authentication/refresh",
            "/api/authentication/forgot-password",
            "/api/authentication/reset-password",
            "/health",
            "/swagger"
        };

        return skipPaths.Any(skipPath => path?.StartsWith(skipPath) == true);
    }

    private static async Task WriteUnauthorizedResponse(HttpContext context, string message)
    {
        context.Response.StatusCode = 401;
        context.Response.ContentType = "application/json";

        var response = new
        {
            error = "Unauthorized",
            message,
            timestamp = DateTimeOffset.UtcNow
        };

        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}
