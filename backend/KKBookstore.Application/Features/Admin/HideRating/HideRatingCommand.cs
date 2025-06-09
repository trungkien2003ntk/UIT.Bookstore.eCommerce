using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Admin.Services;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Admin.HideRating;

public record HideRatingCommand(int RatingId, string Reason) : IRequest<Result>;

public class HideRatingCommandHandler : IRequestHandler<HideRatingCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IModerationNotificationService _notificationService;
    private readonly ILogger<HideRatingCommandHandler> _logger;
    private readonly ICurrentUser _currentUser;

    public HideRatingCommandHandler(
        IApplicationDbContext dbContext,
        IModerationNotificationService notificationService,
        ILogger<HideRatingCommandHandler> logger,
        ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _notificationService = notificationService;
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(HideRatingCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Reason))
        {
            return Result.Failure(Error.Validation("HideRating.ReasonRequired", "Reason is required when hiding a rating"));
        }        var rating = await _dbContext.Ratings
            .Include(r => r.Customer)
            .FirstOrDefaultAsync(r => r.Id == request.RatingId, cancellationToken);if (rating == null)
        {
            return Result.Failure(ProductErrors.RatingNotFound);
        }

        if (rating.Status == RatingStatus.Hidden)
        {
            return Result.Failure(Error.Validation("HideRating.AlreadyHidden", "Rating is already hidden"));
        }

        // Update rating status
        rating.Status = RatingStatus.Hidden;

        // Create audit log
        var auditLog = new ModerationAuditLog(
            rating.Id,
            "MANUAL_HIDDEN",
            $"Rating manually hidden by admin. Reason: {request.Reason}",
            _currentUser.Id,
            rating.AiModerationScore);

        _dbContext.ModerationAuditLogs.Add(auditLog);        await _dbContext.SaveChangesAsync(cancellationToken);

        // Send notification to user
        if (!string.IsNullOrEmpty(rating.Customer?.Email))
        {
            await _notificationService.NotifyUserOfHiddenRatingAsync(
                rating.Customer.Email, 
                rating.Id, 
                request.Reason);
        }

        _logger.LogInformation("Rating {RatingId} manually hidden by admin {AdminId}. Reason: {Reason}", 
            request.RatingId, _currentUser.Id, request.Reason);

        return Result.Success();
    }
}
