using KKBookstore.Common.Interfaces;

namespace KKBookstore.AI;

public abstract class RelatedProductsServiceBaseDecorator(
    IRelatedProductsService wrappee
) : IRelatedProductsService
{
    private readonly IRelatedProductsService _wrappee = wrappee;

    public virtual async Task<List<int>> GetRelatedProductIdsByIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await _wrappee.GetRelatedProductIdsByIdAsync(productId, cancellationToken);
    }

    public virtual async Task<List<int>> GetRelatedProductIdsByImageAsync(string base64Image, CancellationToken cancellationToken = default)
    {
        return await _wrappee.GetRelatedProductIdsByImageAsync(base64Image, cancellationToken);
    }
}
