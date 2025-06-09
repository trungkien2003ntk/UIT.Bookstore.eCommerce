using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models;
using KKBookstore.Features.Products.GetProductList;
using KKBookstore.Features.Products.Models;
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
            // Get AI-recommended product IDs
            var aiProductIds = await relatedProductsService.GetRelatedProductIdsByIdAsync(request.ProductId, cancellationToken);
            
            // Fetch products using the existing product list query mechanism
            var query = new GetProductListQuery
            {
                ProductIds = aiProductIds.Take(10).ToList(),
                PageNumber = 1,
                PageSize = 10
            };
            
            var aiResult = await sender.Send(query, cancellationToken);
            if (!aiResult.IsSuccess)
            {
                return Result.Failure<List<ProductSummary>>(aiResult.Error);
            }
            
            var aiProducts = aiResult.Value.Items.ToList();
            
            // If we need more products to reach 30, fetch additional random/new products
            var remainingCount = 30 - aiProducts.Count;
            var allProducts = new List<ProductSummary>(aiProducts);
            
            if (remainingCount > 0)
            {
                // Get additional products excluding already selected ones
                var excludeIds = aiProductIds.Concat(new[] { request.ProductId }).ToList();
                var additionalQuery = new GetProductListQuery
                {
                    ExcludeProductIds = excludeIds,
                    PageNumber = 1,
                    PageSize = remainingCount,
                    SortBy = "CreationTime", // Get newer products first
                    SortDirection = "desc"
                };
                
                var additionalResult = await sender.Send(additionalQuery, cancellationToken);
                if (additionalResult.IsSuccess)
                {
                    var additionalProducts = additionalResult.Value.Items.ToList();
                    
                    // Shuffle the additional products for randomness
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
