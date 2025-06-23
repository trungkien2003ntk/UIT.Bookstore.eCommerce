using KKBookstore.Models;

namespace KKBookstore.Common.Interfaces;

public interface ITokenBlacklistService
{
    Task<Result> BlacklistTokenAsync(string token, CancellationToken cancellationToken = default);
    Task<bool> IsTokenBlacklistedAsync(string token, CancellationToken cancellationToken = default);
    Task LoadBlacklistedTokensAsync(CancellationToken cancellationToken = default);
    Task CleanupExpiredTokensAsync(CancellationToken cancellationToken = default);
}
