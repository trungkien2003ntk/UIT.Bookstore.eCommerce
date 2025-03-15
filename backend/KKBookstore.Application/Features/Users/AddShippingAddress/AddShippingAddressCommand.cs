using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Customers;
using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Domain.Shared.Users;
using KKBookstore.Domain.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace KKBookstore.Application.Features.Users.AddShippingAddress;

public record AddShippingAddressCommand : IRequest<Result<AddShippingAddressResponse>>
{
    public int CustomerId { get; init; }

    [Required]
    [StringLength(ShippingAddressConsts.ReceiverNameMaxLength)]
    public string ReceiverName { get; init; } = null!;

    [Required]
    public string PhoneNumber { get; init; } = null!;

    public int ProvinceId { get; init; }

    [Required]
    [StringLength(AddressConsts.ProvinceNameMaxLength)]
    public string ProvinceName { get; init; } = null!;

    public int DistrictId { get; init; }

    [Required]
    [StringLength(AddressConsts.DistrictNameMaxLength)]
    public string DistrictName { get; init; } = null!;

    [Required]
    public string CommuneCode { get; init; } = null!;

    [Required]
    [StringLength(AddressConsts.CommuneNameMaxLength)]
    public string CommuneName { get; init; } = null!;

    [Required]
    [StringLength(AddressConsts.DetailAddressMaxLength)]
    public string DetailAddress { get; init; } = null!;

    public bool IsDefault { get; init; }

    [Required]
    public AddressType Type { get; init; }
}

public class AddShippingAddressCommandHandler(
    IApplicationDbContext dbContext,
    ILogger<AddShippingAddressCommandHandler> logger
) : IRequestHandler<AddShippingAddressCommand, Result<AddShippingAddressResponse>>
{
    public async Task<Result<AddShippingAddressResponse>> Handle(AddShippingAddressCommand request, CancellationToken cancellationToken)
    {
        var createShippingAddressResult = ShippingAddress.Create(
            request.CustomerId,
            request.ReceiverName,
            request.PhoneNumber,
            request.ProvinceId,
            request.ProvinceName,
            request.DistrictId,
            request.DistrictName,
            request.CommuneCode,
            request.CommuneName,
            request.IsDefault,
            request.DetailAddress,
            request.Type
        );

        if (createShippingAddressResult.IsFailure)
        {
            return Result.Failure<AddShippingAddressResponse>(createShippingAddressResult.Error);
        }

        var shippingAddress = createShippingAddressResult.Value;
        // start transaction
        using var transaction = await dbContext.BeginTransactionAsync(cancellationToken);

        try
        {
            dbContext.ShippingAddresses.Add(shippingAddress);
            await dbContext.SaveChangesAsync(cancellationToken);

            var response = new AddShippingAddressResponse()
            {
                Id = shippingAddress.Id,
                CustomerId = shippingAddress.CustomerId,
                ReceiverName = shippingAddress.ReceiverName,
                PhoneNumber = shippingAddress.PhoneNumber,
                ProvinceId = shippingAddress.ProvinceId,
                ProvinceName = shippingAddress.ProvinceName,
                DistrictId = shippingAddress.DistrictId,
                DistrictName = shippingAddress.DistrictName,
                CommuneCode = shippingAddress.CommuneCode,
                CommuneName = shippingAddress.CommuneName,
                DetailAddress = shippingAddress.DetailAddress,
                IsDefault = shippingAddress.IsDefault,
                Type = shippingAddress.Type,
            };

            await transaction.CommitAsync(cancellationToken);

            logger.LogInformation("Added shipping address with id {ShippingAddressId}", shippingAddress.Id);

            return Result.Success(response);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error adding shipping address with id {ShippingAddressId}", shippingAddress.Id);
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<AddShippingAddressResponse>(UserErrors.AddShippingAddressFailed);
        }
    }
}
