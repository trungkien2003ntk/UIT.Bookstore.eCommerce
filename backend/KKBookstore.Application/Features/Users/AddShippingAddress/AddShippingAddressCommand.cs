using AutoMapper;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Shared.Customers;
using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Domain.Shared.Users;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace KKBookstore.Application.Features.Users.AddShippingAddress;

public record AddShippingAddressCommand : IRequest<Result<AddShippingAddressResponse>>
{
    public int UserId { get; init; }

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
    IMapper mapper
) : IRequestHandler<AddShippingAddressCommand, Result<AddShippingAddressResponse>>
{
    public async Task<Result<AddShippingAddressResponse>> Handle(AddShippingAddressCommand request, CancellationToken cancellationToken)
    {
        var shippingAddress = mapper.Map<ShippingAddress>(request);

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

        return Result.Success(response);
    }
}
