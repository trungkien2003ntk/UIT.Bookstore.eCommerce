using KKBookstore.Common.Configuration;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Admin.Services;
using KKBookstore.Features.Products.Models;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KKBookstore.Features.Products.ReportProductRating;

public record ReportProductRatingCommand(int RatingId, int CustomerId, string Reason) : IRequest<Result<ProductRatingDto>>
{
}

public class ReportProductRatingCommandHandler : IRequestHandler<ReportProductRatingCommand, Result<ProductRatingDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICommentModerationService _moderationService;
    private readonly IModerationNotificationService _notificationService;
    private readonly ILogger<ReportProductRatingCommandHandler> _logger;
    private readonly ModerationConfiguration _config;

    public ReportProductRatingCommandHandler(
        IApplicationDbContext dbContext,
        ICommentModerationService moderationService,
        IModerationNotificationService notificationService,
        ILogger<ReportProductRatingCommandHandler> logger,
        IOptions<ModerationConfiguration> config)
    {
        _dbContext = dbContext;
        _moderationService = moderationService;
        _notificationService = notificationService;
        _logger = logger;
        _config = config.Value;
    }
    public async Task<Result<ProductRatingDto>> Handle(ReportProductRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await _dbContext.Ratings
            .Include(r => r.ProductVariant)
                .ThenInclude(v => v.ProductVariantOptionValues)!
                    .ThenInclude(x => x.OptionValue)
            .Include(r => r.Customer)
            .Include(r => r.Reports)
            .Include(r => r.Likes)
            .FirstOrDefaultAsync(r => r.Id == request.RatingId, cancellationToken);
        if (rating == null)
        {
            return Result.Failure<ProductRatingDto>(ProductErrors.RatingNotFound);
        }

        var reportResult = rating.ReportedBy(request.CustomerId, request.Reason);
        if (reportResult.IsFailure)
        {
            return Result.Failure<ProductRatingDto>(reportResult.Error);
        }

        // Check if we've reached the AI moderation threshold
        if (_config.IsEnabled &&
            rating.ReportsCount >= _config.ReportThresholdForAiModeration &&
            !rating.IsAiModerated &&
            !string.IsNullOrWhiteSpace(rating.Comment))
        {
            _logger.LogInformation("Triggering AI moderation for rating {RatingId} with {ReportsCount} reports",
                rating.Id, rating.ReportsCount);

            try
            {
                var moderationResult = await _moderationService.EvaluateCommentAsync(
                    rating.Comment,
                    _config.DefaultLanguage);

                if (moderationResult.Success)
                {
                    rating.SetAiModerationResult(
                        moderationResult.BadnessScore,
                        moderationResult.Category,
                        moderationResult.Explanation);                    // Auto-hide if score exceeds threshold
                    if (rating.ShouldBeAutoHidden(_config.AutoHideThreshold))
                    {
                        rating.Status = RatingStatus.Hidden;
                        _logger.LogInformation("Auto-hiding rating {RatingId} due to AI score {Score}",
                            rating.Id, moderationResult.BadnessScore);
                    }

                    // Create audit log
                    var auditLog = new ModerationAuditLog(
                        rating.Id,
                        rating.Status == RatingStatus.Hidden ? "AUTO_HIDDEN" : "AI_EVALUATED",
                        $"AI Score: {moderationResult.BadnessScore}, Category: {moderationResult.Category}, Explanation: {moderationResult.Explanation}",
                        null,
                        moderationResult.BadnessScore);

                    _dbContext.ModerationAuditLogs.Add(auditLog);

                    // Save changes first to ensure we have the updated rating status
                    await _dbContext.SaveChangesAsync(cancellationToken);

                    // Send notification for auto-hidden ratings
                    if (rating.Status == RatingStatus.Hidden)
                    {
                        await _notificationService.NotifyAdminOfAutoHiddenRatingAsync(
                            rating.Id,
                            rating.Comment!,
                            moderationResult.BadnessScore);

                        await _notificationService.NotifyUserOfHiddenRatingAsync(
                            rating.Customer.Email!,
                            rating.Id,
                            request.Reason);
                    }
                }
                else
                {
                    _logger.LogWarning("AI moderation failed for rating {RatingId}: {Error}",
                        rating.Id, moderationResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during AI moderation for rating {RatingId}", rating.Id);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        var ratingDto = new ProductRatingDto
        {
            Id = rating.Id,
            Comment = rating.Comment!,
            RatingValue = rating.RatingValue,
            UserName = rating.Customer!.UserName ?? "Anonymous User",
            FullName = rating.Customer.FullName ?? "Anonymous User",
            UserAvatarUrl = rating.Customer.ImageUrl,
            ProductVariantName = MappingHelpers.GetProductVariantOptionValuesString(rating.ProductVariant),
            ReportsCount = rating.ReportsCount,
            LikesCount = rating.Likes.Count,
            ImageUrls = rating.Images?.Select(i => i.ImageUrl).ToList(),
            Status = rating.Status,
            Response = rating.Response,
            CreationTime = rating.CreationTime
        };

        return Result.Success(ratingDto);
    }
}
