using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Application.Features.Users.UpdateShippingAddress;

public record UpdateShippingAddressCommand(
    int Id,
    int CustomerId,
    string ReceiverName,
    string PhoneNumber,
    int ProvinceId,
    string ProvinceName,
    int DistrictId,
    string DistrictName,
    string CommuneCode,
    string CommuneName,
    string DetailAddress,
    bool IsDefault,
    AddressType Type
) : IRequest<Result<UpdateShippingAddressResponse>>;

public class UpdateShippingAddressCommandHandler(
    IApplicationDbContext dbContext,
    ILogger<UpdateShippingAddressCommandHandler> logger
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

        // start transaction
        using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);

        try
        {
            shippingAddress.CustomerId = request.CustomerId;
            shippingAddress.ReceiverName = request.ReceiverName;
            shippingAddress.PhoneNumber = request.PhoneNumber;
            shippingAddress.ProvinceId = request.ProvinceId;
            shippingAddress.ProvinceName = request.ProvinceName;
            shippingAddress.DistrictId = request.DistrictId;
            shippingAddress.DistrictName = request.DistrictName;
            shippingAddress.CommuneCode = request.CommuneCode;
            shippingAddress.CommuneName = request.CommuneName;
            shippingAddress.DetailAddress = request.DetailAddress;
            shippingAddress.IsDefault = request.IsDefault;
            shippingAddress.Type = request.Type;


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
                shippingAddress.Type
            );

            await transaction.CommitAsync(cancellationToken);

            logger.LogInformation("Updated shipping address with id {ShippingAddressId}", shippingAddress.Id);

            return Result.Success(response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error updating shipping address with id {ShippingAddressId}", shippingAddress.Id);
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<UpdateShippingAddressResponse>(UserErrors.UpdateShippingAddressFailed);
        }
    }
}
