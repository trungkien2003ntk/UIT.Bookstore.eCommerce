using KKBookstore.Abstractions;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Locations.GetCommuneList;
using KKBookstore.Features.Locations.GetDistrictList;
using KKBookstore.Features.Locations.GetProvinceList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/locations")]
public class LocationController(
    ISender sender,
    IGeoCoordService geoCoordService
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

    [HttpPost("geocode")]
    public async Task<IActionResult> GetCoordinatesAsync(
        [FromBody] GeocodeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var result = await geoCoordService.GetCoordinatesAsync(request.Address, cancellationToken);

        return result.Success ? Ok(result) : BadRequest(result);
    }
}

public class GeocodeRequest
{
    public string Address { get; set; } = string.Empty;
}
