using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Products.GetProductOptions;

public record GetProductOptionsQuery(int ProductId) : IRequest<Result<ListResult<ProductOptionsDto>>>;

public class GetProductOptionsQueryHandler : IRequestHandler<GetProductOptionsQuery, Result<ListResult<ProductOptionsDto>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetProductOptionsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ListResult<ProductOptionsDto>>> Handle(GetProductOptionsQuery request, CancellationToken cancellationToken)
    {
        var productOptions = await _dbContext.ProductOptions
            .AsNoTracking()
            .Where(x => x.ProductId == request.ProductId)
            .Include(x => x.OptionValues)
            .Select(x => new ProductOptionsDto
            {
                Id = x.Id,
                VariantName = x.Name,
                Values = x.OptionValues.Select(y => new ProductOptionsDto.ProductOptionValueDto
                {
                    Id = y.Id,
                    Value = y.Value
                }).ToList()
            }).ToListAsync(cancellationToken);

        if (productOptions is null || productOptions.Count == 0)
        {
            return Result.Failure<ListResult<ProductOptionsDto>>(ProductOptionErrors.NotFound);
        }

        var result = new ListResult<ProductOptionsDto>(productOptions);

        return result;
    }
}
