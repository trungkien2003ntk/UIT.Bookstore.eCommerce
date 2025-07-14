using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.Models;
using KKBookstore.Models;
using KKBookstore.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Products.GetAdminProductDetail;

public record GetAdminProductDetailQuery(int ProductId) : IRequest<Result<AdminProductDto>>;

public class GetAdminProductDetailQueryHandler : IRequestHandler<GetAdminProductDetailQuery, Result<AdminProductDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAdminProductDetailQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<AdminProductDto>> Handle(GetAdminProductDetailQuery request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.Id == request.ProductId)
            .Include(x => x.ProductType)
            .Include(x => x.UnitMeasure)
            .Include(x => x.AttributeProductValues)
                .ThenInclude(x => x.AttributeValue)
                    .ThenInclude(x => x.ProductTypeAttribute)
            .Include(x => x.ProductVariants)
                .ThenInclude(x => x.ProductVariantOptionValues)
                        .ThenInclude(x => x.Option)
            .Include(x => x.ProductVariants)
                .ThenInclude(x => x.ProductVariantOptionValues)
                        .ThenInclude(x => x.OptionValue)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Inventories)
                    .ThenInclude(i => i.Warehouse)
                        .ThenInclude(w => w!.Address)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Ratings)
                    .ThenInclude(r => r.Customer)
            .Include(p => p.ProductVariants)
                .ThenInclude(pv => pv.Ratings)
                    .ThenInclude(r => r.Likes)
            .Include(p => p.Ratings)
                .ThenInclude(r => r.Customer)
            .Include(p => p.Ratings)
                .ThenInclude(r => r.Likes)
            .Include(x => x.ProductImages)
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            return Result.Failure<AdminProductDto>(ProductErrors.NotFound);
        }

        // Calculate overall product rating directly from Product.Ratings
        decimal? overallRating = null;
        int totalRatingsCount = 0;

        if (product.Ratings != null && product.Ratings.Any())
        {
            overallRating = Convert.ToDecimal(product.Ratings.Average(r => r.RatingValue));
            totalRatingsCount = product.Ratings.Count;
        }

        // Calculate sentiment summary for the product
        var sentimentSummary = CalculateAdminProductSentimentSummary(product);

        var productDto = new AdminProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku?.Value,
            ProductTypeId = product.ProductTypeId,
            Description = product.Description,
            IsBook = product.IsBook,
            IsActive = product.IsActive,
            UnitMeasureId = product.UnitMeasureId,
            AverageRating = overallRating,
            RatingsCount = totalRatingsCount,
            TotalStockQuantity = product.ProductVariants.Sum(pv => pv.StockQuantity),
            SentimentSummary = sentimentSummary,
            ProductType = new ProductTypeDto
            {
                Id = product.ProductType.Id,
                Level = product.ProductType.Level,
                DisplayName = product.ProductType.DisplayName,
                Description = product.ProductType.Description,
                ParentProductTypeId = product.ProductType.ParentProductTypeId,
                ProductTypeCode = product.ProductType.ProductTypeCode
            },
            UnitMeasure = new UnitMeasureDto
            {
                Id = product.UnitMeasure.Id,
                Name = product.UnitMeasure.Name
            },
            AttributeProductValues = product.AttributeProductValues.Select(apv => new ProductTypeAttributeProductValueDto
            {
                AttributeId = apv.AttributeValue.ProductTypeAttributeId,
                AttributeValueId = apv.AttributeValueId,
                Name = apv.AttributeValue.ProductTypeAttribute.Name,
                Value = apv.AttributeValue.Value
            }).ToList(),
            ProductVariants = product.ProductVariants.Select(pv =>
            {
                // Calculate average rating for this variant
                decimal? avgRating = null;
                int ratingsCount = 0;

                if (pv.Ratings != null && pv.Ratings.Any())
                {
                    avgRating = Convert.ToDecimal(pv.Ratings.Average(r => r.RatingValue));
                    ratingsCount = pv.Ratings.Count;
                }

                // Calculate sentiment summary for this variant
                var variantSentimentSummary = CalculateAdminVariantSentimentSummary(pv);

                return new ProductVariantDto
                {
                    Id = pv.Id,
                    Sku = pv.SkuValue?.Value,
                    RecommendedRetailPrice = pv.RecommendedRetailPrice,
                    UnitPrice = pv.UnitPrice,
                    Weight = pv.Weight,
                    Dimension = pv.Dimension,
                    TaxRate = pv.TaxRate,
                    Comment = pv.Comment,
                    StockQuantity = pv.StockQuantity,
                    TotalQuantity = pv.StockQuantity,
                    AverageRating = avgRating,
                    RatingsCount = ratingsCount,
                    SentimentSummary = variantSentimentSummary,
                    Ratings = pv.Ratings?.Select(r => new RatingDto
                    {
                        Id = r.Id,
                        Comment = r.Comment ?? string.Empty,
                        RatingValue = r.RatingValue,
                        CustomerId = r.CustomerId,
                        CustomerName = r.Customer?.FirstName ?? "Anonymous",
                        CreationTime = r.CreationTime ?? DateTimeOffset.Now,
                        ProductVariantId = r.ProductVariantId,
                        LikesCount = r.Likes?.Count ?? 0,
                        Response = r.Response ?? string.Empty,
                        IsReported = r.ReportsCount > 0,
                        VariantOptions = pv.ProductVariantOptionValues?.Select(pov => new ProductVariantOptionDto
                        {
                            ProductOptionId = pov.OptionId,
                            ProductOptionValueId = pov.OptionValueId,
                            Name = pov.Option?.Name ?? string.Empty,
                            Value = pov.OptionValue?.Value ?? string.Empty
                        }).ToList() ?? new List<ProductVariantOptionDto>()
                    }).ToList() ?? new List<RatingDto>(),
                    VariantOptions = pv.ProductVariantOptionValues?.Select(pov => new ProductVariantDto.VariantOptionDto
                    {
                        ProductOptionId = pov.OptionId,
                        ProductOptionValueId = pov.OptionValueId,
                        Value = pov.OptionValue?.Value ?? string.Empty,
                        Name = pov.Option?.Name ?? string.Empty,
                    }).ToList() ?? new List<ProductVariantDto.VariantOptionDto>(),
                    StockBreakdowns = pv.Inventories?
                        .Where(inv => inv.WarehouseId != null && inv.IsActive)
                        .GroupBy(inv => inv.WarehouseId)
                        .Select(g =>
                        {
                            var firstInv = g.First();
                            return new StockBreakdownDto
                            {
                                BranchId = g.Key ?? 0,
                                BranchName = firstInv.Warehouse?.Name ?? "Unknown",
                                Description = firstInv.Warehouse?.Description ?? string.Empty,
                                IsActive = true,
                                Address = firstInv.Warehouse?.Address != null ? new BranchAddressDto
                                {
                                    PhoneNumber = firstInv.Warehouse.Address.PhoneNumber,
                                    ProvinceId = firstInv.Warehouse.Address.ProvinceId,
                                    ProvinceName = firstInv.Warehouse.Address.ProvinceName,
                                    DistrictId = firstInv.Warehouse.Address.DistrictId,
                                    DistrictName = firstInv.Warehouse.Address.DistrictName,
                                    CommuneCode = firstInv.Warehouse.Address.CommuneCode,
                                    CommuneName = firstInv.Warehouse.Address.CommuneName,
                                    DetailAddress = firstInv.Warehouse.Address.DetailAddress,
                                    Type = firstInv.Warehouse.Address.Type
                                } : null,
                                StockQuantity = g.Sum(x => x.StockQuantity)
                            };
                        })
                        .ToList() ?? []
                };
            }).ToList(),
            ProductImages = product.ProductImages.Select(pi => new ProductImageDto
            {
                Id = pi.Id,
                ThumbnailImageUrl = pi.ThumbnailImageUrl,
                LargeImageUrl = pi.LargeImageUrl
            }).ToList()
        };

        return Result.Success(productDto);
    }

    private static AdminProductSentimentSummary? CalculateAdminProductSentimentSummary(Product product)
    {
        var allRatings = product.Ratings?.Where(r => r.SentimentScore.HasValue).ToList() ?? new List<Rating>();
        var variantRatings = product.ProductVariants
            .SelectMany(pv => pv.Ratings?.Where(r => r.SentimentScore.HasValue) ?? new List<Rating>())
            .ToList();

        allRatings.AddRange(variantRatings);

        if (!allRatings.Any())
            return null;

        var positiveCount = allRatings.Count(r => r.SentimentLabel == "Positive");
        var negativeCount = allRatings.Count(r => r.SentimentLabel == "Negative");
        var neutralCount = allRatings.Count(r => r.SentimentLabel == "Neutral");

        var dominantSentiment = GetDominantSentiment(positiveCount, negativeCount, neutralCount);
        var sentimentDistribution = CalculateSentimentDistribution(positiveCount, negativeCount, neutralCount);

        var variantSentiments = product.ProductVariants
            .Select(pv => CalculateAdminVariantSentimentSummary(pv))
            .Where(vs => vs != null)
            .ToList();

        return new AdminProductSentimentSummary
        {
            AverageSentimentScore = allRatings.Average(r => r.SentimentScore!.Value),
            TotalRatings = allRatings.Count,
            PositiveRatings = positiveCount,
            NegativeRatings = negativeCount,
            NeutralRatings = neutralCount,
            DominantSentiment = dominantSentiment,
            SentimentDistribution = sentimentDistribution,
            VariantSentiments = variantSentiments
        };
    }

    private static AdminVariantSentimentDto? CalculateAdminVariantSentimentSummary(ProductVariant variant)
    {
        var ratings = variant.Ratings?.Where(r => r.SentimentScore.HasValue).ToList() ?? new List<Rating>();

        if (!ratings.Any())
            return null;

        var positiveCount = ratings.Count(r => r.SentimentLabel == "Positive");
        var negativeCount = ratings.Count(r => r.SentimentLabel == "Negative");
        var neutralCount = ratings.Count(r => r.SentimentLabel == "Neutral");

        return new AdminVariantSentimentDto
        {
            ProductVariantId = variant.Id,
            VariantSku = variant.SkuValue?.Value,
            AverageSentimentScore = ratings.Average(r => r.SentimentScore!.Value),
            TotalRatings = ratings.Count,
            PositiveRatings = positiveCount,
            NegativeRatings = negativeCount,
            NeutralRatings = neutralCount,
            DominantSentiment = GetDominantSentiment(positiveCount, negativeCount, neutralCount)
        };
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