using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Admin.GetModerationQueue;

public record GetModerationQueueQuery : IRequest<Result<List<ModerationQueueItemDto>>>
{
    public RatingStatus? Status { get; init; } = RatingStatus.PendingReview;
    public int? MinAiScore { get; init; }
    public int? MaxAiScore { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

public class ModerationQueueItemDto
{
    public int RatingId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int RatingValue { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int ReportsCount { get; set; }
    public RatingStatus Status { get; set; }
    
    // AI Moderation Info
    public int? AiModerationScore { get; set; }
    public string? AiModerationCategory { get; set; }
    public string? AiModerationExplanation { get; set; }
    public DateTimeOffset? AiModerationDate { get; set; }
    public bool IsAiModerated { get; set; }
    
    public DateTimeOffset CreationTime { get; set; }
}

public class GetModerationQueueQueryHandler : IRequestHandler<GetModerationQueueQuery, Result<List<ModerationQueueItemDto>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetModerationQueueQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<ModerationQueueItemDto>>> Handle(GetModerationQueueQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Ratings
            .Include(r => r.Customer)
            .Include(r => r.ProductVariant)
                .ThenInclude(pv => pv.Product)
            .AsQueryable();

        // Apply filters
        if (request.Status.HasValue)
        {
            query = query.Where(r => r.Status == request.Status.Value);
        }

        if (request.MinAiScore.HasValue)
        {
            query = query.Where(r => r.AiModerationScore >= request.MinAiScore.Value);
        }

        if (request.MaxAiScore.HasValue)
        {
            query = query.Where(r => r.AiModerationScore <= request.MaxAiScore.Value);
        }

        // Order by priority: highest AI scores first, then by creation time
        query = query.OrderByDescending(r => r.AiModerationScore)
                    .ThenByDescending(r => r.CreationTime);

        // Pagination
        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(r => new ModerationQueueItemDto
            {
                RatingId = r.Id,
                Comment = r.Comment ?? "",
                RatingValue = r.RatingValue,
                UserName = r.Customer.UserName ?? "Anonymous",
                ProductName = r.ProductVariant.Product.Name,
                ReportsCount = r.ReportsCount,
                Status = r.Status,
                AiModerationScore = r.AiModerationScore,
                AiModerationCategory = r.AiModerationCategory,
                AiModerationExplanation = r.AiModerationExplanation,
                AiModerationDate = r.AiModerationDate,
                IsAiModerated = r.IsAiModerated,
                CreationTime = r.CreationTime ?? DateTimeOffset.UtcNow
            })
            .ToListAsync(cancellationToken);

        return Result.Success(items);
    }
}
