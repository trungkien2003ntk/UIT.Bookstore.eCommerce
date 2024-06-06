using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Checkout.PlaceOrder;

public record PlaceOrderCommand : IRequest<Result<PlaceOrderResponse>>
{
    public int UserId { get; init; }
    public List<int> ItemIds { get; init; } = [];
    public int ShippingAddressId { get; init; }
    public int PaymentMethodId { get; init; }
    public int DeliveryMethodId { get; init; }
    public DateTimeOffset ExpectedDeliveryWhen { get; init; }
    public int? DiscountVoucherId { get; init; }
    public int? ShippingVoucherId { get; init; }
    public string Note { get; init; } = string.Empty;

    public string? PaymentReturnUrl { get; init; }
    public string IpAddress { get; init; } = string.Empty;
}

public class PlaceOrderHandler(
    IApplicationDbContext dbContext,
    IPaymentService paymentService
) : IRequestHandler<PlaceOrderCommand, Result<PlaceOrderResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly IPaymentService _paymentService = paymentService;

    public async Task<Result<PlaceOrderResponse>> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            var checkoutItems = await _dbContext.ShoppingCartItems
                .Where(i => request.ItemIds.Contains(i.Id))
                .Include(sci => sci.Sku)
                .ToListAsync(cancellationToken);

            // 1. Check Inventory:
            foreach (var item in checkoutItems)
            {
                if (item.Sku.Quantity < item.Quantity)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure<PlaceOrderResponse>(OrderErrors.InsufficientStock);
                }
            }

            // 2. Reduce stock:
            foreach (var item in checkoutItems)
            {
                item.Sku.Quantity -= item.Quantity;
            }

            // 3. Remove item from shopping cart
            _dbContext.ShoppingCartItems.RemoveRange(checkoutItems);

            // 4. Create Order:
            var order = new Order()
            {
                ShippingAddressId = request.ShippingAddressId,
                PaymentMethodId = request.PaymentMethodId,
                DeliveryMethodId = request.DeliveryMethodId,
                ExpectedDeliveryWhen = request.ExpectedDeliveryWhen,
                Status = OrderStatus.Pending,
                CustomerId = request.UserId,
                Comment = request.Note,
                // handle later
                DiscountVoucherId = request.DiscountVoucherId,
            };

            foreach (var item in checkoutItems)
            {
                order.OrderLines.Add(new OrderLine()
                {
                    SkuId = item.SkuId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Sku.UnitPrice,
                    //DiscountVoucherId = something
                });
            }

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);


            // 5. Handle Payment (VNPay):
            var paymentMethod = await _dbContext.PaymentMethods.FindAsync(request.PaymentMethodId, cancellationToken);
            var paymentUrl = string.Empty;
            if (paymentMethod?.Type == PaymentMethodType.VnPay)
            {
                if (string.IsNullOrEmpty(request.PaymentReturnUrl))
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure<PlaceOrderResponse>(OrderErrors.MissingReturnUrl);
                }

                var createPaymentRequest = new PaymentInformationDto()
                {
                    OrderId = order.Id,
                    OrderNumber = order.OrderNumber,
                    Amount = order.Subtotal,
                    ReturnUrl = request.PaymentReturnUrl
                };
                paymentUrl = _paymentService.CreatePaymentUrl(createPaymentRequest, request.IpAddress);
            }

            var result = new PlaceOrderResponse()
            {
                OrderId = order.Id,
                PaymentUrl = paymentUrl
            };

            await transaction.CommitAsync(cancellationToken);
            return Result.Success(result);
        }
        catch(Exception ex)
        {
            // todo: log exception


            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure<PlaceOrderResponse>(OrderErrors.OrderCreationFailed);
        }
    }
}