using KKBookstore.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KKBookstore.HostedServices;

public class TokenVersionPreloadHostedService(
    IServiceProvider serviceProvider,
    ILogger<TokenVersionPreloadHostedService> logger
) : BackgroundService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger<TokenVersionPreloadHostedService> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("TokenVersionPreloadHostedService starting...");

        // Wait a bit for the application to start up
        await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var tokenVersionService = scope.ServiceProvider.GetRequiredService<ITokenVersionService>();

            await tokenVersionService.PreloadTokenVersionsAsync(stoppingToken);

            _logger.LogInformation("TokenVersionPreloadHostedService completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in TokenVersionPreloadHostedService");
        }
    }
}
