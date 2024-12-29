using Azure.Messaging.ServiceBus;
using KKBookstore.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KKBookstore.Infrastructure.Storage;

public class AzureServiceBus : IServiceBus
{
    private readonly string _connectionString;

    public AzureServiceBus(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AzureServiceBus")!;
    }

    public async Task SendMessageAsync<T>(T message, string queueName) where T : class
    {
        await using var client = new ServiceBusClient(_connectionString);
        var sender = client.CreateSender(queueName);
        var serviceBusMessage = new ServiceBusMessage(new BinaryData(message));
        await sender.SendMessageAsync(serviceBusMessage);
    }

    public async Task SubscribeAsync<T>(string queueName, Func<T, Task> handler) where T : class
    {
        await using var client = new ServiceBusClient(_connectionString);
        var processor = client.CreateProcessor(queueName);

        processor.ProcessMessageAsync += async args =>
        {
            var message = args.Message.Body.ToObjectFromJson<T>();
            await handler(message);
            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine($"Error processing message: {args.Exception}");
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();
    }
}