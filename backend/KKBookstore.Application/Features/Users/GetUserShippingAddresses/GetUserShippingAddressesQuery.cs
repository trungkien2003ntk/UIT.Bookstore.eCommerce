using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
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
            .Where(sa => sa.CustomerId == request.UserId && !sa.IsDeleted)
            .ToListAsync(cancellationToken);

        if (shippingAddresses is null)
        {
            return Result.Success(new List<GetUserShippingAddressesResponse>());
        }

        return mapper.Map<List<GetUserShippingAddressesResponse>>(shippingAddresses);
    }
}
