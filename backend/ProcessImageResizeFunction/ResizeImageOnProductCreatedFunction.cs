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
        _logger.LogInformation("Processing message: {MessageId}", message.MessageId);

        try
        {
            var productCreatedEvent = JsonSerializer.Deserialize<ProductCreatedEvent>(message.Body.ToString());

            if (productCreatedEvent is null || productCreatedEvent.Images is null)
            {
                _logger.LogWarning("Invalid message: {MessageId}", message.MessageId);
                return;
            }

            var thumbnailGeneratedEvent = await ProcessImagesAsync(productCreatedEvent);

            await PublishThumbnailGeneratedEventAsync(thumbnailGeneratedEvent);

            await messageActions.CompleteMessageAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing message: {MessageId}", message.MessageId);
            throw;
        }
    }

    private async Task<ThumbnailGeneratedEvent> ProcessImagesAsync(ProductCreatedEvent productCreatedEvent)
    {
        const string imagesBlobContainerName = "images";
        var thumbnailGeneratedEvent = new ThumbnailGeneratedEvent(productCreatedEvent.ProductId, []);

        foreach (var image in productCreatedEvent.Images)
        {
            var resizedImageUrl = await ResizeAndUploadImageAsync(imagesBlobContainerName, image);
            thumbnailGeneratedEvent.Images.Add(new ImageDto
            {
                ImageId = image.ImageId,
                ThumbnailImageUrl = resizedImageUrl
            });
        }

        return thumbnailGeneratedEvent;
    }

    private async Task<string> ResizeAndUploadImageAsync(string containerName, ImageDto image)
    {
        var blobName = Path.GetFileName(image.ThumbnailImageUrl);

        // Get the blob client
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        // Download the image
        var downloadResult = await blobClient.DownloadContentAsync();
        var imageStream = downloadResult.Value.Content.ToStream();

        // Resize the image
        var resizedImageStream = await ResizeImageAsync(imageStream, 150, 150);

        // Upload the resized image
        var resizedBlobName = $"{Path.GetFileNameWithoutExtension(blobName)}_resized.jpg";
        var resizedBlobClient = blobContainerClient.GetBlobClient(resizedBlobName);
        await resizedBlobClient.UploadAsync(resizedImageStream);

        return resizedBlobClient.Uri.ToString();
    }

    private async Task<Stream> ResizeImageAsync(Stream imageStream, int maxWidth, int maxHeight)
    {
        // Load the image
        using var image = await Image.LoadAsync(imageStream);

        // Calculate new dimensions while maintaining aspect ratio
        var (newWidth, newHeight) = CalculateNewDimensions(image.Width, image.Height, maxWidth, maxHeight);

        // Resize the image
        var resizedImage = image.Clone(ctx => ctx.Resize(newWidth, newHeight));

        // Save the resized image to a stream
        var resizedImageStream = new MemoryStream();
        await resizedImage.SaveAsJpegAsync(resizedImageStream);
        resizedImageStream.Position = 0;

        return resizedImageStream;
    }

    private (int Width, int Height) CalculateNewDimensions(int originalWidth, int originalHeight, int maxWidth, int maxHeight)
    {
        var ratioX = (double)maxWidth / originalWidth;
        var ratioY = (double)maxHeight / originalHeight;
        var ratio = Math.Min(ratioX, ratioY); // Use the smaller ratio to maintain aspect ratio

        var newWidth = (int)(originalWidth * ratio);
        var newHeight = (int)(originalHeight * ratio);

        return (newWidth, newHeight);
    }

    private async Task PublishThumbnailGeneratedEventAsync(ThumbnailGeneratedEvent thumbnailGeneratedEvent)
    {
        const string thumbnailGeneratedQueueName = "thumbnail-generated-queue";

        await using var sender = _serviceBusClient.CreateSender(thumbnailGeneratedQueueName);
        var serviceBusMessage = new ServiceBusMessage(new BinaryData(JsonSerializer.Serialize(thumbnailGeneratedEvent)));
        await sender.SendMessageAsync(serviceBusMessage);
    }
}
