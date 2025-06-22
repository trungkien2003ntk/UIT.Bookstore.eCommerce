using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Orders.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.Orders.GetOrderDetail;

public record GetOrderDetailQuery(int Id) : IRequest<Result<GetOrderDetailResponse>>;

public class GetOrderDetailHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetOrderDetailQuery, Result<GetOrderDetailResponse>>
{
    public async Task<Result<GetOrderDetailResponse>> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .Where(o => o.Id == request.Id)
            .Include(o => o.Customer)
                .ThenInclude(c => c.CustomerType)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(pv => pv!.Product)
                        .ThenInclude(p => p.ProductType)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(pv => pv!.Product)
                        .ThenInclude(p => p.ProductImages)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(pv => pv!.Product)
                        .ThenInclude(p => p.AttributeProductValues)
                            .ThenInclude(apv => apv.AttributeValue)
                                .ThenInclude(av => av.ProductTypeAttribute)
            .Include(o => o.DeliveryMethod)
            .Include(o => o.PaymentMethod)
            .Include(o => o.ShippingAddress)
            .Include(o => o.ShippingDiscountVoucher)
            .Include(o => o.PriceDiscountVoucher)
            .Include(o => o.OrderFulfillments)
                .ThenInclude(of => of.Branch)
                    .ThenInclude(b => b!.Address)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return Result.Failure<GetOrderDetailResponse>(OrderErrors.OrderNotFound);
        }

        // Load product variant option values separately to avoid EF complexity
        var productVariantIds = order.OrderLines
            .Where(ol => ol.ProductVariantId.HasValue)
            .Select(ol => ol.ProductVariantId!.Value)
            .ToList();

        var variantOptionValues = await dbContext.ProductVariantOptionValues
            .Where(pvov => productVariantIds.Contains(pvov.ProductVariantId))
            .Include(pvov => pvov.Option)
            .Include(pvov => pvov.OptionValue)
            .ToListAsync(cancellationToken);

        // Calculate price summary
        var subtotal = order.Subtotal;
        var taxAmount = subtotal * order.TaxRate;
        var shippingFee = order.ShippingFee;
        var productDiscount = order.ProductDiscount; ;

        var priceDiscountAmount = 0m;
        var shippingDiscountAmount = 0m;

        if (order.PriceDiscountVoucher != null)
        {
            priceDiscountAmount = order.PriceDiscountVoucher.GetDiscountValue(subtotal);
        }

        if (order.ShippingDiscountVoucher != null)
        {
            shippingDiscountAmount = order.ShippingDiscountVoucher.GetDiscountValue(shippingFee);
        }

        var total = subtotal + taxAmount + shippingFee - productDiscount - priceDiscountAmount - shippingDiscountAmount;
        var paidAmount = order.CalculateTotal(); // This should match our calculation
        var remainingAmount = total - paidAmount;

        var response = new GetOrderDetailResponse()
        {
            Id = order.Id,
            OrderNumber = order.OrderNumber,
            OrderWhen = order.OrderWhen,
            DueWhen = order.DueWhen,
            PaidWhen = order.PaidWhen,
            ExpectedDeliveryWhen = order.ExpectedDeliveryWhen,
            PickingCompletedWhen = order.PickingCompletedWhen,
            ConfirmedDeliveryWhen = order.ConfirmedDeliveryWhen,
            ConfirmedReceivedWhen = order.ConfirmedReceivedWhen,

            Status = order.Status.ToString(),
            CustomerId = order.CustomerId,
            Comment = order.Comment,
            DeliveryInstruction = order.DeliveryInstruction,

            PriceSummary = new GetOrderDetailResponse.OrderPriceSummary
            {
                Subtotal = subtotal,
                TaxRate = order.TaxRate,
                TaxAmount = taxAmount,
                ShippingFee = shippingFee,
                OrderVoucherDiscount = priceDiscountAmount,
                ShippingDiscount = shippingDiscountAmount,
                ProductDiscount = productDiscount,
                Total = total,
                PaidAmount = paidAmount,
                RemainingAmount = remainingAmount
            },

            DeliveryMethod = new DeliveryMethodDto
            {
                Id = order.DeliveryMethod!.Id,
                Name = order.DeliveryMethod.Name,
                Description = order.DeliveryMethod.Description,
            },

            PaymentMethod = new PaymentMethodDto
            {
                Id = order.PaymentMethod!.Id,
                Name = order.PaymentMethod.Name,
                Description = order.PaymentMethod.Description,
            },

            ShippingAddress = new ShippingAddressDto
            {
                Id = order.ShippingAddress!.Id,
                DetailedFullAddress = $"{order.ShippingAddress.DetailAddress}, " +
                    $"{order.ShippingAddress.CommuneName}, " +
                    $"{order.ShippingAddress.DistrictName}, " +
                    $"{order.ShippingAddress.ProvinceName}",
                ReceiverName = order.ShippingAddress.ReceiverName,
                PhoneNumber = order.ShippingAddress.PhoneNumber,
            },

            Customer = new GetOrderDetailResponse.CustomerSummaryDto
            {
                Id = order.Customer.Id,
                FullName = order.Customer.FullName ?? $"{order.Customer.FirstName} {order.Customer.LastName}",
                Email = order.Customer.Email,
                PhoneNumber = order.Customer.PhoneNumber ?? "",
                CustomerType = order.Customer.CustomerType?.Name ?? "Regular"
            },
            PriceDiscountVoucher = order.PriceDiscountVoucher != null ? new GetOrderDetailResponse.DiscountVoucherDto
            {
                Id = order.PriceDiscountVoucher.Id,
                Name = order.PriceDiscountVoucher.Name,
                Code = order.PriceDiscountVoucher.Code,
                VoucherType = order.PriceDiscountVoucher.VoucherType.ToString(),
                DiscountValue = order.PriceDiscountVoucher.Value,
                MaxDiscountValue = order.PriceDiscountVoucher.MaximumDiscountValue ?? 0,
                IsPercentage = order.PriceDiscountVoucher.ValueType == DiscountValueType.Percentage
            } : null,

            ShippingDiscountVoucher = order.ShippingDiscountVoucher != null ? new GetOrderDetailResponse.DiscountVoucherDto
            {
                Id = order.ShippingDiscountVoucher.Id,
                Name = order.ShippingDiscountVoucher.Name,
                Code = order.ShippingDiscountVoucher.Code,
                VoucherType = order.ShippingDiscountVoucher.VoucherType.ToString(),
                DiscountValue = order.ShippingDiscountVoucher.Value,
                MaxDiscountValue = order.ShippingDiscountVoucher.MaximumDiscountValue ?? 0,
                IsPercentage = order.ShippingDiscountVoucher.ValueType == DiscountValueType.Percentage
            } : null,

            OrderLines = order.OrderLines.Select(ol => new OrderLineDto
            {
                Id = ol.Id,
                OrderId = ol.OrderId,
                ProductId = ol.ProductVariant?.ProductId ?? 0,
                ProductVariantId = ol.ProductVariantId,

                ProductName = ol.ProductVariant?.Product?.Name ?? "Unknown Product",
                ProductDescription = ol.ProductVariant?.Product?.Description,
                ProductTypeName = ol.ProductVariant?.Product?.ProductType?.DisplayName ?? "Unknown Type",
                ProductVariantName = ol.ProductVariant?.VariantName,

                UnitPrice = ol.UnitPrice,
                RecommendedRetailPrice = ol.ProductVariant?.RecommendedRetailPrice,
                Quantity = ol.Quantity,
                DiscountAmount = 0m, // Calculate if you have line-level discounts

                ThumbnailUrl = ol.ProductVariant?.GetThumbnailImageUrl() ??
                              ol.ProductVariant?.Product?.ProductImages?.FirstOrDefault()?.ThumbnailImageUrl ?? "",
                LargeImageUrl = ol.ProductVariant?.Product?.ProductImages?.FirstOrDefault()?.LargeImageUrl,

                VariantOptions = ol.ProductVariantId.HasValue
                    ? variantOptionValues
                        .Where(vov => vov.ProductVariantId == ol.ProductVariantId.Value)
                        .Select(vov => new OrderLineDto.ProductOptionDto
                        {
                            OptionName = vov.Option?.Name ?? "Unknown Option",
                            OptionValue = vov.OptionValue?.Value ?? "Unknown Value"
                        })
                    : [],
                ProductAttributes = ol.ProductVariant?.Product?.AttributeProductValues?.Select(apv => new OrderLineDto.ProductAttributeDto
                {
                    AttributeName = apv.AttributeValue?.ProductTypeAttribute?.Name ?? "Unknown Attribute",
                    AttributeValue = apv.AttributeValue?.Value ?? "Unknown Value"
                }) ?? []
            }),

            Fulfillments = order.OrderFulfillments.Select(of => new GetOrderDetailResponse.OrderFulfillmentDto
            {
                Id = of.Id,
                BranchId = of.BranchId,
                BranchName = of.Branch?.Name ?? "Unknown Branch",
                BranchAddress = of.Branch?.Address != null
                    ? $"{of.Branch.Address.DetailAddress}, {of.Branch.Address.CommuneName}, {of.Branch.Address.DistrictName}, {of.Branch.Address.ProvinceName}"
                    : "Unknown Address",
                Status = of.Status.ToString(),
                AllocatedQuantity = of.OrderLineAllocations?.Sum(ola => ola.Quantity) ?? 0,
                FulfilledQuantity = of.OrderLineAllocations?.Sum(ola => ola.Quantity) ?? 0, // Same as allocated for now
                PackagingStartedWhen = of.PackagingStartedWhen,
                PackagingCompletedWhen = of.PackagingCompletedWhen,
                TrackingNumber = of.Notes, // Use notes field for tracking info for now
                ShippingCarrier = "GHN" // Since we're using GHN for shipping
            })
        };

        return response;
    }
}