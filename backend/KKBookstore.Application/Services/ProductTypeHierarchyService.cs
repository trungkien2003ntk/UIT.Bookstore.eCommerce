using KKBookstore.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Services;

public class ProductTypeHierarchyService : IProductTypeHierarchyService
{
    private readonly IApplicationDbContext _dbContext;

    public ProductTypeHierarchyService(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string?> GetDescendantProductTypeIdsAsStringAsync(int productTypeId, CancellationToken cancellationToken)
    {
        var descendantIds = await GetDescendantProductTypeIdsAsync(productTypeId, cancellationToken);
        
        if (descendantIds.Count == 0)
            return null;

        return string.Join(",", descendantIds);
    }

    public async Task<List<int>> GetDescendantProductTypeIdsAsync(int productTypeId, CancellationToken cancellationToken)
    {
        var productType = await _dbContext.ProductTypes
            .Where(pt => pt.Id == productTypeId)
            .FirstOrDefaultAsync(cancellationToken);

        if (productType is null)
        {
            return [];
        }

        var descendantIds = new List<int> { productTypeId };

        // Get all children of this product type
        await CollectDescendantIds(productTypeId, descendantIds, cancellationToken);

        return descendantIds;
    }

    private async Task CollectDescendantIds(int parentId, List<int> descendantIds, CancellationToken cancellationToken)
    {
        var children = await _dbContext.ProductTypes
            .Where(pt => pt.ParentProductTypeId == parentId)
            .Select(pt => pt.Id)
            .ToListAsync(cancellationToken);

        foreach (var childId in children)
        {
            descendantIds.Add(childId);
            // Recursively collect descendants of this child
            await CollectDescendantIds(childId, descendantIds, cancellationToken);
        }
    }

    public async Task<string?> GetDescendantProductTypeIdsAsStringAsync(IEnumerable<int> productTypeIds, CancellationToken cancellationToken)
    {
        var descendantIds = await GetDescendantProductTypeIdsAsync(productTypeIds, cancellationToken);
        
        if (descendantIds.Count == 0)
            return null;

        return string.Join(",", descendantIds);
    }

    public async Task<List<int>> GetDescendantProductTypeIdsAsync(IEnumerable<int> productTypeIds, CancellationToken cancellationToken)
    {
        var allDescendantIds = new HashSet<int>();

        foreach (var productTypeId in productTypeIds)
        {
            var descendantIds = await GetDescendantProductTypeIdsAsync(productTypeId, cancellationToken);
            foreach (var id in descendantIds)
            {
                allDescendantIds.Add(id);
            }
        }

        return allDescendantIds.OrderBy(id => id).ToList();
    }

    public async Task<List<ProductTypeDetail>> GetProductTypeDetailsFromStringAsync(string productTypeIdsString, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(productTypeIdsString))
            return [];

        var ids = productTypeIdsString.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();

        return await GetProductTypeDetailsAsync(ids, cancellationToken);
    }

    public async Task<List<ProductTypeDetail>> GetProductTypeDetailsAsync(IEnumerable<int> productTypeIds, CancellationToken cancellationToken)
    {
        var productTypes = await _dbContext.ProductTypes
            .Where(pt => productTypeIds.Contains(pt.Id))
            .Select(pt => new ProductTypeDetail
            {
                Id = pt.Id,
                DisplayName = pt.DisplayName
            })
            .OrderBy(pt => pt.Id)
            .ToListAsync(cancellationToken);

        return productTypes;
    }
}
