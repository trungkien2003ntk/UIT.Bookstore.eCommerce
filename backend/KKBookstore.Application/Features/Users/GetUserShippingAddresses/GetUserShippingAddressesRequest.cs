using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Users.GetUserShippingAddresses;

public record GetUserShippingAddressesRequest(int UserId) : IRequest<Result<List<GetUserShippingAddressesResponse>>>;

public class GetUserShippingAddressesHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetUserShippingAddressesRequest, Result<List<GetUserShippingAddressesResponse>>>
{
    public async Task<Result<List<GetUserShippingAddressesResponse>>> Handle(GetUserShippingAddressesRequest request, CancellationToken cancellationToken)
    {
        var shippingAddresses = await dbContext.ShippingAddresses
            .Where(sa => sa.UserId == request.UserId)
            .ToListAsync(cancellationToken);

        if (shippingAddresses is null)
        {
            return Result.Failure<List<GetUserShippingAddressesResponse>>(UserErrors.ShippingAddressNotFound);
        }

        return mapper.Map<List<GetUserShippingAddressesResponse>>(shippingAddresses);
    }
}
