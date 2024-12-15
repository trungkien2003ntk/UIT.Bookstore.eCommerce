using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.ProductTypes;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Services;

public class ProductTypeAttributeService
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductTypeAttributeService(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<HashSet<ProductTypeAttributeDto>>> GetProductTypeAttributesIncludingParents(
        int productTypeId,
        CancellationToken cancellationToken)
    {
        var productType = await _dbContext.ProductTypes
            .Where(pt => pt.Id == productTypeId)
            .Include(pt => pt.ParentProductType)
            .FirstOrDefaultAsync(cancellationToken);

        if (productType is null)
        {
            return Result.Failure<HashSet<ProductTypeAttributeDto>>(ProductTypeErrors.NotFound);
        }

        var productTypeAttributesResult = new HashSet<ProductTypeAttributeDto>();

        // Get attributes for the current product type
        var currentProductTypeAttributes = await GetAttributesForSingleProductType(productTypeId, false, cancellationToken);
        productTypeAttributesResult.UnionWith(currentProductTypeAttributes);

        // If it's a root product type or has no parent, return current attributes
        if (productType.ParentProductType is null && productType.ParentProductTypeId is null)
        {
            return productTypeAttributesResult.Count > 0
                ? Result.Success(productTypeAttributesResult)
                : Result.Failure<HashSet<ProductTypeAttributeDto>>(ProductTypeErrors.NotFound);
        }

        // Traverse up the product type hierarchy to collect parent attributes
        var currentTraversalProductType = productType;
        for (int i = productType.Level; i > 1; i--)
        {
            _dbContext.Entry(currentTraversalProductType)
                .Reference(pt => pt.ParentProductType)
                .Load();

            if (currentTraversalProductType.ParentProductType is null)
                break;

            var parentAttributes = await GetAttributesForSingleProductType(
                currentTraversalProductType.ParentProductTypeId!.Value,
                true,
                cancellationToken
            );

            productTypeAttributesResult.UnionWith(parentAttributes);


            currentTraversalProductType = currentTraversalProductType.ParentProductType;
        }

        return Result.Success(productTypeAttributesResult);
    }

    private async Task<HashSet<ProductTypeAttributeDto>> GetAttributesForSingleProductType(
        int productTypeId,
        bool isParentAttribute,
        CancellationToken cancellationToken)
    {
        var productTypeAttributeMappings = await _dbContext.ProductTypeAttributeMappings
            .Where(ptam => ptam.ProductTypeId == productTypeId)
            .Include(ptam => ptam.ProductTypeAttribute)
            .ThenInclude(pa => pa.Values)
            .Include(ptam => ptam.ProductType)
                .ThenInclude(pt => pt.ParentProductType)
            .ToListAsync(cancellationToken);

        return new HashSet<ProductTypeAttributeDto>(
            productTypeAttributeMappings.Select(ptam => new ProductTypeAttributeDto()
            {
                Id = ptam.ProductTypeAttribute.Id,
                Name = ptam.ProductTypeAttribute.Name,
                IsInherited = isParentAttribute,
                Values = _mapper.Map<List<ProductTypeAttributeValueDto>>(ptam.ProductTypeAttribute.Values)
            })
        );
    }
}