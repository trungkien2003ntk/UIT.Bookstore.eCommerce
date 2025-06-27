using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.RequestDtos;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Orders.ConfirmPackagingComplete;

public class ConfirmPackagingCompleteCommandHandler : IRequestHandler<ConfirmPackagingCompleteCommand, Result<ConfirmPackagingCompleteResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IGhnShippingService _ghnShippingService;
    private readonly ILogger<ConfirmPackagingCompleteCommandHandler> _logger;

    public ConfirmPackagingCompleteCommandHandler(
        IApplicationDbContext dbContext,
        IGhnShippingService ghnShippingService,
        ILogger<ConfirmPackagingCompleteCommandHandler> logger)
    {
        _dbContext = dbContext;
        _ghnShippingService = ghnShippingService;
        _logger = logger;
    }

    public async Task<Result<ConfirmPackagingCompleteResponse>> Handle(ConfirmPackagingCompleteCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);
        try
        {
            // start transaction

            // Find the branch that is selected for packaging
            var selectedFulfillment = await _dbContext.OrderFulfillments
                .Where(of => of.OrderId == request.OrderId && of.IsSelectedForPackaging)
                .FirstOrDefaultAsync();

            if (selectedFulfillment == null)
            {
                return Result.Failure<ConfirmPackagingCompleteResponse>(
                    Error.Validation("OrderFulfillment.NotSelected", "No branch selected for packaging"));
            }
            var branchId = selectedFulfillment.BranchId;

            // Get the order with all necessary data
            var order = await _dbContext.Orders
                .Include(o => o.OrderFulfillments.Where(of => of.BranchId == branchId))
                    .ThenInclude(of => of.Branch)
                        .ThenInclude(b => b.Address)
                .Include(o => o.OrderFulfillments.Where(of => of.BranchId == branchId))
                    .ThenInclude(of => of.OrderLineAllocations)
                        .ThenInclude(ola => ola.ProductVariant)
                            .ThenInclude(pv => pv.Product)
                .Include(o => o.ShippingAddress)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

            if (order == null)
            {
                return Result.Failure<ConfirmPackagingCompleteResponse>(OrderErrors.NotFound);
            }

            // Check if order is in packaging status
            if (order.Status != OrderStatus.Packaging)
            {
                return Result.Failure<ConfirmPackagingCompleteResponse>(
                    Error.Validation("Order.InvalidStatus", "Order must be in Packaging status"));
            }

            // Find the fulfillment for the specified branch
            var fulfillment = order.OrderFulfillments.FirstOrDefault();
            if (fulfillment == null || !fulfillment.IsSelectedForPackaging)
            {
                return Result.Failure<ConfirmPackagingCompleteResponse>(
                    Error.Validation("OrderFulfillment.NotSelected", "Branch is not selected for packaging"));
            }

            // Complete packaging
            fulfillment.CompletePackaging();
            fulfillment.Notes = request.Notes;

            //// Create GHN shipping order
            //var ghnOrderRequest = CreateGhnOrderRequest(order, fulfillment);
            //var ghnResult = await _ghnShippingService.CreateOrderAsync(ghnOrderRequest);

            //if (!ghnResult.Success)
            //{
            //    _logger.LogError("Failed to create GHN order for order {OrderId}: {Error}",
            //        order.Id, ghnResult.ErrorMessage);
            //    return Result.Failure<ConfirmPackagingCompleteResponse>(
            //        Error.Failure("GhnOrder.CreationFailed", ghnResult.ErrorMessage ?? "Failed to create shipping order"));
            //}            // Update order status to Processing (waiting for pickup)
            var previousStatus = order.Status;
            order.Status = OrderStatus.Processing;
            //order.Comment = $"GHN Order Code: {ghnResult.OrderCode}";

            // Record order history
            var orderHistory = OrderHistory.Create(
                orderId: order.Id,
                fromStatus: previousStatus,
                toStatus: OrderStatus.Processing,
                action: "Đóng gói hoàn tất, đang chờ lấy hàng",
                //notes: $"GHN Order Code: {ghnResult.OrderCode}. {request.Notes}".Trim(),
                triggeredByUserId: request.AdminUserId
            //externalReference: ghnResult.OrderCode
            );

            if (orderHistory.IsSuccess)
            {
                await _dbContext.OrderHistories.AddAsync(orderHistory.Value, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            //_logger.LogInformation("Completed packaging and created GHN order {GhnOrderCode} for order {OrderId}",
            //    ghnResult.OrderCode, order.Id);

            var response = new ConfirmPackagingCompleteResponse
            {
                GhnOrderCode = string.Empty,
                GhnTrackingUrl = string.Empty,
                ExpectedDeliveryTime = order.ExpectedDeliveryWhen,
                ShippingCost = order.ShippingFee
            };

            await transaction.CommitAsync(cancellationToken);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);

            _logger.LogError(ex, "Error confirming packaging complete for order {OrderId}", request.OrderId);
            return Result.Failure<ConfirmPackagingCompleteResponse>(
                Error.Failure("ConfirmPackagingComplete.Failed", "Failed to confirm packaging complete"));
        }
    }

    private CreateGhnOrderRequest CreateGhnOrderRequest(Order order, OrderFulfillment fulfillment)
    {
        var branch = fulfillment.Branch;
        var shippingAddress = order.ShippingAddress;
        var customer = order.Customer; var items = fulfillment.OrderLineAllocations.Select(ola => new GhnOrderItem
        {
            Name = ola.ProductVariant.Product.Name,
            Code = ola.ProductVariant.SkuValue?.Value ?? "N/A",
            Quantity = ola.Quantity,
            Price = (int)(ola.UnitPrice * 100), // Convert to cents
            Length = 10, // Default dimensions - should be from product data
            Width = 10,
            Height = 5,
            Weight = 200, // Default weight in grams - should be from product data
            Category = new GhnItemCategory
            {
                Level1 = ola.ProductVariant.Product.ProductType?.DisplayName ?? "Books"
            }
        }).ToList();

        return new CreateGhnOrderRequest
        {
            PaymentTypeId = 2, // COD
            Note = order.Comment ?? $"Order {order.OrderNumber}",
            RequiredNote = "CHOXEMHANGKHONGTHU", // Standard note
            ClientOrderCode = order.OrderNumber,
            ToName = $"{customer.FirstName} {customer.LastName}".Trim(),
            ToPhone = customer.PhoneNumber ?? string.Empty,
            ToAddress = $"{shippingAddress.DetailAddress}, {shippingAddress.CommuneName}, {shippingAddress.DistrictName}, {shippingAddress.ProvinceName}",
            ToWardCode = shippingAddress.CommuneCode ?? "00000",
            ToDistrictId = shippingAddress.DistrictId,
            CodAmount = (int)(order.CalculateTotal() * 100), // Convert to cents
            Content = $"Order {order.OrderNumber} - {items.Count} items",
            Weight = items.Sum(i => i.Weight * i.Quantity),
            Length = items.Any() ? items.Max(i => i.Length) ?? 20 : 20,
            Width = items.Any() ? items.Max(i => i.Width) ?? 15 : 15,
            Height = items.Any() ? items.Sum(i => (i.Height ?? 5) * i.Quantity) : 10,
            PickShift = new List<int> { 2 }, // Afternoon pickup
            Items = items
        };
    }
}
