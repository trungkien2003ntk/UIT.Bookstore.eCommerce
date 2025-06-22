using KKBookstore.Application.Common.Interfaces;
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

    public ConfirmOrderReceivedCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<ConfirmOrderReceivedCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result> Handle(ConfirmOrderReceivedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _dbContext.Orders
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
            }            // Update order status and timestamp
            var previousStatus = order.Status;
            order.Status = OrderStatus.Received;
            order.ConfirmedReceivedWhen = DateTimeOffset.Now;

            // Add customer feedback if provided
            var feedbackNotes = string.Empty;
            if (!string.IsNullOrEmpty(request.FeedbackNotes) || request.Rating.HasValue)
            {
                var feedback = "Customer feedback: ";
                if (request.Rating.HasValue)
                {
                    feedback += $"Rating: {request.Rating}/5 stars. ";
                }
                if (!string.IsNullOrEmpty(request.FeedbackNotes))
                {
                    feedback += $"Notes: {request.FeedbackNotes}";
                }

                order.Comment = string.IsNullOrEmpty(order.Comment) 
                    ? feedback 
                    : $"{order.Comment}\n{feedback}";
                
                feedbackNotes = feedback;
            }

            // Record order history
            var orderHistory = OrderHistory.Create(
                orderId: order.Id,
                fromStatus: previousStatus,
                toStatus: OrderStatus.Received,
                action: "Customer confirmed order received",
                notes: feedbackNotes,
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
