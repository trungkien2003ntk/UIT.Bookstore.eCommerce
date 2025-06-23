using KKBookstore.Common.Interfaces;
using System.Net;

namespace KKBookstore.Middleware;

public class JwtBlacklistMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<JwtBlacklistMiddleware> _logger;

    public JwtBlacklistMiddleware(RequestDelegate next, ILogger<JwtBlacklistMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, ITokenBlacklistService tokenBlacklistService)
    {
        try
        {
            // Skip validation for certain paths
            if (ShouldSkipValidation(context.Request.Path))
            {
                await _next(context);
                return;
            }

            // Extract JWT token from Authorization header
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                await _next(context);
                return;
            }

            var token = authHeader.Substring("Bearer ".Length).Trim();
            if (string.IsNullOrEmpty(token))
            {
                await _next(context);
                return;
            }

            // Check if token is blacklisted
            var isBlacklisted = await tokenBlacklistService.IsTokenBlacklistedAsync(token);
            if (isBlacklisted)
            {
                _logger.LogWarning("Blacklisted token attempted to access {Path}", context.Request.Path);
                await HandleBlacklistedToken(context);
                return;
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in JWT blacklist middleware");
            // Continue to next middleware even if there's an error to avoid blocking valid requests
            await _next(context);
        }
    }

    private static bool ShouldSkipValidation(PathString path)
    {
        var pathsToSkip = new[]
        {
            "/api/auth/sign-in",
            "/api/auth/register",
            "/api/auth/refresh",
            "/health",
            "/swagger",
            "/api/products", // Public product endpoints
            "/api/banners"   // Public banner endpoints
        };

        return pathsToSkip.Any(skipPath => path.StartsWithSegments(skipPath, StringComparison.OrdinalIgnoreCase));
    }

    private static async Task HandleBlacklistedToken(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = 401,
            title = "Unauthorized",
            detail = "Token has been revoked",
            type = "https://tools.ietf.org/html/rfc7235#section-3.1"
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
