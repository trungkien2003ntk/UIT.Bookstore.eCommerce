using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Features.Orders.Models;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Orders.GetOrderDetail;

public record GetOrderDetailQuery(int Id) : IRequest<Result<GetOrderDetailResponse>>;

public class GetOrderDetailHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetOrderDetailQuery, Result<GetOrderDetailResponse>>
{
    public async Task<Result<GetOrderDetailResponse>> Handle(GetOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .Where(o => o.Id == request.Id)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(s => s.ProductVariantOptionValues)
                        .ThenInclude(sov => sov.Option)
                            .ThenInclude(sov => sov.OptionValues)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(s => s.ProductVariantOptionValues)
                        .ThenInclude(sov => sov.OptionValue)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(s => s.Product)
            .Include(o => o.DeliveryMethod)
            .Include(o => o.PaymentMethod)
            .Include(o => o.ShippingAddress)
            .Include(o => o.ShippingDiscountVoucher)
            .Include(o => o.PriceDiscountVoucher)
            //.AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return Result.Failure<GetOrderDetailResponse>(OrderErrors.OrderNotFound);
        }

        var response = new GetOrderDetailResponse()
        {
            Comment = order.Comment,
            ConfirmedDeliveryWhen = order.ConfirmedDeliveryWhen,
            ConfirmedReceivedWhen = order.ConfirmedReceivedWhen,
            CustomerId = order.CustomerId,
            DeliveryInstruction = order.DeliveryInstruction,
            DiscountVoucherId = order.PriceDiscountVoucherId,
            DueWhen = order.DueWhen,
            ExpectedDeliveryWhen = order.ExpectedDeliveryWhen,
            OrderNumber = order.OrderNumber,
            OrderWhen = order.OrderWhen,
            PaidAmount = order.CalculateTotal(),
            PickingCompletedWhen = order.PickingCompletedWhen,
            Status = order.Status.ToString(),
            Subtotal = order.Subtotal,
            TaxRate = order.TaxRate,
            Id = order.Id,

            DeliveryMethod = new()
            {
                Id = order.DeliveryMethod.Id,
                Name = order.DeliveryMethod.Name,
                Description = order.DeliveryMethod.Description,
            },
            PaymentMethod = new()
            {
                Id = order.PaymentMethod.Id,
                Name = order.PaymentMethod.Name,
                Description = order.PaymentMethod.Description,
            },
            ShippingAddress = new()
            {
                Id = order.ShippingAddress.Id,
                DetailedFullAddress = order.ShippingAddress.DetailAddress,
                ReceiverName = order.ShippingAddress.ReceiverName,
                PhoneNumber = order.ShippingAddress.PhoneNumber,
            },
            OrderLines = order.OrderLines.Select(ol => new OrderLineDto
            {
                Id = ol.Id,
                ProductName = ol.ProductVariant.Product.Name,
                Quantity = ol.Quantity,
                UnitPrice = ol.UnitPrice,
                RecommendedRetailPrice = ol.ProductVariant.RecommendedRetailPrice,
                ProductVariantOptionName = ol.ProductVariant.VariantName,
                OrderId = ol.OrderId,
                ProductVariantId = ol.ProductVariantId,
                ThumbnailUrl = ol.ProductVariant.GetThumbnailImageUrl() ?? ""
            })
        };

        return response;
    }
}