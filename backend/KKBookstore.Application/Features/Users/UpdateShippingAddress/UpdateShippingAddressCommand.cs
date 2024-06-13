using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.OrderAggregate;
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


        shippingAddress.UserId = request.UserId;
        shippingAddress.ReceiverName = request.ReceiverName;
        shippingAddress.PhoneNumber = request.PhoneNumber;
        shippingAddress.Province = request.Province;
        shippingAddress.District = request.District;
        shippingAddress.Commune = request.Commune;
        shippingAddress.DetailAddress = request.DetailAddress;
        shippingAddress.IsDefault = request.IsDefault;
        shippingAddress.AddressTypeEnum = Enum.Parse<AddressType>(request.AddressType);


        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new UpdateShippingAddressResponse
        (
            shippingAddress.Id,
            shippingAddress.UserId,
            shippingAddress.ReceiverName,
            shippingAddress.PhoneNumber,
            shippingAddress.Province,
            shippingAddress.District,
            shippingAddress.Commune,
            shippingAddress.DetailAddress,
            shippingAddress.IsDefault,
            shippingAddress.AddressTypeEnum.ToString()
        );

        return Result.Success(response);
    }
}
