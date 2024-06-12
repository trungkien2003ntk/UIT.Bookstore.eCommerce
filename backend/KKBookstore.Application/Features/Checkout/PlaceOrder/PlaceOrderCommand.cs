using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace KKBookstore.Application.Features.Checkout.PlaceOrder;

public record PlaceOrderCommand : IRequest<Result<PlaceOrderResponse>>
{
    public int UserId { get; init; }
    public List<int> ItemIds { get; init; } = [];
    public int ShippingAddressId { get; init; }
    public int PaymentMethodId { get; init; }
    public int DeliveryMethodId { get; init; }
    public decimal ShippingFee { get; init; }
    public DateTimeOffset ExpectedDeliveryWhen { get; init; }
    public int? OrderDiscountVoucherId { get; init; }
    public int? ShippingVoucherId { get; init; }
    public string Note { get; init; } = string.Empty;
    public string? PaymentReturnUrl { get; init; }
    public string IpAddress { get; init; } = string.Empty;
}

public class PlaceOrderHandler(
    DefaultOrderProcessor orderProcessor
) : IRequestHandler<PlaceOrderCommand, Result<PlaceOrderResponse>>
{
    private readonly OrderProcessor _orderProcessor = orderProcessor;

    public async Task<Result<PlaceOrderResponse>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        return await _orderProcessor.ProcessOrder(request, cancellationToken);
    }
}