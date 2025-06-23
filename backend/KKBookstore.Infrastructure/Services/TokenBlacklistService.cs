using KKBookstore.Common.Interfaces;
using KKBookstore.Identity;
using KKBookstore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace KKBookstore.Infrastructure.Services;

public class TokenBlacklistService : ITokenBlacklistService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<TokenBlacklistService> _logger;
    private const string BLACKLISTED_TOKEN_KEY_PREFIX = "blacklisted_token_";

    public TokenBlacklistService(
        IApplicationDbContext dbContext,
        IMemoryCache memoryCache,
        ILogger<TokenBlacklistService> logger)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<Result> BlacklistTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return Result.Failure(Error.Validation("Token.Invalid", "Token cannot be null or empty"));
            }

            // Extract expiration from JWT token
            var expireAt = GetTokenExpiration(token);
            if (expireAt <= DateTimeOffset.UtcNow)
            {
                return Result.Failure(Error.Validation("Token.Expired", "Token has already expired"));
            }

            // Check if token is already blacklisted
            if (await IsTokenBlacklistedAsync(token, cancellationToken))
            {
                return Result.Success(); // Already blacklisted, consider it successful
            }

            // Add to database
            var blacklistedToken = BlacklistedToken.Create(token, expireAt);
            await _dbContext.BlacklistedTokens.AddAsync(blacklistedToken, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Add to memory cache with expiration
            var cacheKey = BLACKLISTED_TOKEN_KEY_PREFIX + token;
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = expireAt,
                Priority = CacheItemPriority.Normal
            };
            _memoryCache.Set(cacheKey, true, cacheOptions);

            _logger.LogInformation("Token successfully blacklisted and cached");
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error blacklisting token");
            return Result.Failure(Error.Failure("Token.BlacklistFailed", "Failed to blacklist token"));
        }
    }

    public async Task<bool> IsTokenBlacklistedAsync(string token, CancellationToken cancellationToken = default)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            // First check memory cache
            var cacheKey = BLACKLISTED_TOKEN_KEY_PREFIX + token;
            if (_memoryCache.TryGetValue(cacheKey, out _))
            {
                return true;
            }

            // Fallback to database
            var isBlacklisted = await _dbContext.BlacklistedTokens
                .AnyAsync(bt => bt.Token == token && bt.ExpireAt > DateTimeOffset.UtcNow, cancellationToken);

            // If found in database but not in cache, add to cache
            if (isBlacklisted)
            {
                var expireAt = await _dbContext.BlacklistedTokens
                    .Where(bt => bt.Token == token)
                    .Select(bt => bt.ExpireAt)
                    .FirstOrDefaultAsync(cancellationToken);

                if (expireAt > DateTimeOffset.UtcNow)
                {
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = expireAt,
                        Priority = CacheItemPriority.Normal
                    };
                    _memoryCache.Set(cacheKey, true, cacheOptions);
                }
            }

            return isBlacklisted;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if token is blacklisted");
            // In case of error, be conservative and return false to avoid blocking valid tokens
            return false;
        }
    }

    public async Task LoadBlacklistedTokensAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var currentTime = DateTimeOffset.UtcNow;
            var validBlacklistedTokens = await _dbContext.BlacklistedTokens
                .Where(bt => bt.ExpireAt > currentTime)
                .ToListAsync(cancellationToken);

            foreach (var blacklistedToken in validBlacklistedTokens)
            {
                var cacheKey = BLACKLISTED_TOKEN_KEY_PREFIX + blacklistedToken.Token;
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = blacklistedToken.ExpireAt,
                    Priority = CacheItemPriority.Normal
                };
                _memoryCache.Set(cacheKey, true, cacheOptions);
            }

            _logger.LogInformation("Loaded {Count} blacklisted tokens into memory cache", validBlacklistedTokens.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading blacklisted tokens into memory cache");
        }
    }

    public async Task CleanupExpiredTokensAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var currentTime = DateTimeOffset.UtcNow;
            var expiredTokens = await _dbContext.BlacklistedTokens
                .Where(bt => bt.ExpireAt <= currentTime)
                .ToListAsync(cancellationToken);

            if (expiredTokens.Any())
            {
                _dbContext.BlacklistedTokens.RemoveRange(expiredTokens);
                await _dbContext.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Cleaned up {Count} expired blacklisted tokens", expiredTokens.Count);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cleaning up expired blacklisted tokens");
        }
    }

    private static DateTimeOffset GetTokenExpiration(string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            
            if (jsonToken.ValidTo == DateTime.MinValue)
            {
                // If no expiration, set a default expiration (e.g., 1 day from now)
                return DateTimeOffset.UtcNow.AddDays(1);
            }

            return new DateTimeOffset(jsonToken.ValidTo);
        }
        catch
        {
            // If unable to parse token, set a default expiration
            return DateTimeOffset.UtcNow.AddDays(1);
        }
    }
}
