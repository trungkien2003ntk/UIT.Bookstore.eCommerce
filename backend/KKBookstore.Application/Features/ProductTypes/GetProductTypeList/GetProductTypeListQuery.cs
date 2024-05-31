using AutoMapper;
using AutoMapper.QueryableExtensions;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeList;

public record GetProductTypeListQuery : IRequest<Result<List<ProductTypeGeneralDto>>>
{
    // filter properties
    public bool Flatten { get; set; } = false;
}

public class GetProductTypeListQueryHandler(
    IApplicationDbContext _context,
    IMapper _mapper
) : IRequestHandler<GetProductTypeListQuery, Result<List<ProductTypeGeneralDto>>>
{
    public async Task<Result<List<ProductTypeGeneralDto>>> Handle(GetProductTypeListQuery request, CancellationToken cancellationToken)
    {
        if (request.Flatten)
        {
            // Fetch all product types from the database
            var productTypes = await _context.ProductTypes
                .ProjectTo<ProductTypeGeneralDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return productTypes;
        }
        else
        {
            // Fetch all product types from the database
            var productTypes = await _context.ProductTypes
                .ProjectTo<ProductTypeGeneralDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // Build a lookup table to find children for each product type
            var lookup = productTypes.ToLookup(p => p.ParentProductTypeId);

            // Build the hierarchy
            foreach (var productType in productTypes)
            {
                productType.ChildProductTypes = lookup[productType.Id].ToList();
            }

            // Filter to get only root product types (those without a parent)
            var rootProductTypes = productTypes.Where(p => p.ParentProductTypeId == null).ToList();

            return rootProductTypes;
        }
    }
}