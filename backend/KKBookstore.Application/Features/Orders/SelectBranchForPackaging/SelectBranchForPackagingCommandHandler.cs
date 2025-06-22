using KKBookstore.Common.Interfaces;
using KKBookstore.Emailing;
using KKBookstore.Emailing.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Orders.SelectBranchForPackaging;

public class SelectBranchForPackagingCommandHandler : IRequestHandler<SelectBranchForPackagingCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IEmailService _emailService;
    private readonly ILogger<SelectBranchForPackagingCommandHandler> _logger;

    public SelectBranchForPackagingCommandHandler(
        IApplicationDbContext dbContext,
        IEmailService emailService,
        ILogger<SelectBranchForPackagingCommandHandler> logger)
    {
        _dbContext = dbContext;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Result> Handle(SelectBranchForPackagingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Get the order with fulfillments
            var order = await _dbContext.Orders
                .Include(o => o.OrderFulfillments)
                    .ThenInclude(of => of.Branch)
                .Include(o => o.OrderFulfillments)
                    .ThenInclude(of => of.OrderLineAllocations)
                        .ThenInclude(ola => ola.ProductVariant)
                            .ThenInclude(pv => pv.Product)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

            if (order == null)
            {
                return Result.Failure(OrderErrors.NotFound);
            }

            // Check if order is in correct status
            if (order.Status != OrderStatus.WaitForConfirmPackageBranch)
            {
                return Result.Failure(Error.Validation("Order.InvalidStatus", 
                    "Order must be in WaitForConfirmPackageBranch status to select branch for packaging"));
            }

            // Find the fulfillment for the selected branch
            var fulfillment = order.OrderFulfillments
                .FirstOrDefault(of => of.BranchId == request.BranchId);

            if (fulfillment == null)
            {
                return Result.Failure(Error.Validation("OrderFulfillment.NotFound", 
                    "No inventory allocation found for the selected branch"));
            }

            // Check if branch has sufficient inventory
            if (!fulfillment.OrderLineAllocations.Any())
            {
                return Result.Failure(Error.Validation("OrderFulfillment.NoInventory", 
                    "Selected branch has no allocated inventory for this order"));
            }            // Select the branch for packaging
            fulfillment.SelectForPackaging();
            fulfillment.Notes = request.Notes;

            // Record order history before changing status
            var orderHistory = OrderHistory.Create(
                orderId: order.Id,
                fromStatus: order.Status,
                toStatus: OrderStatus.Packaging,
                action: $"Admin selected branch '{fulfillment.Branch.Name}' for packaging",
                notes: request.Notes,
                triggeredByUserId: request.AdminUserId
            );

            if (orderHistory.IsSuccess)
            {
                await _dbContext.OrderHistories.AddAsync(orderHistory.Value, cancellationToken);
            }

            // Update order status to Packaging
            order.Status = OrderStatus.Packaging;

            await _dbContext.SaveChangesAsync(cancellationToken);

            // Send email notification to branch
            await SendBranchNotificationEmail(order, fulfillment.Branch, cancellationToken);

            _logger.LogInformation("Selected branch {BranchId} for packaging order {OrderId}", 
                request.BranchId, request.OrderId);

            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error selecting branch for packaging order {OrderId}", request.OrderId);
            return Result.Failure(Error.Failure("SelectBranchForPackaging.Failed", 
                "Failed to select branch for packaging"));
        }
    }

    private async Task SendBranchNotificationEmail(Order order, KKBookstore.Branches.Branch branch, CancellationToken cancellationToken)
    {
        try
        {            var emailModel = new BranchPackagingNotificationEmailModel
            {
                BranchName = branch.Name,
                OrderNumber = order.OrderNumber,
                OrderId = order.Id,
                CustomerName = order.Customer?.FirstName + " " + order.Customer?.LastName,
                ItemCount = order.OrderLines.Sum(ol => ol.Quantity),
                OrderTotal = order.CalculateTotal().ToString("C"),
                ExpectedDelivery = order.ExpectedDeliveryWhen.ToString("dd/MM/yyyy HH:mm"),
                ReceiverFullName = branch.Name
            };

            await _emailService.SendAsync(
                to: branch.Email,
                subject: emailModel.Subject,
                emailModel: emailModel
            );

            _logger.LogInformation("Sent packaging notification email to branch {BranchName} for order {OrderNumber}", 
                branch.Name, order.OrderNumber);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send branch notification email for order {OrderNumber}", order.OrderNumber);
            // Don't fail the whole operation if email fails
        }
    }
}

// Email model for branch notification
public class BranchPackagingNotificationEmailModel : IEmailModel
{
    public string BranchName { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int ItemCount { get; set; }
    public string OrderTotal { get; set; } = string.Empty;
    public string ExpectedDelivery { get; set; } = string.Empty;

    // IEmailModel implementation
    public string TemplateName => "BranchPackagingNotification";
    public string Subject => $"New Order Ready for Packaging - {OrderNumber}";
    public string? ReceiverFullName { get; set; }
    public object TemplateDataModel => this;
}
