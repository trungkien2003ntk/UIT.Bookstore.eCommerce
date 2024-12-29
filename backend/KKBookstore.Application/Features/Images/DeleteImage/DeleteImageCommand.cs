using KKBookstore.Application.Common.Constants;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Application.Features.Images.DeleteImage;

public record DeleteImageCommand(string imageName)
    : IRequest<Result>;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Result>
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly ILogger<DeleteImageCommandHandler> _logger;

    public DeleteImageCommandHandler(IBlobStorageService blobStorageService, ILogger<DeleteImageCommandHandler> logger)
    {
        _blobStorageService = blobStorageService;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Deleting image: {ImageName}", request.imageName);

            var deleteResult = await _blobStorageService.DeleteFileAsync(StorageConsts.ImageContainerName, request.imageName);

            if (deleteResult)
            {
                _logger.LogInformation("Image deleted: {ImageName}", request.imageName);
                return Result.Success();
            }

            return Result.Failure(Error.NotFound("ImageNotFound", "Image was not found."));
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Failure("ImageDeleteError", $"An error occurred while deleting the image: {ex}"));
        }
    }
}