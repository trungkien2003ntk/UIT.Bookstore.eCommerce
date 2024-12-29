using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Text.Json;


namespace ProcessImageResizeFunction;

public class ResizeImageOnProductCreatedFunction
{
    private readonly ILogger<ResizeImageOnProductCreatedFunction> _logger;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly ServiceBusClient _serviceBusClient;

    public ResizeImageOnProductCreatedFunction(ILogger<ResizeImageOnProductCreatedFunction> logger, BlobServiceClient blobServiceClient, ServiceBusClient serviceBusClient)
    {
        _logger = logger;
        _blobServiceClient = blobServiceClient;
        _serviceBusClient = serviceBusClient;
    }

    [Function(nameof(ResizeImageOnProductCreatedFunction))]
    public async Task Run(
        [ServiceBusTrigger("product-created-queue", Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        const string thumbnailGeneratedQueueName = "thumbnail-generated-queue";
        const string imagesBlobContainerName = "images";

        _logger.LogInformation("Processing message: {MessageId}", message.MessageId);

        try
        {
            // Deserialize the event
            var productCreatedEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(message.Body.ToString());

            var thumbnailGeneratedEvent = new ThumbnailGeneratedEvent(productCreatedEvent.ProductId, []);

            foreach (var image in productCreatedEvent.Images)
            {
                var blobName = Path.GetFileName(image.ThumbnailImageUrl);

                // Get the blob client
                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(imagesBlobContainerName);
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                // Download the image
                var downloadResult = await blobClient.DownloadContentAsync();
                var imageStream = downloadResult.Value.Content.ToStream();

                // Resize the image (using SixLabors.ImageSharp or your preferred library)
                using var imageFromStream = await Image.LoadAsync(imageStream);
                var resizedImage = imageFromStream.Clone(ctx => ctx.Resize(150, 150));

                using var resizedImageStream = new MemoryStream();
                await resizedImage.SaveAsJpegAsync(resizedImageStream);
                resizedImageStream.Position = 0;

                // Upload the resized image
                var resizedBlobName = $"{Path.GetFileNameWithoutExtension(blobName)}_resized.jpg";
                var resizedBlobClient = blobContainerClient.GetBlobClient(resizedBlobName);
                await resizedBlobClient.UploadAsync(resizedImageStream);

                thumbnailGeneratedEvent.Images.Add(new ImageDto()
                {
                    ImageId = image.ImageId,
                    ThumbnailImageUrl = resizedBlobClient.Uri.ToString()
                });
            }

            // Publish a ThumbnailGenerated event
            await using var sender = _serviceBusClient.CreateSender(thumbnailGeneratedQueueName);
            var serviceBusMessage = new ServiceBusMessage(new BinaryData(JsonSerializer.Serialize(thumbnailGeneratedEvent)));
            await sender.SendMessageAsync(serviceBusMessage);

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
