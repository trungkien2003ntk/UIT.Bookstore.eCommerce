namespace KKBookstore.Common.Interfaces;

public interface IProductTypeHierarchyService
{
    /// <summary>
    /// Gets all descendant product type IDs (children and their children) for a given product type
    /// </summary>
    /// <param name="productTypeId">The parent product type ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Comma-separated string of all descendant product type IDs, or null if no descendants</returns>
    Task<string?> GetDescendantProductTypeIdsAsStringAsync(int productTypeId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all descendant product type IDs (children and their children) for a given product type
    /// </summary>
    /// <param name="productTypeId">The parent product type ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all descendant product type IDs</returns>
    Task<List<int>> GetDescendantProductTypeIdsAsync(int productTypeId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all descendant product type IDs (children and their children) for multiple product types
    /// </summary>
    /// <param name="productTypeIds">The parent product type IDs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Comma-separated string of all distinct descendant product type IDs, or null if no descendants</returns>
    Task<string?> GetDescendantProductTypeIdsAsStringAsync(IEnumerable<int> productTypeIds, CancellationToken cancellationToken);

    /// <summary>
    /// Gets all descendant product type IDs (children and their children) for multiple product types
    /// </summary>
    /// <param name="productTypeIds">The parent product type IDs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of all distinct descendant product type IDs</returns>
    Task<List<int>> GetDescendantProductTypeIdsAsync(IEnumerable<int> productTypeIds, CancellationToken cancellationToken);

    /// <summary>
    /// Gets product type details (ID and DisplayName) from a comma-separated string of IDs
    /// </summary>
    /// <param name="productTypeIdsString">Comma-separated product type IDs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of product type details</returns>
    Task<List<ProductTypeDetail>> GetProductTypeDetailsFromStringAsync(string productTypeIdsString, CancellationToken cancellationToken);
    
    /// <summary>
    /// Gets product type details (ID and DisplayName) for a list of IDs
    /// </summary>
    /// <param name="productTypeIds">List of product type IDs</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List of product type details</returns>
    Task<List<ProductTypeDetail>> GetProductTypeDetailsAsync(IEnumerable<int> productTypeIds, CancellationToken cancellationToken);
}

public class ProductTypeDetail
{
    public int Id { get; init; }
    public string DisplayName { get; init; } = string.Empty;
}
