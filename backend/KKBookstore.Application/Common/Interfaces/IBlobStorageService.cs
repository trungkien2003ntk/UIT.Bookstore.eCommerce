using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Common.Interfaces;

public interface IBlobStorageService
{
    /// <summary>
    /// Uploads a file to the blob storage.
    /// </summary>
    /// <param name="containerName">The name of the container/bucket.</param>
    /// <param name="fileName">The name of the file to be uploaded.</param>
    /// <param name="fileData">The file data as a stream.</param>
    /// <param name="contentType">The MIME type of the file (e.g., "image/jpeg").</param>
    /// <returns>The URL of the uploaded file.</returns>
    Task<string> UploadFileAsync(string containerName, string fileName, Stream fileData, string contentType);

    /// <summary>
    /// Deletes a file from the blob storage.
    /// </summary>
    /// <param name="containerName">The name of the container/bucket.</param>
    /// <param name="fileName">The name of the file to be deleted.</param>
    /// <returns>True if the file was deleted successfully, otherwise false.</returns>
    Task<bool> DeleteFileAsync(string containerName, string fileName);

    /// <summary>
    /// Retrieves metadata for a file in the blob storage.
    /// </summary>
    /// <param name="containerName">The name of the container/bucket.</param>
    /// <param name="fileName">The name of the file.</param>
    /// <returns>The file metadata, including size, content type, and last modified date.</returns>
    Task<BlobMetadata> GetFileMetadataAsync(string containerName, string fileName);
}