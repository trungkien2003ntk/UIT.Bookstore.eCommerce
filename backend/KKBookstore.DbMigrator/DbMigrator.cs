﻿using KKBookstore.DbMigrator.Seeders;
using KKBookstore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace KKBookstore.DbMigrator;

internal class DbMigrator(IServiceProvider serviceProvider, IHostEnvironment webHostEnvironment)
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IHostEnvironment _hostEnvironment = webHostEnvironment;
    private DataSeeder? _seeder;

    public async Task MigrateAndSeedAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<KKBookstoreDbContext>();

        Log.Information("Migrating database associated with context {DbContextName}", typeof(KKBookstoreDbContext).Name);
        dbContext.Database.Migrate();
        await SeedDataAsync(dbContext);
    }

    private async Task SeedDataAsync(KKBookstoreDbContext dbContext)
    {
        Log.Information("Seeding initial data started");
        _seeder = new DataSeeder(dbContext);
        var env = _hostEnvironment.EnvironmentName;

        if (env == "Development")
            await SeedDevelopmentDataAsync(dbContext);
        else if (env == "Staging")
            await SeedStagingDataAsync(dbContext);
        else if (env == "Production")
            await SeedProductionDataAsync(dbContext);

        Log.Information("Seeding initial data completed");
    }

    private async Task SeedDevelopmentDataAsync(KKBookstoreDbContext dbContext)
    {
        // Seed development data here

        await _seeder!.SeedAsync();
    }

    private async Task SeedStagingDataAsync(KKBookstoreDbContext dbContext)
    {
        // Seed staging data here
        // Use json files to seed staging data

    }

    private async Task SeedProductionDataAsync(KKBookstoreDbContext dbContext)
    {
        // Seed production data here
        // Leave this empty for now
    }
}
