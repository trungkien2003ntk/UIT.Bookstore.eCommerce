using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Products.GetUnitMeasures;

public record GetUnitMeasuresQuery() : IRequest<Result<ListResult<UnitMeasureDto>>>;

public class GetUnitMeasuresQueryHandler : IRequestHandler<GetUnitMeasuresQuery, Result<ListResult<UnitMeasureDto>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetUnitMeasuresQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ListResult<UnitMeasureDto>>> Handle(GetUnitMeasuresQuery request, CancellationToken cancellationToken)
    {
        var unitMeasures = await _dbContext.UnitMeasures
            .Select(x => new UnitMeasureDto(x.Name, x.Description))
            .ToListAsync(cancellationToken);

        var result = new ListResult<UnitMeasureDto>(unitMeasures);

        return result;
    }
}