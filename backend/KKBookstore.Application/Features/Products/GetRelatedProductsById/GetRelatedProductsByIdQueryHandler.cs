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
            var topAiProductIds = aiProductIds.Take(40).ToList(); // limit to 20

            // Step 2: Query for those products
            var query = new GetProductListQuery
            {
                ProductIds = topAiProductIds,
                PageNumber = 1,
                PageSize = 40
            };

            var aiResult = await sender.Send(query, cancellationToken);
            if (!aiResult.IsSuccess)
                return Result.Failure<List<ProductSummary>>(aiResult.Error);

            // Step 3: Map products by ID for ordering and return only AI-recommended products
            var aiProductsDict = aiResult.Value.Items.ToDictionary(p => p.Id);
            var orderedAiProducts = topAiProductIds
                .Where(id => aiProductsDict.ContainsKey(id))
                .Select(id => aiProductsDict[id])
                .ToList();

            return Result.Success(orderedAiProducts);
        }
        catch (Exception ex)
        {
            return Result.Failure<List<ProductSummary>>(Error.Failure("Products.RelatedProductsError", $"Error getting related products: {ex.Message}"));
        }
    }
}
