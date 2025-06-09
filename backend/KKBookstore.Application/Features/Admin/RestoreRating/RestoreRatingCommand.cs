using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Admin.Services;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Admin.RestoreRating;

public record RestoreRatingCommand(int RatingId, string? AdminNote = null) : IRequest<Result>;

public class RestoreRatingCommandHandler : IRequestHandler<RestoreRatingCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IModerationNotificationService _notificationService;
    private readonly ILogger<RestoreRatingCommandHandler> _logger;
    private readonly ICurrentUser _currentUser;

    public RestoreRatingCommandHandler(
        IApplicationDbContext dbContext,
        IModerationNotificationService notificationService,
        ILogger<RestoreRatingCommandHandler> logger,
        ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _notificationService = notificationService;
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(RestoreRatingCommand request, CancellationToken cancellationToken)
    {        var rating = await _dbContext.Ratings
            .Include(r => r.Customer)
            .FirstOrDefaultAsync(r => r.Id == request.RatingId, cancellationToken);

        if (rating == null)
        {
            return Result.Failure(ProductErrors.RatingNotFound);
        }

        if (rating.Status != RatingStatus.Hidden)
        {
            return Result.Failure(Error.Conflict("Rating.NotHidden", "Rating is not hidden and cannot be restored"));
        }

        // Restore rating to Posted status
        rating.Status = RatingStatus.Posted;

        // Create audit log
        var auditLog = new ModerationAuditLog(
            rating.Id,
            "MANUAL_RESTORED",
            $"Rating manually restored by admin. Note: {request.AdminNote ?? "No note provided"}",
            _currentUser.Id,
            rating.AiModerationScore);

        _dbContext.ModerationAuditLogs.Add(auditLog);        await _dbContext.SaveChangesAsync(cancellationToken);

        // Send notification to user
        if (!string.IsNullOrEmpty(rating.Customer?.Email))
        {
            await _notificationService.NotifyUserOfRestoredRatingAsync(
                rating.Customer.Email, 
                rating.Id);
        }

        _logger.LogInformation("Rating {RatingId} manually restored by admin {AdminId}", 
            request.RatingId, _currentUser.Id);

        return Result.Success();
    }
}
