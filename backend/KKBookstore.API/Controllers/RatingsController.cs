using KKBookstore.Abstractions;
using KKBookstore.Features.Ratings.GetRatingDetail;
using KKBookstore.Features.Ratings.GetRatingList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[Route("api/ratings")]
public class RatingsController(
    ISender sender
) : ApiController(sender)
{
    [HttpGet]
    public virtual async Task<IActionResult> GetListAsync(
        [FromQuery] GetRatingListQuery query,
        CancellationToken cancellationToken = default
    )
    {
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRatingDetail(
        int id,
        CancellationToken cancellationToken = default
    )
    {
        var query = new GetRatingDetailQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}
