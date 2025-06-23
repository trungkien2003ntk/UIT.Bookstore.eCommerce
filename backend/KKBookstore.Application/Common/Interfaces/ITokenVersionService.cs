using KKBookstore.Models;

namespace KKBookstore.Common.Interfaces;

public interface ITokenVersionService
{
    /// <summary>
    /// Validates if the token version claim matches the user's current token version
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <param name="tokenVersion">The token version from the JWT claim</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if token version is valid, false otherwise</returns>
    Task<Result<bool>> ValidateTokenVersionAsync(int userId, string tokenVersion, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current token version for a user (from cache or database)
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The current token version</returns>
    Task<Result<Guid>> GetCurrentTokenVersionAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Invalidates the cached token version for a user (when token version is updated)
    /// </summary>
    /// <param name="userId">The user ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task InvalidateTokenVersionCacheAsync(int userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Preloads all token versions into cache on application startup
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    Task PreloadTokenVersionsAsync(CancellationToken cancellationToken = default);
}
