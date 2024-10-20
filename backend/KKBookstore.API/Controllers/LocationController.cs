using KKBookstore.API.Abstractions;
using KKBookstore.Application.Features.Locations.GetCommuneList;
using KKBookstore.Application.Features.Locations.GetDistrictList;
using KKBookstore.Application.Features.Locations.GetProvinceList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers;

[Route("api/locations")]
public class LocationController(
    ISender sender
) : ApiController(sender)
{
    [HttpGet("province")]
    public async Task<IActionResult> GetChildDivisionListAsync(
        [FromQuery] GetProvinceListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("district")]
    public async Task<IActionResult> GetDistrictListAsync(
        [FromQuery] GetDistrictListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("commune")]
    public async Task<IActionResult> GetCommuneListAsync(
        [FromQuery] GetCommuneListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}
