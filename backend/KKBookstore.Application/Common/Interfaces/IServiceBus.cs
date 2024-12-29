namespace KKBookstore.Application.Common.Interfaces;

public interface IServiceBus
{
    Task SendMessageAsync<T>(T message, string queueName) where T : class;
    Task SubscribeAsync<T>(string queueName, Func<T, Task> handler) where T : class;
}