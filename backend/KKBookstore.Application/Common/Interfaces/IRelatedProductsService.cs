namespace KKBookstore.Common.Interfaces;

/// <summary>
/// Service for fetching AI-recommended related products based on product ID or image input
/// </summary>
public interface IRelatedProductsService
{
    /// <summary>
    /// Get related product IDs based on a product ID
    /// </summary>
    /// <param name="productId">The product ID to find related products for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of related product IDs</returns>
    Task<List<int>> GetRelatedProductIdsByIdAsync(int productId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get related product IDs based on a base64-encoded image
    /// </summary>
    /// <param name="base64Image">Base64-encoded image</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of related product IDs</returns>
    Task<List<int>> GetRelatedProductIdsByImageAsync(string base64Image, CancellationToken cancellationToken = default);
}
