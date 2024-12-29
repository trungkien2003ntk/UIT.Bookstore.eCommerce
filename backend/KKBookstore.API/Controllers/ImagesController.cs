using KKBookstore.API.Abstractions;
using KKBookstore.Application.Features.Images.DeleteImage;
using KKBookstore.Application.Features.Images.UploadImages;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static KKBookstore.Application.Features.Images.UploadImages.UploadImagesCommand;

namespace KKBookstore.API.Controllers;


[Route("api/images")]
public class ImagesController(ISender sender) : ApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateImage(
        [FromForm] IList<IFormFile> images,
        CancellationToken cancellationToken = default
    )
    {
        var imageFiles = new List<ImageFile>();
        foreach (var image in images)
        {
            using var memoryStream = new MemoryStream();
            await image.CopyToAsync(memoryStream, cancellationToken);

            var imageFile = new ImageFile
            {
                Data = memoryStream.ToArray(),
                FileName = image.FileName,
                ContentType = image.ContentType
            };
            imageFiles.Add(imageFile);
        }

        // Create the command
        var command = new UploadImagesCommand
        {
            Files = imageFiles
        };

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteImage([FromQuery][Required] string fileName)
    {
        var result = await Sender.Send(new DeleteImageCommand(fileName));

        return result.IsSuccess ? NoContent() : ToActionResult(result);
    }
}
