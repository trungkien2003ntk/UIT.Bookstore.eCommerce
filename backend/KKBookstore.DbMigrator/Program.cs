using KKBookstore.DbMigrator.Seeders;
using KKBookstore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace KKBookstore.DbMigrator;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        _ = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
#if DEBUG
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
#else
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
#endif
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateLogger();

        try
        {
            Log.Information("Starting up the application");
            var host = CreateHostBuilder(args).Build();
            var migrator = host.Services.GetRequiredService<DbMigrator>();
            await migrator.MigrateAndSeedAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(delegate (HostBuilderContext context, IConfigurationBuilder builder)
            {
                var environment = context.HostingEnvironment.EnvironmentName;
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                builder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
                builder.AddJsonFile("appsettings.secrets.json", optional: true, reloadOnChange: true);
            })
            .ConfigureLogging((context, logging) => logging.ClearProviders())
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<KKBookstoreDbContext>(options =>
                {
                    options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                });
                services.AddTransient<DataSeeder>();
                services.AddTransient<DbMigrator>();
            });
}