using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Orders.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UpdateOrderStatusCommandHandler> _logger;

    public UpdateOrderStatusCommandHandler(
        IApplicationDbContext dbContext,
        ILogger<UpdateOrderStatusCommandHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

            if (order == null)
            {
                return Result.Failure(OrderErrors.NotFound);
            }

            var previousStatus = order.Status;
            order.Status = request.Status;

            // Update specific timestamps based on status
            switch (request.Status)
            {
                case OrderStatus.Packaging:
                    // Already handled by SelectBranchForPackaging
                    break;
                case OrderStatus.Shipped:
                    // Already handled by ConfirmPackagingComplete
                    break;
                case OrderStatus.Delivered:
                    order.ConfirmedDeliveryWhen = DateTimeOffset.Now;
                    break;
                case OrderStatus.Received:
                    order.ConfirmedReceivedWhen = DateTimeOffset.Now;
                    break;
                case OrderStatus.Cancelled:
                    // Cancel order logic
                    break;
                case OrderStatus.Refunded:
                    // Refund logic
                    break;
            }

            // Add note about manual status update
            var statusUpdateNote = $"Status manually updated from {previousStatus} to {request.Status}";
            if (!string.IsNullOrEmpty(request.Reason))
            {
                statusUpdateNote += $". Reason: {request.Reason}";
            }
            if (!string.IsNullOrEmpty(request.Notes))
            {
                statusUpdateNote += $". Notes: {request.Notes}";
            }
            order.Comment = string.IsNullOrEmpty(order.Comment)
                ? statusUpdateNote
                : $"{order.Comment}\n{statusUpdateNote}";

            // Record order history
            var orderHistory = OrderHistory.Create(
                orderId: order.Id,
                fromStatus: previousStatus,
                toStatus: request.Status,
                action: "Cập nhật trạng thái thủ công bởi quản trị viên",
                notes: statusUpdateNote,
                triggeredByUserId: request.AdminUserId
            );

            if (orderHistory.IsSuccess)
            {
                await _dbContext.OrderHistories.AddAsync(orderHistory.Value, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Manually updated order {OrderId} status from {PreviousStatus} to {NewStatus}. Reason: {Reason}",
                request.OrderId, previousStatus, request.Status, request.Reason);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating order {OrderId} status to {Status}",
                request.OrderId, request.Status);
            return Result.Failure(Error.Failure("UpdateOrderStatus.Failed",
                "Failed to update order status"));
        }
    }
}
