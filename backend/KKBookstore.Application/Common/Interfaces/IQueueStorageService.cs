namespace KKBookstore.Application.Common.Interfaces;

public interface IQueueStorageService
{
    /// <summary>
    /// Sends a message to the queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="message">The message to send.</param>
    Task SendMessageAsync(string queueName, string message);

    /// <summary>
    /// Receives a message from the queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <returns>The received message, or null if no message is available.</returns>
    Task<string?> ReceiveMessageAsync(string queueName);

    /// <summary>
    /// Deletes a message from the queue.
    /// </summary>
    /// <param name="queueName">The name of the queue.</param>
    /// <param name="messageId">The ID of the message to delete.</param>
    Task DeleteMessageAsync(string queueName, string messageId);
}