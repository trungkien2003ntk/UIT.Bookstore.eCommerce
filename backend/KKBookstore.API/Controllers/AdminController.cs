using KKBookstore.Abstractions;
using KKBookstore.Features.Admin.ApproveRating;
using KKBookstore.Features.Admin.GetModerationQueue;
using KKBookstore.Features.Admin.HideRating;
using KKBookstore.Features.Admin.RestoreRating;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Staff")]
public class AdminController : ApiController
{
    public AdminController(ISender sender) : base(sender)
    {
    }    /// <summary>
    /// Get the moderation queue for admin review
    /// </summary>
    /// <param name="query">Query parameters</param>
    /// <returns>List of ratings pending moderation</returns>
    [HttpGet("moderation-queue")]
    public async Task<IActionResult> GetModerationQueue([FromQuery] GetModerationQueueQuery query)
    {
        var result = await Sender.Send(query);
        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Manually approve a rating
    /// </summary>
    /// <param name="command">Approve rating command</param>
    /// <returns>Success result</returns>
    [HttpPost("ratings/{ratingId}/approve")]
    public async Task<IActionResult> ApproveRating(int ratingId, [FromBody] ApproveRatingRequest request)
    {
        var command = new ApproveRatingCommand(ratingId, request.AdminNote);
        var result = await Sender.Send(command);
        
        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    /// <summary>
    /// Manually hide a rating
    /// </summary>
    /// <param name="command">Hide rating command</param>
    /// <returns>Success result</returns>
    [HttpPost("ratings/{ratingId}/hide")]
    public async Task<IActionResult> HideRating(int ratingId, [FromBody] HideRatingRequest request)
    {
        var command = new HideRatingCommand(ratingId, request.Reason);
        var result = await Sender.Send(command);
        
        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    /// <summary>
    /// Restore a hidden rating
    /// </summary>
    /// <param name="command">Restore rating command</param>
    /// <returns>Success result</returns>
    [HttpPost("ratings/{ratingId}/restore")]
    public async Task<IActionResult> RestoreRating(int ratingId, [FromBody] RestoreRatingRequest request)
    {
        var command = new RestoreRatingCommand(ratingId, request.AdminNote);
        var result = await Sender.Send(command);
        
        return result.IsSuccess ? Ok() : ToActionResult(result);
    }
}

// Request DTOs
public record ApproveRatingRequest(string? AdminNote = null);
public record HideRatingRequest(string Reason);
public record RestoreRatingRequest(string? AdminNote = null);
