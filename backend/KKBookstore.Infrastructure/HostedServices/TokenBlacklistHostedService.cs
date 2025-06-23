using KKBookstore.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KKBookstore.HostedServices;

public class TokenBlacklistHostedService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TokenBlacklistHostedService> _logger;
    private readonly TimeSpan _cleanupInterval = TimeSpan.FromHours(6); // Cleanup every 6 hours

    public TokenBlacklistHostedService(
        IServiceProvider serviceProvider,
        ILogger<TokenBlacklistHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TokenBlacklistHostedService starting...");

        // Load blacklisted tokens into memory cache on startup
        using var scope = _serviceProvider.CreateScope();
        var tokenBlacklistService = scope.ServiceProvider.GetRequiredService<ITokenBlacklistService>();
        await tokenBlacklistService.LoadBlacklistedTokensAsync(cancellationToken);

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("TokenBlacklistHostedService background task started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(_cleanupInterval, stoppingToken);

                using var scope = _serviceProvider.CreateScope();
                var tokenBlacklistService = scope.ServiceProvider.GetRequiredService<ITokenBlacklistService>();

                await tokenBlacklistService.CleanupExpiredTokensAsync(stoppingToken);

                _logger.LogDebug("Token blacklist cleanup completed");
            }
            catch (OperationCanceledException)
            {
                // Expected when cancellation is requested
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during token blacklist cleanup");
                // Continue running even if cleanup fails
            }
        }

        _logger.LogInformation("TokenBlacklistHostedService background task stopped");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("TokenBlacklistHostedService stopping...");
        await base.StopAsync(cancellationToken);
    }
}
