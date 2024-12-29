using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddSingleton(provider =>
        {
            var connectionString = Environment.GetEnvironmentVariable("BlobStorageConnectionString");

            return new BlobServiceClient(connectionString);
        });
        services.AddSingleton(providedr =>
        {
            var connectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");

            return new ServiceBusClient(connectionString);
        });
    })
    .Build();

host.Run();
