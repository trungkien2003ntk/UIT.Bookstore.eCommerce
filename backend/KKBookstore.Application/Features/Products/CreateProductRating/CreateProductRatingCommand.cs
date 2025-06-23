using KKBookstore.Common.Configuration;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Admin.Services;
using KKBookstore.Features.Products.Models;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.Products;
using KKBookstore.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KKBookstore.Features.Products.CreateProductRating;

public record CreateProductRatingCommand(
    int ProductVariantId,
    int CustomerId,
    string Comment,
    int RatingValue,
    List<string> ImageUrls
) : IRequest<Result<ProductRatingDto>>
{
}

public class CreateProductRatingCommandHandler : IRequestHandler<CreateProductRatingCommand, Result<ProductRatingDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICommentModerationService _moderationService;
    private readonly IModerationNotificationService _notificationService;
    private readonly ILogger<CreateProductRatingCommandHandler> _logger;
    private readonly ModerationConfiguration _config;

    public CreateProductRatingCommandHandler(
        IApplicationDbContext dbContext,
        ICommentModerationService moderationService,
        IModerationNotificationService notificationService,
        ILogger<CreateProductRatingCommandHandler> logger,
        IOptions<ModerationConfiguration> config)
    {
        _dbContext = dbContext;
        _moderationService = moderationService;
        _notificationService = notificationService;
        _logger = logger;
        _config = config.Value;
    }

    public async Task<Result<ProductRatingDto>> Handle(CreateProductRatingCommand request, CancellationToken cancellationToken)
    {
        var productVariant = await _dbContext.ProductVariants
            .Include(v => v.ProductVariantOptionValues)!
                .ThenInclude(x => x.OptionValue)
            .FirstOrDefaultAsync(v => v.Id == request.ProductVariantId, cancellationToken);

        var currentUser = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.CustomerId, cancellationToken);

        if (currentUser == null)
        {
            return Result.Failure<ProductRatingDto>(UserErrors.NotFound);
        }

        if (productVariant == null)
        {
            return Result.Failure<ProductRatingDto>(ProductErrors.NotFound);
        }

        var createRatingResult = Rating.Create(
            request.Comment,
            request.RatingValue,
            request.CustomerId,
            productVariant,
            request.ImageUrls
        );

        if (createRatingResult.IsFailure)
        {
            return Result.Failure<ProductRatingDto>(createRatingResult.Error);
        }
        var rating = createRatingResult.Value;

        await _dbContext.Ratings.AddAsync(rating, cancellationToken);
        await _dbContext.SaveChangesAsync();

        // Immediately evaluate comment with AI moderation if enabled and comment exists
        if (_config.IsEnabled && !string.IsNullOrWhiteSpace(rating.Comment))
        {
            _logger.LogInformation("Evaluating new rating {RatingId} with AI moderation", rating.Id);

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
                        moderationResult.Explanation);

                    // Auto-hide if score exceeds threshold
                    if (rating.ShouldBeAutoHidden(_config.AutoHideThreshold))
                    {
                        rating.Status = RatingStatus.Hidden;
                        _logger.LogInformation("Auto-hiding new rating {RatingId} due to AI score {Score}",
                            rating.Id, moderationResult.BadnessScore);
                    }

                    // Create audit log
                    var auditLog = new ModerationAuditLog(
                        rating.Id,
                        rating.Status == RatingStatus.Hidden ? "AUTO_HIDDEN_ON_CREATE" : "AI_EVALUATED_ON_CREATE",
                        $"AI Score: {moderationResult.BadnessScore}, Category: {moderationResult.Category}, Explanation: {moderationResult.Explanation}",
                        null,
                        moderationResult.BadnessScore);

                    _dbContext.ModerationAuditLogs.Add(auditLog);
                }
                else
                {
                    _logger.LogWarning("AI moderation failed for new rating {RatingId}: {Error}",
                        rating.Id, moderationResult.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during AI moderation for new rating {RatingId}", rating.Id);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        // Send notification for auto-hidden ratings after saving
        if (_config.IsEnabled &&
            rating.Status == RatingStatus.Hidden &&
            !string.IsNullOrWhiteSpace(rating.Comment))
        {
            try
            {
                await _notificationService.NotifyAdminOfAutoHiddenRatingAsync(
                    rating.Id,
                    rating.Comment,
                    rating.AiModerationScore ?? 0);

                await _notificationService.NotifyUserOfHiddenRatingAsync(
                    currentUser.Email!,
                    rating.Id,
                    "Inappropriate content detected by AI moderation");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending notifications for auto-hidden rating {RatingId}", rating.Id);
            }
        }
        var ratingDto = new ProductRatingDto
        {
            Id = rating.Id,
            Comment = rating.Comment!,
            RatingValue = rating.RatingValue,
            CustomerId = rating.CustomerId,
            UserName = currentUser.UserName ?? "Anonymous User",
            FullName = currentUser.FullName ?? "Anonymous User",
            UserAvatarUrl = null,
            ProductVariantName = MappingHelpers.GetProductVariantOptionValuesString(productVariant),
            LikesCount = 0,
            ReportsCount = 0,
            ImageUrls = rating.Images?.Select(i => i.ImageUrl).ToList(),
            Response = null,
            Status = rating.Status,
            CreationTime = rating.CreationTime
        };
        return Result.Success(ratingDto);
    }
}