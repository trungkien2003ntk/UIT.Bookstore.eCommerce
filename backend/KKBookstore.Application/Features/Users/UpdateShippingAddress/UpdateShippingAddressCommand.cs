using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Users.UpdateShippingAddress;

public record UpdateShippingAddressCommand(
    int Id,
    int UserId,
    string ReceiverName,
    string PhoneNumber,
    string Province,
    string District,
    string Commune,
    string DetailAddress,
    bool IsDefault,
    string AddressType
) : IRequest<Result<UpdateShippingAddressResponse>>;

public class UpdateShippingAddressCommandHandler(
    IApplicationDbContext dbContext,
    IMapper mapper
) : IRequestHandler<UpdateShippingAddressCommand, Result<UpdateShippingAddressResponse>>
{
    public async Task<Result<UpdateShippingAddressResponse>> Handle(UpdateShippingAddressCommand request, CancellationToken cancellationToken)
    {
        var shippingAddress = await dbContext.ShippingAddresses
            .FirstOrDefaultAsync(sa => sa.Id == request.Id, cancellationToken);

        if (shippingAddress is null)
        {
            return Result.Failure<UpdateShippingAddressResponse>(UserErrors.ShippingAddressNotFound);
        }

        mapper.Map(request, shippingAddress);

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<UpdateShippingAddressResponse>(shippingAddress);

        return Result.Success(response);
    }
}
