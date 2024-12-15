using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.ProductTypeAttributes.GetProductTypeAttributeValues;

public record GetProductTypeAttributeValuesQuery(int ProductTypeAttributeId, string? SearchQuery = null)
    : IRequest<Result<ListResult<ProductTypeAttributeValueDto>>>;

public class GetProductTypeAttributeValuesQueryHandler : IRequestHandler<GetProductTypeAttributeValuesQuery, Result<ListResult<ProductTypeAttributeValueDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetProductTypeAttributeValuesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<ListResult<ProductTypeAttributeValueDto>>> Handle(GetProductTypeAttributeValuesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ProductTypeAttributeValues
            .Where(x => x.ProductTypeAttributeId == request.ProductTypeAttributeId);

        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            query = query.Where(x => x.Value.Contains(request.SearchQuery));
        }

        var values = await query
            .OrderBy(x => x.Value)
            .ToListAsync(cancellationToken);

        return Result.Success(new ListResult<ProductTypeAttributeValueDto>
        (
            values.Select(x => new ProductTypeAttributeValueDto
            {
                Id = x.Id,
                ProductTypeAttributeId = x.ProductTypeAttributeId,
                Value = x.Value
            }).ToList(),
            values.Count
        ));
    }
}
