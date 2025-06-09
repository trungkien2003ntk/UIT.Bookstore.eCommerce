using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.GetProductList;
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Products.GetRelatedProductsById;

public class GetRelatedProductsByIdQueryHandler(
    IRelatedProductsService relatedProductsService,
    ISender sender
) : IRequestHandler<GetRelatedProductsByIdQuery, Result<List<ProductSummary>>>
{
    public async Task<Result<List<ProductSummary>>> Handle(GetRelatedProductsByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Step 1: Get AI-recommended product IDs
            var aiProductIds = await relatedProductsService.GetRelatedProductIdsByIdAsync(request.ProductId, cancellationToken);
            var topAiProductIds = aiProductIds.Take(20).ToList(); // limit to 20

            // Step 2: Query for those products
            var query = new GetProductListQuery
            {
                ProductIds = topAiProductIds,
                PageNumber = 1,
                PageSize = 20
            };

            var aiResult = await sender.Send(query, cancellationToken);
            if (!aiResult.IsSuccess)
                return Result.Failure<List<ProductSummary>>(aiResult.Error);

            // Step 3: Map products by ID for ordering
            var aiProductsDict = aiResult.Value.Items.ToDictionary(p => p.Id);
            var orderedAiProducts = topAiProductIds
                .Where(id => aiProductsDict.ContainsKey(id))
                .Select(id => aiProductsDict[id])
                .ToList();

            // Step 4: Check if we need more products
            var remainingCount = 30 - orderedAiProducts.Count;
            var allProducts = new List<ProductSummary>(orderedAiProducts);

            if (remainingCount > 0)
            {
                var excludeIds = topAiProductIds.Concat(new[] { request.ProductId }).ToList();

                var additionalQuery = new GetProductListQuery
                {
                    ExcludeProductIds = excludeIds,
                    PageNumber = 1,
                    PageSize = remainingCount,
                    SortBy = "CreationTime",
                    SortDirection = "desc"
                };

                var additionalResult = await sender.Send(additionalQuery, cancellationToken);
                if (additionalResult.IsSuccess)
                {
                    var additionalProducts = additionalResult.Value.Items.ToList();

                    // Shuffle for randomness
                    var random = new Random();
                    for (int i = additionalProducts.Count - 1; i > 0; i--)
                    {
                        int j = random.Next(0, i + 1);
                        (additionalProducts[i], additionalProducts[j]) = (additionalProducts[j], additionalProducts[i]);
                    }

                    allProducts.AddRange(additionalProducts);
                }
            }

            return Result.Success(allProducts.Take(30).ToList());
        }
        catch (Exception ex)
        {
            return Result.Failure<List<ProductSummary>>(Error.Failure("Products.RelatedProductsError", $"Error getting related products: {ex.Message}"));
        }
    }
}
