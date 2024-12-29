using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;

namespace KKBookstore.Infrastructure.Storage;

internal class AzureBlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public AzureBlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<string> UploadFileAsync(string containerName, string fileName, Stream fileData, string contentType)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileData, new BlobHttpHeaders { ContentType = contentType });

        return blobClient.Uri.ToString();
    }

    public async Task<bool> DeleteFileAsync(string containerName, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        return await blobClient.DeleteIfExistsAsync();
    }

    public async Task<BlobMetadata> GetFileMetadataAsync(string containerName, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        var properties = await blobClient.GetPropertiesAsync();

        return new BlobMetadata
        {
            FileName = fileName,
            Size = properties.Value.ContentLength,
            ContentType = properties.Value.ContentType,
            LastModified = properties.Value.LastModified.DateTime
        };
    }
}