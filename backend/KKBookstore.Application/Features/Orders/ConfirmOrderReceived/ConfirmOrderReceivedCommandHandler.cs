using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Orders.ConfirmOrderReceived;

public class ConfirmOrderReceivedCommandHandler : IRequestHandler<ConfirmOrderReceivedCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<ConfirmOrderReceivedCommandHandler> _logger;
    private readonly ICustomerService _customerService;

    public ConfirmOrderReceivedCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<ConfirmOrderReceivedCommandHandler> logger,
        ICustomerService customerService)
    {
        _dbContext = dbContext;
        _logger = logger;
        _customerService = customerService;
    }

    public async Task<Result> Handle(ConfirmOrderReceivedCommand request, CancellationToken cancellationToken)
    {        try
        {
            var order = await _dbContext.Orders
                .Include(o => o.PaymentMethod)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.CustomerId == request.CustomerId,
                    cancellationToken);

            if (order == null)
            {
                return Result.Failure(OrderErrors.NotFound);
            }

            // Check if order can be confirmed as received
            if (order.Status != OrderStatus.Delivered)
            {
                return Result.Failure(Error.Validation("Order.InvalidStatus",
                    "Order must be delivered before it can be confirmed as received"));
            }

            // Update order status and timestamp
            var previousStatus = order.Status;
            order.Status = OrderStatus.Received;
            order.ConfirmedReceivedWhen = DateTimeOffset.Now;

            // Update customer spending for COD orders (payment happens on delivery)
            if (order.PaymentMethod?.Type == PaymentMethodType.CashOnDelivery)
            {
                var orderTotal = order.CalculateTotal();
                var customerUpdateResult = await _customerService.UpdateCustomerSpentAmountAsync(
                    order.CustomerId, orderTotal, cancellationToken);
                
                if (customerUpdateResult.IsFailure)
                {
                    _logger.LogWarning("Failed to update customer {CustomerId} spending for COD order {OrderId}. Error: {Error}",
                        order.CustomerId, order.Id, customerUpdateResult.Error);
                    // Continue with order confirmation even if customer update fails
                    // The customer update can be retried later if needed
                }
                else
                {
                    _logger.LogInformation("Updated customer {CustomerId} spending for COD order {OrderId} with amount {Amount}",
                        order.CustomerId, order.Id, orderTotal);
                }
            }

            // Record order history
            var orderHistory = OrderHistory.Create(
                orderId: order.Id,
                fromStatus: previousStatus,
                toStatus: OrderStatus.Received,
                action: "Customer confirmed order received",
                notes: null,
                triggeredByUserId: request.CustomerId
            );

            if (orderHistory.IsSuccess)
            {
                await _dbContext.OrderHistories.AddAsync(orderHistory.Value, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Customer {CustomerId} confirmed receipt of order {OrderId}",
                request.CustomerId, request.OrderId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error confirming order {OrderId} as received by customer {CustomerId}",
                request.OrderId, request.CustomerId);
            return Result.Failure(Error.Failure("ConfirmOrderReceived.Failed",
                "Failed to confirm order as received"));
        }
    }
}
