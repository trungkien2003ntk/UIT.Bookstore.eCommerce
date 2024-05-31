using AutoMapper;
using AutoMapper.QueryableExtensions;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Orders.GetOrderDetail;

public record GetOrderDetailQuery(int Id) : IRequest<Result<GetOrderDetailResponse>>;

public class GetOrderDetailHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetOrderDetailQuery, Result<GetOrderDetailResponse>>
{
    public async Task<Result<GetOrderDetailResponse>> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .Where(o => o.Id == request.Id)
            .ProjectTo<GetOrderDetailResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return Result.Failure<GetOrderDetailResponse>(OrderErrors.OrderNotFound);
        }

        var result = mapper.Map<GetOrderDetailResponse>(order);

        return Result.Success(result);
    }
}