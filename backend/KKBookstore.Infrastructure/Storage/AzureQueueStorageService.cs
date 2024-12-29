using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using KKBookstore.Application.Common.Interfaces;

namespace KKBookstore.Infrastructure.Storage;

internal class AzureQueueStorageService : IQueueStorageService
{
    private readonly QueueServiceClient _queueServiceClient;

    public AzureQueueStorageService(QueueServiceClient queueServiceClient)
    {
        _queueServiceClient = queueServiceClient;
    }


    public async Task SendMessageAsync(string queueName, string message)
    {
        var queueClient = _queueServiceClient.GetQueueClient(queueName);
        await queueClient.CreateIfNotExistsAsync();
        await queueClient.SendMessageAsync(message);
    }

    public async Task<string?> ReceiveMessageAsync(string queueName)
    {
        var queueClient = _queueServiceClient.GetQueueClient(queueName);
        await queueClient.CreateIfNotExistsAsync();

        QueueMessage[] messages = await queueClient.ReceiveMessagesAsync(maxMessages: 1);
        return messages.Length > 0 ? messages[0].MessageText : null;
    }

    public async Task DeleteMessageAsync(string queueName, string messageId)
    {
        var queueClient = _queueServiceClient.GetQueueClient(queueName);
        await queueClient.DeleteMessageAsync(messageId, popReceipt: null);
    }
}