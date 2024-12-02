using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Users.GetUserShippingAddresses;

public record GetUserShippingAddressesQuery(int UserId) : IRequest<Result<List<GetUserShippingAddressesResponse>>>;

public class GetUserShippingAddressesHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<GetUserShippingAddressesQuery, Result<List<GetUserShippingAddressesResponse>>>
{
    public async Task<Result<List<GetUserShippingAddressesResponse>>> Handle(GetUserShippingAddressesQuery request, CancellationToken cancellationToken)
    {
        var shippingAddresses = await dbContext.ShippingAddresses
            .Where(sa => sa.CustomerId == request.UserId)
            .ToListAsync(cancellationToken);

        if (shippingAddresses is null)
        {
            return Result.Failure<List<GetUserShippingAddressesResponse>>(UserErrors.ShippingAddressNotFound);
        }

        return mapper.Map<List<GetUserShippingAddressesResponse>>(shippingAddresses);
    }
}
