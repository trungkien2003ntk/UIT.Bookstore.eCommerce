using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Admin.Services;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Admin.BulkEvaluateRatings;

public record BulkEvaluateRatingsCommand(bool DryRun = false) : IRequest<Result<BulkEvaluateRatingsResponse>>;

public class BulkEvaluateRatingsCommandHandler : IRequestHandler<BulkEvaluateRatingsCommand, Result<BulkEvaluateRatingsResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICommentModerationService _moderationService;
    private readonly IModerationNotificationService _notificationService;
    private readonly ILogger<BulkEvaluateRatingsCommandHandler> _logger;

    public BulkEvaluateRatingsCommandHandler(
        IApplicationDbContext context,
        ICommentModerationService moderationService,
        IModerationNotificationService notificationService,
        ILogger<BulkEvaluateRatingsCommandHandler> logger)
    {
        _context = context;
        _moderationService = moderationService;
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task<Result<BulkEvaluateRatingsResponse>> Handle(BulkEvaluateRatingsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting bulk rating evaluation. DryRun: {DryRun}", request.DryRun);

        try
        {
            // Get all ratings that haven't been AI moderated yet
            var ratingsToEvaluate = await _context.Ratings
                .Where(r => !r.IsAiModerated)
                .Include(r => r.ProductVariant)
                .Include(r => r.Customer)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Found {Count} ratings to evaluate", ratingsToEvaluate.Count());

            var response = new BulkEvaluateRatingsResponse
            {
                TotalRatings = ratingsToEvaluate.Count(),
                ProcessedRatings = 0,
                HiddenRatings = 0,
                ErrorCount = 0,
                DryRun = request.DryRun
            };

            if (ratingsToEvaluate.Count() == 0)
            {
                _logger.LogInformation("No ratings found that need AI moderation");
                return Result.Success(response);
            }

            var hiddenRatings = new List<Rating>();

            foreach (var rating in ratingsToEvaluate)
            {
                try
                {
                    _logger.LogDebug("Evaluating rating {RatingId}", rating.Id);

                    // Perform AI moderation with context
                    var moderationResult = await _moderationService.EvaluateCommentWithContextAsync(
                        rating.Comment!,
                        rating.ProductId,
                        "vi");

                    if (moderationResult.Success)
                    {
                        // Update rating with AI moderation results
                        rating.AiModerationScore = moderationResult.BadnessScore;
                        rating.AiModerationCategory = moderationResult.Category;
                        rating.AiModerationExplanation = moderationResult.Explanation;
                        rating.AiModerationDate = DateTimeOffset.UtcNow;
                        rating.IsAiModerated = true;
                        rating.SentimentScore = moderationResult.SentimentScore;
                        rating.SentimentLabel = moderationResult.SentimentLabel;

                        // Check if rating should be auto-hidden based on badness score
                        // You may need to get the current moderation configuration to determine threshold
                        if (moderationResult.BadnessScore >= 70) // Using medium threshold as default
                        {
                            rating.Status = RatingStatus.Hidden;
                            hiddenRatings.Add(rating);
                            response.HiddenRatings++;
                        }

                        response.ProcessedRatings++;

                        if (!request.DryRun)
                        {
                            // Create audit log entry
                            var auditLog = new ModerationAuditLog(
                                rating.Id,
                                "AI_BULK_EVALUATION",
                                $"Bulk evaluation - Score: {moderationResult.BadnessScore}, Category: {moderationResult.Category}",
                                null, // System action
                                moderationResult.BadnessScore
                            );

                            _context.ModerationAuditLogs.Add(auditLog);
                        }
                    }
                    else
                    {
                        _logger.LogError("Failed to moderate rating {RatingId}: {Error}", rating.Id, moderationResult.ErrorMessage);
                        response.ErrorCount++;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error evaluating rating {RatingId}", rating.Id);
                    response.ErrorCount++;
                }
            }

            if (!request.DryRun)
            {
                // Save changes to database
                await _context.SaveChangesAsync(cancellationToken);

                // Send notifications for hidden ratings
                foreach (var hiddenRating in hiddenRatings)
                {
                    try
                    {
                        await _notificationService.NotifyAdminOfAutoHiddenRatingAsync(
                            hiddenRating.Id,
                            hiddenRating.Comment,
                            hiddenRating.AiModerationScore ?? 0);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to send notification for hidden rating {RatingId}", hiddenRating.Id);
                    }
                }

                _logger.LogInformation("Bulk evaluation completed. Processed: {Processed}, Hidden: {Hidden}, Errors: {Errors}",
                    response.ProcessedRatings, response.HiddenRatings, response.ErrorCount);
            }
            else
            {
                _logger.LogInformation("Bulk evaluation dry run completed. Would process: {Processed}, Would hide: {Hidden}, Errors: {Errors}",
                    response.ProcessedRatings, response.HiddenRatings, response.ErrorCount);
            }

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Bulk rating evaluation failed");
            return Result.Failure<BulkEvaluateRatingsResponse>(Error.Failure(
                "BulkEvaluateRatings.Failed",
                "An error occurred while evaluating ratings"));
        }
    }
}