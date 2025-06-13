using KKBookstore.Abstractions;
using KKBookstore.Features.Banners.CreateBanner;
using KKBookstore.Features.Banners.DeleteBanner;
using KKBookstore.Features.Banners.GetBannerDetail;
using KKBookstore.Features.Banners.GetBannerList;
using KKBookstore.Features.Banners.ToggleBannerStatus;
using KKBookstore.Features.Banners.UpdateBanner;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/banners")]
public class BannerController(
    ISender sender
) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetBanners(
        [FromQuery] GetBannerListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBannerDetail(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new GetBannerDetailQuery(id), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBanner(
        [FromBody] CreateBannerCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBanner(
        [FromRoute] int id,
        [FromBody] UpdateBannerCommand command,
        CancellationToken cancellationToken = default
    )
    {
        var commandWithId = command with { Id = id };
        var result = await Sender.Send(commandWithId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpPatch("{id}/toggle-status")]
    public async Task<IActionResult> ToggleBannerStatus(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var command = new ToggleBannerStatusCommand(id);
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBanner(
        [FromRoute] int id,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(new DeleteBannerCommand(id), cancellationToken);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }
}
