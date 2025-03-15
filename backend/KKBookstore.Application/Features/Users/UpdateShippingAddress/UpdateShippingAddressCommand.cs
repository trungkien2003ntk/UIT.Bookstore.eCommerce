using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Users;
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


        shippingAddress.CustomerId = request.UserId;
        shippingAddress.ReceiverName = request.ReceiverName;
        shippingAddress.PhoneNumber = request.PhoneNumber;
        shippingAddress.ProvinceName = request.Province;
        shippingAddress.DistrictName = request.District;
        shippingAddress.CommuneName = request.Commune;
        shippingAddress.DetailAddress = request.DetailAddress;
        shippingAddress.IsDefault = request.IsDefault;


        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new UpdateShippingAddressResponse
        (
            shippingAddress.Id,
            shippingAddress.CustomerId,
            shippingAddress.ReceiverName,
            shippingAddress.PhoneNumber,
            shippingAddress.ProvinceId,
            shippingAddress.ProvinceName,
            shippingAddress.DistrictId,
            shippingAddress.DistrictName,
            shippingAddress.CommuneCode,
            shippingAddress.CommuneName,
            shippingAddress.DetailAddress,
            shippingAddress.IsDefault,
            shippingAddress.Type.ToString()
        );

        return Result.Success(response);
    }
}
