using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace UpdateThumbnailUrlFunction;

public class UpdateThumbnailUrlFunction
{
    private readonly ILogger<UpdateThumbnailUrlFunction> _logger;
    private readonly IProductRepository _productRepository;

    public UpdateThumbnailUrlFunction(ILogger<UpdateThumbnailUrlFunction> logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    [Function(nameof(UpdateThumbnailUrlFunction))]
    public async Task Run(
        [ServiceBusTrigger("thumbnail-generated-queue", Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        _logger.LogInformation("Processing message: {MessageId}", message.MessageId);

        try
        {
            // Deserialize the event
            var thumbnailGeneratedEvent = JsonSerializer.Deserialize<ThumbnailGeneratedEvent>(message.Body.ToString());

            // Prepare the batch update data
            var imagesToUpdate = thumbnailGeneratedEvent.Images
                .Select(image => (image.ImageId, image.ThumbnailImageUrl))
                .ToList();

            // Update the ThumbnailImageUrls in the database
            await _productRepository.UpdateThumbnailImageUrlsAsync(thumbnailGeneratedEvent.ProductId, imagesToUpdate);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing message: {MessageId}", message.MessageId);
            throw;
        }
    }
}
