using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Admin.ApproveRating;

public record ApproveRatingCommand(int RatingId, string? AdminNote = null) : IRequest<Result>;

public class ApproveRatingCommandHandler : IRequestHandler<ApproveRatingCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<ApproveRatingCommandHandler> _logger;
    private readonly ICurrentUser _currentUser;

    public ApproveRatingCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<ApproveRatingCommandHandler> logger,
        ICurrentUser currentUser)
    {
        _dbContext = dbContext;
        _logger = logger;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(ApproveRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = await _dbContext.Ratings
            .FirstOrDefaultAsync(r => r.Id == request.RatingId, cancellationToken);        if (rating == null)
        {
            return Result.Failure(ProductErrors.RatingNotFound);
        }

        if (rating.Status == RatingStatus.Posted)
        {
            return Result.Failure(Error.Conflict("Rating.AlreadyApproved", "Rating is already approved"));
        }

        // Update rating status
        rating.Status = RatingStatus.Posted;

        // Create audit log
        var auditLog = new ModerationAuditLog(
            rating.Id,
            "MANUAL_APPROVED",
            $"Rating manually approved by admin. Note: {request.AdminNote ?? "No note provided"}",
            _currentUser.Id,
            rating.AiModerationScore);

        _dbContext.ModerationAuditLogs.Add(auditLog);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Rating {RatingId} manually approved by admin {AdminId}", 
            request.RatingId, _currentUser.Id);

        return Result.Success();
    }
}
