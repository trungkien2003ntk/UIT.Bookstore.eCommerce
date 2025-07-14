using KKBookstore.Abstractions;
using KKBookstore.Contracts.Requests.Admin;
using KKBookstore.Features.Admin.ApproveRating;
using KKBookstore.Features.Admin.BulkEvaluateRatings;
using KKBookstore.Features.Admin.GetModerationQueue;
using KKBookstore.Features.Admin.GetModerationSettings;
using KKBookstore.Features.Admin.HideRating;
using KKBookstore.Features.Admin.RestoreRating;
using KKBookstore.Features.Admin.TestCommentModeration;
using KKBookstore.Features.Admin.UpdateModerationLevel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KKBookstore.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Staff")]
public class AdminController : ApiController
{
    public AdminController(ISender sender) : base(sender)
    {
    }

    /// <summary>
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

    /// <summary>
    /// Get current moderation settings
    /// </summary>
    /// <returns>Current moderation configuration</returns>
    [HttpGet("moderation/settings")]
    public async Task<IActionResult> GetModerationSettings()
    {
        var query = new GetModerationSettingsQuery();
        var result = await Sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Update moderation level
    /// </summary>
    /// <param name="request">Moderation level update request</param>
    /// <returns>Success result</returns>
    [HttpPost("moderation/level")]
    public async Task<IActionResult> UpdateModerationLevel([FromBody] UpdateModerationLevelRequest request)
    {
        var command = new UpdateModerationLevelCommand(request.Level);
        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok() : ToActionResult(result);
    }

    /// <summary>
    /// Test if a comment will be hidden at the current moderation level
    /// </summary>
    /// <param name="request">Test comment moderation request</param>
    /// <returns>Moderation test result</returns>
    [HttpPost("moderation/test-comment")]
    public async Task<IActionResult> TestCommentModeration([FromBody] TestCommentModerationRequest request)
    {
        var query = new TestCommentModerationQuery(request.Comment, request.ProductId, request.Language);
        var result = await Sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }

    /// <summary>
    /// Bulk evaluate all existing ratings with AI moderation (one-time operation)
    /// </summary>
    /// <param name="request">Bulk evaluation request</param>
    /// <returns>Evaluation results summary</returns>
    [HttpPost("ratings/bulk-evaluate")]
    public async Task<IActionResult> BulkEvaluateRatings([FromBody] BulkEvaluateRatingsRequest request)
    {
        var command = new BulkEvaluateRatingsCommand(request.DryRun);
        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : ToActionResult(result);
    }
}

// Request DTOs
public record ApproveRatingRequest(string? AdminNote = null);
public record HideRatingRequest(string Reason);
public record RestoreRatingRequest(string? AdminNote = null);
public record BulkEvaluateRatingsRequest(bool DryRun = false);
