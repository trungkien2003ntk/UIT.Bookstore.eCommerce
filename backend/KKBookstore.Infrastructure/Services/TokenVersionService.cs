using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Services;

public class TokenVersionService(
    UserManager<User> userManager,
    IMemoryCache cache,
    ILogger<TokenVersionService> logger
) : ITokenVersionService
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IMemoryCache _cache = cache;
    private readonly ILogger<TokenVersionService> _logger = logger;

    private const string TOKEN_VERSION_CACHE_PREFIX = "token_version:";
    private const int CACHE_EXPIRATION_HOURS = 24;

    public async Task<Result<bool>> ValidateTokenVersionAsync(int userId, string tokenVersion, CancellationToken cancellationToken = default)
    {
        try
        {
            var currentVersionResult = await GetCurrentTokenVersionAsync(userId, cancellationToken);
            if (currentVersionResult.IsFailure)
            {
                return Result.Failure<bool>(currentVersionResult.Error);
            }

            var currentVersion = currentVersionResult.Value;

            if (!Guid.TryParse(tokenVersion, out var tokenVersionGuid))
            {
                _logger.LogWarning("Invalid token version format for user {UserId}: {TokenVersion}", userId, tokenVersion);
                return Result.Success(false);
            }

            var isValid = currentVersion == tokenVersionGuid;

            if (!isValid)
            {
                _logger.LogInformation("Token version mismatch for user {UserId}. Current: {Current}, Token: {Token}",
                    userId, currentVersion, tokenVersionGuid);
            }

            return Result.Success(isValid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating token version for user {UserId}", userId);
            return Result.Failure<bool>(Error.Failure("TokenVersion.ValidationFailed", "Failed to validate token version"));
        }
    }

    public async Task<Result<Guid>> GetCurrentTokenVersionAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var cacheKey = GetCacheKey(userId);

            // Try to get from cache first
            if (_cache.TryGetValue(cacheKey, out Guid cachedVersion))
            {
                return Result.Success(cachedVersion);
            }

            // If not in cache, get from database
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                _logger.LogWarning("User not found: {UserId}", userId);
                return Result.Failure<Guid>(Error.NotFound("User.NotFound", "User not found"));
            }

            // Cache the token version for future use
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(CACHE_EXPIRATION_HOURS),
                SlidingExpiration = TimeSpan.FromHours(6)
            };
            _cache.Set(cacheKey, user.TokenVersion, cacheOptions);

            _logger.LogDebug("Token version cached for user {UserId}: {TokenVersion}", userId, user.TokenVersion);

            return Result.Success(user.TokenVersion);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current token version for user {UserId}", userId);
            return Result.Failure<Guid>(Error.Failure("TokenVersion.RetrievalFailed", "Failed to retrieve current token version"));
        }
    }

    public async Task InvalidateTokenVersionCacheAsync(int userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var cacheKey = GetCacheKey(userId);
            _cache.Remove(cacheKey);

            _logger.LogDebug("Token version cache invalidated for user {UserId}", userId);

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error invalidating token version cache for user {UserId}", userId);
        }
    }

    public async Task PreloadTokenVersionsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Starting token version cache preload...");

            var users = _userManager.Users.ToList();
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(CACHE_EXPIRATION_HOURS),
                SlidingExpiration = TimeSpan.FromHours(6)
            };

            var preloadedCount = 0;
            foreach (var user in users)
            {
                if (cancellationToken.IsCancellationRequested)
                    break;

                var cacheKey = GetCacheKey(user.Id);
                _cache.Set(cacheKey, user.TokenVersion, cacheOptions);
                preloadedCount++;
            }

            _logger.LogInformation("Token version cache preload completed. Preloaded {Count} users.", preloadedCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during token version cache preload");
        }
    }

    private static string GetCacheKey(int userId) => $"{TOKEN_VERSION_CACHE_PREFIX}{userId}";
}
