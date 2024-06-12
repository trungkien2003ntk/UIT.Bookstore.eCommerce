using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Application.Features.Checkout.Confirm.ConfirmCheckoutResponse;

namespace KKBookstore.Application.Features.Checkout.Confirm;

public record ConfirmCheckoutQuery : IRequest<Result<ConfirmCheckoutResponse>>
{
    public int UserId { get; init; }
    public List<int> ItemIds { get; init; } = [];
    public int? OrderDiscountVoucherId { get; init; }
    public int? ShippingDiscountVoucherId { get; init; }
}

public class ConfirmCheckoutHandler(
    IApplicationDbContext dbContext,
    IShippingService shippingService
) : IRequestHandler<ConfirmCheckoutQuery, Result<ConfirmCheckoutResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IShippingService _shippingService = shippingService;

    public async Task<Result<ConfirmCheckoutResponse>> Handle(ConfirmCheckoutQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var checkoutItems = await GetCartItems(userId, request.ItemIds, cancellationToken);

        if (checkoutItems.Count == 0)
        {
            return Result.Failure<ConfirmCheckoutResponse>(ShoppingCartError.ItemNotFound);
        }

        var productsInCart = await ExtractDistinctProductInCart(checkoutItems);

        var shippingAddresses = await _dbContext.ShippingAddresses
            .Where(ca => ca.UserId == userId)
            .ToListAsync(cancellationToken);

        var defaultAddress = shippingAddresses.Find(ca => ca.UserId == userId && ca.IsDefault);

        int shippingFee;
        DateTimeOffset expectedDeliveryTime;
        if (defaultAddress == null)
        {
            shippingFee = 0;
            expectedDeliveryTime = DateTimeOffset.Now.AddDays(7);
        }
        else
        {
            var getCustomerProvinceIdResult = await _shippingService.FindProvinceAsync(defaultAddress.Province, cancellationToken);
            if (getCustomerProvinceIdResult.IsFailure)
            {
                return Result.Failure<ConfirmCheckoutResponse>(getCustomerProvinceIdResult.Error);
            }

            var getCustomerDistrictIdResult = await _shippingService.FindDistrictIdAsync(getCustomerProvinceIdResult.Value, defaultAddress.District, cancellationToken);
            if (getCustomerDistrictIdResult.IsFailure)
            {
               return Result.Failure<ConfirmCheckoutResponse>(getCustomerDistrictIdResult.Error);
            }

            var getCustomerCommuneCodeResult = await _shippingService.FindCommuneCodeAsync(getCustomerDistrictIdResult.Value, defaultAddress.Commune, cancellationToken);
            if (getCustomerCommuneCodeResult.IsFailure)
            {
                return Result.Failure<ConfirmCheckoutResponse>(getCustomerCommuneCodeResult.Error);
            }

            var customerDistrictId = getCustomerDistrictIdResult.Value;
            var customerCommuneCode = getCustomerCommuneCodeResult.Value;

            Dimension overallDimension = new()
            {
                Height = checkoutItems.Max(ci => ci.Sku.Dimension.Height),
                Width = checkoutItems.Sum(ci => ci.Sku.Dimension.Width),
                Length = checkoutItems.Max(ci => ci.Sku.Dimension.Length)
            };

            var totalWeight = checkoutItems.Sum(ci => ci.Sku.Weight);

            var shippingFeeRequest = new ShippingFeeRequest
            {
                ToDistrictId = customerDistrictId,
                ToWardCode = customerCommuneCode,
                Height = (int)overallDimension.Height,
                Length = (int)overallDimension.Length,
                Width = (int)overallDimension.Width,
                Weight = totalWeight,
            };

            var getShippingFeeResult = await _shippingService.GetShippingFeeAsync(shippingFeeRequest, cancellationToken);
            if (getShippingFeeResult.IsFailure)
            {
                return Result.Failure<ConfirmCheckoutResponse>(getShippingFeeResult.Error);
            }
            shippingFee = getShippingFeeResult.Value.Data.Total;

            var expectedDeliveryTimeRequest = new ExpectDeliveryTimeRequest
            {
                FromDistrictId = _shippingService.ShopDistrictId,
                ToDistrictId = customerDistrictId,
                FromWardCode = _shippingService.ShopCommuneCode,
                ToWardCode = customerCommuneCode
            };
            var expectedDeliveryTimeResult = await _shippingService.GetExpectedDeliveryTime(expectedDeliveryTimeRequest, cancellationToken);
            if (expectedDeliveryTimeResult.IsFailure)
            {
                return Result.Failure<ConfirmCheckoutResponse>(expectedDeliveryTimeResult.Error);
            }

            expectedDeliveryTime = expectedDeliveryTimeResult.Value;
        }

        var itemSubtotal = checkoutItems.Sum(i => i.TotalUnitPrice);
        var orderSubtotal = itemSubtotal+ shippingFee;

        var shippingVoucher = await _dbContext.DiscountVouchers
            .Where(dv => dv.Id == request.ShippingDiscountVoucherId)
            .Include(dv => dv.VoucherUsages)
            .FirstOrDefaultAsync(cancellationToken);

        var orderVoucher = await _dbContext.DiscountVouchers
            .Where(dv => dv.Id == request.OrderDiscountVoucherId)
            .Include(dv => dv.VoucherUsages)
            .FirstOrDefaultAsync(cancellationToken);

        var shippingDiscount = 0m;
        var orderDiscount = 0m;
        if (shippingVoucher is null)
        {
            if (request.ShippingDiscountVoucherId is not null)
                return Result.Failure<ConfirmCheckoutResponse>(DiscountVoucherErrors.ShippingVoucherNotFound);
        }
        else
        {
            shippingDiscount = shippingVoucher.GetDiscountValue(shippingFee);
        }

        if (orderVoucher is null)
        {
            if (request.OrderDiscountVoucherId is not null)
                return Result.Failure<ConfirmCheckoutResponse>(DiscountVoucherErrors.OrderVoucherNotFound);
        }
        else
        {
            orderDiscount = orderVoucher.GetDiscountValue(itemSubtotal);
        }



        var paymentMethods = _dbContext.PaymentMethods.ToList();
        var deliveryMethods = _dbContext.DeliveryMethods.ToList();
        var orderTotal = orderSubtotal - orderDiscount - shippingDiscount;

        var response = new ConfirmCheckoutResponse
        {
            Items = checkoutItems.Select(i =>
            {
                var product = productsInCart.First(p => p.Id == i.Sku.ProductId);
                return new CheckoutItemDto()
                {
                    Id = i.Id,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    SkuId = i.SkuId,
                    SkuName = i.Sku.SkuName,
                    Quantity = i.Quantity,
                    ImageUrl = i.Sku.GetThumbnailImageUrl(),
                    UnitPrice = i.Sku.UnitPrice,
                    TotalPrice = i.TotalUnitPrice,
                };
            }).ToList(),

            PriceSummary = new()
            {
                Subtotal = itemSubtotal,
                ShippingFee = shippingFee,
                ShippingDiscount = shippingDiscount,
                OrderVoucherDiscount = orderDiscount,
                Total = orderTotal
            },

            PaymentMethods = paymentMethods.Select(pm => new PaymentMethodDto
            {
                Id = pm.Id,
                Name = pm.Name,
                Description = pm.Description
            }).ToList(),

            DeliveryMethods = deliveryMethods.Select(dm => new DeliveryMethodDto
            {
                Id = dm.Id,
                Name = dm.Name,
                Description = dm.Description,
                ExpectedDeliveryWhen = expectedDeliveryTime
            }).ToList(),

            ShippingAddresses = shippingAddresses.Select(sa => new ShippingAddressDto
            {
                Id = sa.Id,
                UserId = sa.UserId,
                ReceiverName = sa.ReceiverName,
                PhoneNumber = sa.PhoneNumber,
                Province = sa.Province,
                District = sa.District,
                Commune = sa.Commune,
                DetailAddress = sa.DetailAddress,
                IsDefault = sa.IsDefault,
                AddressType = sa.AddressTypeEnum.ToString()
            }).ToList()
        };

        return Result.Success(response);
    }

    private async Task<List<ShoppingCartItem>> GetCartItems(int userId, List<int> itemIds, CancellationToken cancellationToken)
    {
        return await _dbContext.ShoppingCartItems
            .Where(sci => itemIds.Contains(sci.Id) && sci.CustomerId == userId)
            .Include(sci => sci.Sku)
                .ThenInclude(sku => sku.SkuOptionValues)
                    .ThenInclude(sov => sov.OptionValue)
            .Include(sci => sci.Sku)
                .ThenInclude(sku => sku.Dimension)
            .ToListAsync(cancellationToken);
    }

    private async Task<List<Product>> ExtractDistinctProductInCart(List<ShoppingCartItem> items)
    {
        // todo: use projection to reduce the amount of data fetched, increase performance
        var productIds = items.Select(ci => ci.Sku.ProductId).Distinct().ToList();
        var neededProducts = await _dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .Include(p => p.Options)                        // these are for 
                .ThenInclude(o => o.OptionValues)           // sku variations
            .Include(p => p.Skus)
                .ThenInclude(s => s.SkuOptionValues)        // these are for
                    .ThenInclude(sov => sov.OptionValue)    // sku thumbnail image
                        .ThenInclude(ov => ov.Option)       // this is for SkuInCart option names
            .Include(p => p.ProductImages)
            .ToListAsync();
        return neededProducts;
    }
}
