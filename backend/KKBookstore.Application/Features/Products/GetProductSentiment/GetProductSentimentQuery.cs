using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Application.Features.Products.GetProductSentiment;

public record GetProductSentimentQuery(int ProductId) : IRequest<Result<ProductSentimentDetailDto>>;

public class ProductSentimentDetailDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal? AverageSentimentScore { get; set; }
    public int TotalRatings { get; set; }
    public int PositiveRatings { get; set; }
    public int NegativeRatings { get; set; }
    public int NeutralRatings { get; set; }
    public decimal? AverageRating { get; set; }
    public string DominantSentiment { get; set; } = string.Empty;
    public decimal SentimentDistribution { get; set; }
    public List<VariantSentimentDto> VariantSentiments { get; set; } = new();
}

public class VariantSentimentDto
{
    public int ProductVariantId { get; set; }
    public string? VariantSku { get; set; }
    public decimal? AverageSentimentScore { get; set; }
    public int TotalRatings { get; set; }
    public int PositiveRatings { get; set; }
    public int NegativeRatings { get; set; }
    public int NeutralRatings { get; set; }
    public decimal? AverageRating { get; set; }
    public string DominantSentiment { get; set; } = string.Empty;
    public List<OptionValueDto> OptionValues { get; set; } = new();
}

public class OptionValueDto
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}

public class GetProductSentimentQueryHandler : IRequestHandler<GetProductSentimentQuery, Result<ProductSentimentDetailDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetProductSentimentQueryHandler> _logger;

    public GetProductSentimentQueryHandler(
        IApplicationDbContext dbContext,
        ILogger<GetProductSentimentQueryHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<ProductSentimentDetailDto>> Handle(GetProductSentimentQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == request.ProductId && !p.IsDeleted)
                .Include(p => p.Ratings.Where(r => !r.IsDeleted && r.SentimentScore.HasValue))
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.Ratings.Where(r => !r.IsDeleted && r.SentimentScore.HasValue))
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantOptionValues)
                        .ThenInclude(pov => pov.Option)
                .Include(p => p.ProductVariants)
                    .ThenInclude(pv => pv.ProductVariantOptionValues)
                        .ThenInclude(pov => pov.OptionValue)
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                return Result.Failure<ProductSentimentDetailDto>(Error.NotFound("Product.NotFound", "Product not found"));
            }

            // Calculate product-level sentiment
            var productRatings = product.Ratings.Where(r => r.SentimentScore.HasValue).ToList();
            var allRatings = productRatings.Concat(
                product.ProductVariants.SelectMany(pv => pv.Ratings.Where(r => r.SentimentScore.HasValue))
            ).ToList();

            if (!allRatings.Any())
            {
                return Result.Success(new ProductSentimentDetailDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    TotalRatings = 0,
                    VariantSentiments = new List<VariantSentimentDto>()
                });
            }

            var positiveCount = allRatings.Count(r => r.SentimentLabel == "Positive");
            var negativeCount = allRatings.Count(r => r.SentimentLabel == "Negative");
            var neutralCount = allRatings.Count(r => r.SentimentLabel == "Neutral");

            var dominantSentiment = GetDominantSentiment(positiveCount, negativeCount, neutralCount);
            var sentimentDistribution = CalculateSentimentDistribution(positiveCount, negativeCount, neutralCount);

            var result = new ProductSentimentDetailDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                AverageSentimentScore = allRatings.Average(r => r.SentimentScore!.Value),
                TotalRatings = allRatings.Count,
                PositiveRatings = positiveCount,
                NegativeRatings = negativeCount,
                NeutralRatings = neutralCount,
                AverageRating = (decimal)allRatings.Average(r => r.RatingValue),
                DominantSentiment = dominantSentiment,
                SentimentDistribution = sentimentDistribution,
                VariantSentiments = product.ProductVariants.Select(pv =>
                {
                    var variantRatings = pv.Ratings.Where(r => r.SentimentScore.HasValue).ToList();
                    if (!variantRatings.Any())
                    {
                        return new VariantSentimentDto
                        {
                            ProductVariantId = pv.Id,
                            VariantSku = pv.SkuValue?.Value,
                            TotalRatings = 0,
                            OptionValues = pv.ProductVariantOptionValues?.Select(pov => new OptionValueDto
                            {
                                Name = pov.Option?.Name ?? string.Empty,
                                Value = pov.OptionValue?.Value ?? string.Empty
                            }).ToList() ?? new List<OptionValueDto>()
                        };
                    }

                    var vPositiveCount = variantRatings.Count(r => r.SentimentLabel == "Positive");
                    var vNegativeCount = variantRatings.Count(r => r.SentimentLabel == "Negative");
                    var vNeutralCount = variantRatings.Count(r => r.SentimentLabel == "Neutral");

                    return new VariantSentimentDto
                    {
                        ProductVariantId = pv.Id,
                        VariantSku = pv.SkuValue?.Value,
                        AverageSentimentScore = variantRatings.Average(r => r.SentimentScore!.Value),
                        TotalRatings = variantRatings.Count,
                        PositiveRatings = vPositiveCount,
                        NegativeRatings = vNegativeCount,
                        NeutralRatings = vNeutralCount,
                        AverageRating = (decimal)variantRatings.Average(r => r.RatingValue),
                        DominantSentiment = GetDominantSentiment(vPositiveCount, vNegativeCount, vNeutralCount),
                        OptionValues = pv.ProductVariantOptionValues?.Select(pov => new OptionValueDto
                        {
                            Name = pov.Option?.Name ?? string.Empty,
                            Value = pov.OptionValue?.Value ?? string.Empty
                        }).ToList() ?? new List<OptionValueDto>()
                    };
                }).ToList()
            };

            _logger.LogInformation("Retrieved sentiment data for product {ProductId} with {TotalRatings} ratings", 
                product.Id, result.TotalRatings);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving sentiment data for product {ProductId}", request.ProductId);
            return Result.Failure<ProductSentimentDetailDto>(Error.Failure("GetProductSentiment.Failed", "Failed to retrieve product sentiment"));
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

    private static decimal CalculateSentimentDistribution(int positive, int negative, int neutral)
    {
        var total = positive + negative + neutral;
        if (total == 0) return 0;

        var maxCount = Math.Max(positive, Math.Max(negative, neutral));
        return (decimal)maxCount / total * 100;
    }
}