using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Products.GetProductsBySentiment;

public class GetProductsBySentimentQuery : IPaginatedQuery, IRequest<Result<PagedResult<ProductSentimentDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 12;
    public decimal? MinSentimentScore { get; set; }
    public int? ProductTypeId { get; set; }
    public string? SentimentLabel { get; set; } // Positive, Negative, Neutral
}

public class ProductSentimentDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductImageUrl { get; set; }
    public string? ProductTypeName { get; set; }
    public decimal? AverageSentimentScore { get; set; }
    public int TotalRatings { get; set; }
    public int PositiveRatings { get; set; }
    public int NegativeRatings { get; set; }
    public int NeutralRatings { get; set; }
    public decimal? AverageRating { get; set; } // Traditional star rating
    public string DominantSentiment { get; set; } = string.Empty;
}

public class GetProductsBySentimentQueryHandler : IRequestHandler<GetProductsBySentimentQuery, Result<PagedResult<ProductSentimentDto>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetProductsBySentimentQueryHandler> _logger;

    public GetProductsBySentimentQueryHandler(
        IApplicationDbContext dbContext,
        ILogger<GetProductsBySentimentQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<PagedResult<ProductSentimentDto>>> Handle(GetProductsBySentimentQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = from product in _dbContext.Products.Where(p => !p.IsDeleted)
                        join rating in _dbContext.Ratings.Where(r => !r.IsDeleted && r.Status != RatingStatus.Hidden && r.SentimentScore.HasValue)
                            on product.Id equals rating.ProductId
                        join productType in _dbContext.ProductTypes.Where(pt => !pt.IsDeleted)
                            on product.ProductTypeId equals productType.Id into productTypeGroup
                        from productType in productTypeGroup.DefaultIfEmpty()
                        group new { product, rating, productType } by new
                        {
                            product.Id,
                            product.Name,
                            ProductTypeName = productType.DisplayName,
                            product.ProductTypeId
                        } into g
                        select new ProductSentimentDto
                        {
                            ProductId = g.Key.Id,
                            ProductName = g.Key.Name,
                            ProductTypeName = g.Key.ProductTypeName,
                            ProductImageUrl = _dbContext.ProductImages
                                .Where(pi => pi.ProductId == g.Key.Id)
                                .OrderBy(pi => pi.Id)
                                .Select(pi => pi.ThumbnailImageUrl)
                                .FirstOrDefault(),
                            AverageSentimentScore = g.Average(x => x.rating.SentimentScore),
                            TotalRatings = g.Count(),
                            PositiveRatings = g.Count(x => x.rating.SentimentLabel == "Positive"),
                            NegativeRatings = g.Count(x => x.rating.SentimentLabel == "Negative"),
                            NeutralRatings = g.Count(x => x.rating.SentimentLabel == "Neutral"),
                            AverageRating = (decimal)g.Average(x => x.rating.RatingValue),
                        };

            // Apply filters
            if (request.MinSentimentScore.HasValue)
            {
                query = query.Where(x => x.AverageSentimentScore >= request.MinSentimentScore.Value);
            }

            if (!string.IsNullOrEmpty(request.SentimentLabel))
            {
                // Filter by dominant sentiment
                if (request.SentimentLabel == "Positive")
                    query = query.Where(x => x.PositiveRatings > x.NegativeRatings && x.PositiveRatings > x.NeutralRatings);
                else if (request.SentimentLabel == "Negative")
                    query = query.Where(x => x.NegativeRatings > x.PositiveRatings && x.NegativeRatings > x.NeutralRatings);
                else if (request.SentimentLabel == "Neutral")
                    query = query.Where(x => x.NeutralRatings > x.PositiveRatings && x.NeutralRatings > x.NegativeRatings);
            }

            if (request.ProductTypeId.HasValue)
            {
                query = from product in _dbContext.Products.Where(p => !p.IsDeleted && p.ProductTypeId == request.ProductTypeId.Value)
                        join rating in _dbContext.Ratings.Where(r => !r.IsDeleted && r.SentimentScore.HasValue)
                            on product.Id equals rating.ProductId
                        join productType in _dbContext.ProductTypes.Where(pt => !pt.IsDeleted)
                            on product.ProductTypeId equals productType.Id into productTypeGroup
                        from productType in productTypeGroup.DefaultIfEmpty()
                        group new { product, rating, productType } by new
                        {
                            product.Id,
                            product.Name,
                            ProductTypeName = productType.DisplayName,
                            product.ProductTypeId
                        } into g
                        select new ProductSentimentDto
                        {
                            ProductId = g.Key.Id,
                            ProductName = g.Key.Name,
                            ProductTypeName = g.Key.ProductTypeName,
                            ProductImageUrl = _dbContext.ProductImages
                                .Where(pi => pi.ProductId == g.Key.Id)
                                .OrderBy(pi => pi.Id)
                                .Select(pi => pi.ThumbnailImageUrl)
                                .FirstOrDefault(),
                            AverageSentimentScore = g.Average(x => x.rating.SentimentScore),
                            TotalRatings = g.Count(),
                            PositiveRatings = g.Count(x => x.rating.SentimentLabel == "Positive"),
                            NegativeRatings = g.Count(x => x.rating.SentimentLabel == "Negative"),
                            NeutralRatings = g.Count(x => x.rating.SentimentLabel == "Neutral"),
                            AverageRating = (decimal)g.Average(x => x.rating.RatingValue),
                        };
            }

            // Order by sentiment score descending
            query = query.OrderByDescending(x => x.AverageSentimentScore);

            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            foreach (var product in products)
            {
                product.DominantSentiment = GetDominantSentiment(product.PositiveRatings, product.NegativeRatings, product.NeutralRatings);
            }

            var result = new PagedResult<ProductSentimentDto>(products, totalCount, request.PageNumber, request.PageSize);

            _logger.LogInformation("Retrieved {Count} products by sentiment with {TotalCount} total", products.Count, totalCount);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving products by sentiment");
            return Result.Failure<PagedResult<ProductSentimentDto>>(Error.Failure("GetProductsBySentiment.Failed", "Failed to retrieve products by sentiment"));
        }
    }

    private static string GetDominantSentiment(int positive, int negative, int neutral)
    {
        if (positive > negative && positive > neutral)
            return "Positive";
        if (negative > positive && negative > neutral)
            return "Negative";
        if (neutral > positive && neutral > negative)
            return "Neutral";
        return "Mixed";
    }
}