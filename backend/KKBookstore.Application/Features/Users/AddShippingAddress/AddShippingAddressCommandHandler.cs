using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using MediatR;

namespace KKBookstore.Application.Features.Users.AddShippingAddress;

public class AddShippingAddressCommandHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<AddShippingAddressCommand, Result<AddShippingAddressResponse>>
{
    public async Task<Result<AddShippingAddressResponse>> Handle(AddShippingAddressCommand request, CancellationToken cancellationToken)
    {
        var shippingAddress = mapper.Map<ShippingAddress>(request);

        dbContext.ShippingAddresses.Add(shippingAddress);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<AddShippingAddressResponse>(shippingAddress);

        return Result.Success(response);
    }
}
