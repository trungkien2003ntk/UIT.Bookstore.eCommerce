using KKBookstore.Application.Common.Constants;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KKBookstore.Application.Features.Images.UploadImages;

public record UploadImagesCommand
    : IRequest<Result<UploadImagesResponse>>
{
    [Required]
    public List<ImageFile> Files { get; init; } = [];

    public record ImageFile
    {
        public byte[] Data { get; init; } = []; // File data
        public string FileName { get; init; } = null!;
        public string ContentType { get; init; } = null!;
    }
}

public class UploadImagesCommandHandler : IRequestHandler<UploadImagesCommand, Result<UploadImagesResponse>>
{
    private readonly IBlobStorageService blobStorageService;

    public UploadImagesCommandHandler(IBlobStorageService blobStorageService)
    {
        this.blobStorageService = blobStorageService;
    }

    public async Task<Result<UploadImagesResponse>> Handle(UploadImagesCommand request, CancellationToken cancellationToken)
    {
        var imageUrls = new List<string>();
        foreach (var file in request.Files)
        {
            using var fileStream = new MemoryStream(file.Data);
            var imageUrl = await blobStorageService.UploadFileAsync(
                StorageConsts.ImageContainerName,
                file.FileName,
                fileStream,
                file.ContentType
            );

            imageUrls.Add(imageUrl);
        }

        return Result.Success(new UploadImagesResponse { ImageUrls = imageUrls });
    }
}