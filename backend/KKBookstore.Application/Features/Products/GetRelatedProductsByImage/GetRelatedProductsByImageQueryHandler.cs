using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Products.GetProductList;
using KKBookstore.Models;
using MediatR;

namespace KKBookstore.Features.Products.GetRelatedProductsByImage;

public class GetRelatedProductsByImageQueryHandler(
    IRelatedProductsService relatedProductsService,
    ISender sender
) : IRequestHandler<GetRelatedProductsByImageQuery, Result<List<ProductSummary>>>
{
    public async Task<Result<List<ProductSummary>>> Handle(GetRelatedProductsByImageQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Base64Image))
        {
            return Result.Failure<List<ProductSummary>>(Error.Validation("Products.InvalidImage", "Base64 image is required"));
        }

        try
        {
            // Get AI-recommended product IDs
            var aiProductIds = await relatedProductsService.GetRelatedProductIdsByImageAsync(request.Base64Image, cancellationToken);

            if (!aiProductIds.Any())
            {
                return Result.Success(new List<ProductSummary>());
            }

            // Fetch products using the existing product list query mechanism
            var query = new GetProductListQuery
            {
                ProductIds = aiProductIds.ToList(),
                PageNumber = 1,
                PageSize = aiProductIds.Count()
            };

            var result = await sender.Send(query, cancellationToken);
            if (!result.IsSuccess)
            {
                return Result.Failure<List<ProductSummary>>(result.Error);
            }

            return Result.Success(result.Value.Items.ToList());
        }
        catch (Exception ex)
        {
            return Result.Failure<List<ProductSummary>>(Error.Failure("Products.RelatedProductsByImageError", $"Error getting related products by image: {ex.Message}"));
        }
    }
}