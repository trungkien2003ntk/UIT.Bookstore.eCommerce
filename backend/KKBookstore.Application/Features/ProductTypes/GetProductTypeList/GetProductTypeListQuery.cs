using AutoMapper;
using AutoMapper.QueryableExtensions;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Application.Features.ProductTypes.GetProductTypeList.GetProductTypeListResponse;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeList;

public record GetProductTypeListQuery : IRequest<Result<GetProductTypeListResponse>>
{
    // filter properties
    public bool Flatten { get; set; } = false;
}

public class GetProductTypeListQueryHandler(
    IApplicationDbContext _context,
    IMapper _mapper
) : IRequestHandler<GetProductTypeListQuery, Result<GetProductTypeListResponse>>
{
    public async Task<Result<GetProductTypeListResponse>> Handle(GetProductTypeListQuery request, CancellationToken cancellationToken)
    {
        var defaultThumbnailImage = "https://cdn1.iconfinder.com/data/icons/basic-ui-elements-color-round/3/05-512.png";

        var productTypes = await _context.ProductTypes
            .Select(pt =>
                new ProductTypeGeneralDto
                {
                    Id = pt.Id,
                    ProductTypeCode = pt.ProductTypeCode,
                    Level = pt.Level,
                    ThumbnailImageUrl = _context.ProductImages
                        .Include(pi => pi.Product)
                        .Where(pi => pi.Product.ProductTypeId == pt.Id)
                        .Select(pi => pi.ThumbnailImageUrl)
                        .FirstOrDefault() ?? defaultThumbnailImage,
                    DisplayName = pt.DisplayName,
                    Description = pt.Description,
                    ParentProductTypeId = pt.ParentProductTypeId,
                    ChildProductTypes = new()
                })
            .ToListAsync(cancellationToken);


        if (request.Flatten)
        {
            var response = new GetProductTypeListResponse
            {
                ListItem = productTypes
            };

            return response;
        }
        else
        {
            // Build a lookup table to find children for each product type
            var lookup = productTypes.ToLookup(p => p.ParentProductTypeId);

            // Build the hierarchy
            foreach (var productType in productTypes)
            {
                productType.ChildProductTypes = lookup[productType.Id].ToList();
            }

            // Filter to get only root product types (those without a parent)
            var rootProductTypes = productTypes.Where(p => p.ParentProductTypeId == null).ToList();

            var response = new GetProductTypeListResponse
            {
                ListItem = rootProductTypes
            };

            return response;
        }
    }
}