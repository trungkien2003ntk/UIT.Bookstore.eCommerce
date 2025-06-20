using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Constants;
using KKBookstore.Emailing;
using KKBookstore.Emailing.TemplateModels;
using KKBookstore.Models;
using KKBookstore.Orders;
using KKBookstore.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KKBookstore.Features.Checkout.PlaceOrder;

public class DefaultOrderProcessor(
    IApplicationDbContext dbContext,
    IPaymentService paymentService,
    IEmailSender emailSender,
    IEmailService emailService,
    IBranchSelectionService branchSelectionService,
    IIdentityService identityService,
    ILogger<DefaultOrderProcessor> logger
) : OrderProcessor(dbContext, paymentService, emailSender)
{
    private readonly IEmailService _emailTemplateService = emailService;
    private readonly IBranchSelectionService _branchSelectionService = branchSelectionService;
    private readonly IIdentityService identityService = identityService;
    private readonly ILogger<DefaultOrderProcessor> logger = logger;
    protected override async Task<List<ShoppingCartItem>> GetCheckoutItems(PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        return await _dbContext.ShoppingCartItems
            .Where(i => request.ItemIds.Contains(i.Id))
            .Include(sci => sci.ProductVariant)
                .ThenInclude(s => s.Product)
                    .ThenInclude(p => p.ProductImages)
            .Include(sci => sci.ProductVariant)
                .ThenInclude(s => s.ProductVariantOptionValues)
                    .ThenInclude(sov => sov.OptionValue)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    protected override async Task<bool> CheckInventory(List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken)
    {
        foreach (var item in checkoutItems)
        {
            if (item.ProductVariant.StockQuantity < item.Quantity)
            {
                return false;
            }
        }
        return true;
    }    protected override async Task ReduceStock(List<ShoppingCartItem> checkoutItems, List<OrderFulfillment> orderFulfillments, CancellationToken cancellationToken)
    {
        // Reduce stock based on allocated inventory from specific branches
        foreach (var fulfillment in orderFulfillments)
        {
            foreach (var allocation in fulfillment.OrderLineAllocations)
            {
                var inventory = await _dbContext.Inventories.FindAsync([allocation.InventoryId], cancellationToken);
                if (inventory != null)
                {
                    inventory.StockQuantity -= allocation.Quantity;
                    if (inventory.StockQuantity <= 0)
                    {
                        inventory.Deactivate();
                    }
                }
            }
        }
    }    protected override Task RemoveFromCart(List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken)
    {
        _dbContext.ShoppingCartItems.RemoveRange(checkoutItems);
        return Task.CompletedTask;
    }

    protected override Task<Order> CreateOrder(PlaceOrderCommand request, List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken)
    {
        var order = new Order()
        {
            ShippingAddressId = request.ShippingAddressId,
            PaymentMethodId = request.PaymentMethodId,
            DeliveryMethodId = request.DeliveryMethodId,
            ExpectedDeliveryWhen = request.ExpectedDeliveryWhen,
            Status = OrderStatus.Pending,
            CustomerId = request.UserId,
            Comment = request.Note,
            ShippingFee = request.ShippingFee,
            DeliveryInstruction = "",
            TaxRate = 0m,
        };

        foreach (var item in checkoutItems)
        {
            order.OrderLines.Add(new OrderLine()
            {
                ProductVariantId = item.ProductVariantId,
                Quantity = item.Quantity,
                UnitPrice = item.ProductVariant.UnitPrice,
                ProductVariant = item.ProductVariant
            });
        }

        return Task.FromResult(order);
    }

    protected override async Task<bool> ApplyDiscountVouchers(Order order, PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;

        var shippingDiscountVoucher = await _dbContext.DiscountVouchers
            .Where(dv => dv.Id == request.ShippingVoucherId)
            .Include(dv => dv.VoucherUsages)
            .FirstOrDefaultAsync(cancellationToken);

        if (shippingDiscountVoucher != null)
        {
            var applyVoucherResult = order.ApplyVoucher(shippingDiscountVoucher);
            if (applyVoucherResult.IsFailure)
            {
                return false;
            }

            await _dbContext.VoucherUsages.AddAsync(new VoucherUsage()
            {
                VoucherId = shippingDiscountVoucher.Id,
                OrderId = order.Id,
                CustomerId = userId,
                RedemptionTime = DateTimeOffset.Now
            }, cancellationToken);
        }

        var orderDiscountVoucher = await _dbContext.DiscountVouchers
            .Where(dv => dv.Id == request.OrderDiscountVoucherId)
            .Include(dv => dv.VoucherUsages)
            .FirstOrDefaultAsync(cancellationToken);

        if (orderDiscountVoucher != null)
        {
            var applyVoucherResult = order.ApplyVoucher(orderDiscountVoucher);
            if (applyVoucherResult.IsFailure)
            {
                return false;
            }

            await _dbContext.VoucherUsages.AddAsync(new VoucherUsage()
            {
                VoucherId = orderDiscountVoucher.Id,
                OrderId = order.Id,
                CustomerId = userId,
                RedemptionTime = DateTimeOffset.Now
            }, cancellationToken);
        }

        return true;
    }

    protected override async Task<Result<string>> HandlePayment(PlaceOrderCommand request, Order order, CancellationToken cancellationToken)
    {
        var paymentMethod = await _dbContext.PaymentMethods.FindAsync(new object[] { request.PaymentMethodId }, cancellationToken);
        var paymentUrl = string.Empty;
        if (paymentMethod?.Type == PaymentMethodType.VnPay)
        {
            if (string.IsNullOrEmpty(request.PaymentReturnUrl))
            {
                return Result.Failure<string>(OrderErrors.MissingReturnUrl);
            }

            var createPaymentRequest = new PaymentInformationDto()
            {
                OrderId = order.Id,
                OrderNumber = order.OrderNumber,
                Amount = order.CalculateTotal(),
                ReturnUrl = request.PaymentReturnUrl
            };
            paymentUrl = _paymentService.CreatePaymentUrl(createPaymentRequest, request.IpAddress);
        }
        return paymentUrl;
    }
    protected override async Task SendOrderConfirmation(int userId, Order order, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FindAsync([userId], cancellationToken);
        if (user?.Email == null) return;        // Get order details with related data
        var orderWithDetails = await _dbContext.Orders
            .Where(o => o.Id == order.Id)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(pv => pv.Product)
            .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.ProductVariant)
                    .ThenInclude(pv => pv.ProductVariantOptionValues!)
                        .ThenInclude(pvov => pvov.OptionValue)
            .Include(o => o.ShippingAddress)
            .Include(o => o.PaymentMethod)
            .Include(o => o.DeliveryMethod)
            .Include(o => o.PriceDiscountVoucher)
            .Include(o => o.ShippingDiscountVoucher)
            .FirstOrDefaultAsync(cancellationToken);

        if (orderWithDetails == null) return;

        // Create order line items for email
        var orderItems = orderWithDetails.OrderLines.Select(ol => new OrderLineItem
        {
            ProductName = ol.ProductVariant.Product.Name,
            VariantName = ol.ProductVariant.VariantName,
            Quantity = ol.Quantity,
            UnitPrice = ol.UnitPrice,
            ThumbnailUrl = ol.ProductVariant.GetThumbnailImageUrl()
        }).ToList();

        // Calculate discount amount
        decimal? discountAmount = null;
        if (orderWithDetails.PriceDiscountVoucher != null)
        {
            discountAmount = orderWithDetails.PriceDiscountVoucher.GetDiscountValue(orderWithDetails.Subtotal);
        }
        if (orderWithDetails.ShippingDiscountVoucher != null)
        {
            var shippingDiscount = orderWithDetails.ShippingDiscountVoucher.GetDiscountValue(orderWithDetails.ShippingFee);
            discountAmount = (discountAmount ?? 0) + shippingDiscount;
        }

        // Build shipping address string
        var shippingAddress = $"{orderWithDetails.ShippingAddress?.DetailAddress}, " +
                             $"{orderWithDetails.ShippingAddress?.CommuneName}, " +
                             $"{orderWithDetails.ShippingAddress?.DistrictName}, " +
                             $"{orderWithDetails.ShippingAddress?.ProvinceName}";

        // Create email model
        var emailModel = new OrderConfirmationEmailModel(
            orderId: orderWithDetails.Id,
            orderNumber: orderWithDetails.OrderNumber,
            totalAmount: orderWithDetails.CalculateTotal(),
            orderDate: orderWithDetails.OrderWhen,
            expectedDeliveryDate: orderWithDetails.ExpectedDeliveryWhen,
            orderItems: orderItems,
            shippingFee: orderWithDetails.ShippingFee,
            shippingAddress: shippingAddress,
            paymentMethod: orderWithDetails.PaymentMethod?.Name ?? "N/A",
            deliveryMethod: orderWithDetails.DeliveryMethod?.Name ?? "N/A",
            receiverFullName: user.FullName,
            note: orderWithDetails.Comment,
            discountAmount: discountAmount
        );

        // Send email using template service
        await _emailTemplateService.SendAsync(
            user.Email,
            emailModel.Subject,
            emailModel
        );
    }

    // NEW: Intelligent branch selection methods implementation
    protected override async Task<Result<List<OrderFulfillment>>> AllocateInventoryFromNearestBranches(Order order, PlaceOrderCommand request, CancellationToken cancellationToken)
    {
        // Get customer shipping address
        var shippingAddress = await _dbContext.ShippingAddresses
            .FirstOrDefaultAsync(sa => sa.Id == request.ShippingAddressId, cancellationToken);

        if (shippingAddress == null)
        {
            return Result.Failure<List<OrderFulfillment>>(
                Error.NotFound("ShippingAddress.NotFound", "Shipping address not found"));
        }

        // Use the branch selection service to allocate inventory
        var allocationResult = await _branchSelectionService.AllocateInventoryFromNearestBranchesAsync(
            order, shippingAddress, cancellationToken);

        if (allocationResult.IsSuccess)
        {
            // Save the order fulfillments to database
            foreach (var fulfillment in allocationResult.Value)
            {
                _dbContext.OrderFulfillments.Add(fulfillment);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return allocationResult;
    }

    protected override bool RequiresAdminConfirmation(List<OrderFulfillment> orderFulfillments)
    {
        return _branchSelectionService.RequiresAdminConfirmation(orderFulfillments);
    }    protected override async Task NotifyAdminForBranchSelection(Order order, List<OrderFulfillment> orderFulfillments, CancellationToken cancellationToken)
    {
        // Create email model for admin notification
        var branchOptions = orderFulfillments.Select(of => new BranchSelectionOption
        {
            BranchId = of.BranchId,
            BranchName = of.Branch?.Name ?? $"Branch {of.BranchId}",
            DistanceKm = of.DistanceFromCustomer,
            TotalItems = of.GetTotalAllocatedItems(),
            TotalValue = of.GetTotalAllocatedValue()        }).ToList();

        // Load customer information
        var customer = await dbContext.Customers
            .FirstOrDefaultAsync(c => c.Id == order.CustomerId, cancellationToken);

        var customerName = customer?.FullName ?? customer?.UserName ?? "Unknown Customer";

        var emailModel = new AdminBranchSelectionEmailModel(
            orderId: order.Id,
            orderNumber: order.OrderNumber,
            customerName: customerName,
            orderDate: order.OrderWhen.DateTime,
            branchOptions: branchOptions,
            totalOrderValue: order.CalculateTotal()
        );

        // Get admin users to notify
        var adminUsers = await identityService.GetUsersInRoleAsync(AppRoles.Admin);
        if (adminUsers.IsFailure || adminUsers.Value.Count == 0)
        {
            logger.LogWarning("No admin users found to notify for branch selection for order {OrderId}", order.Id);
            return;
        }

        var adminEmails = adminUsers.Value
            .Where(u => !string.IsNullOrEmpty(u.Email))
            .Select(u => u.Email!)
            .ToList();

        if (adminEmails.Count == 0)
        {
            logger.LogWarning("No admin emails configured for branch selection notifications");
            return;
        }

        // Send email to all admins
        foreach (var adminEmail in adminEmails)
        {
            try
            {
                await _emailTemplateService.SendAsync(adminEmail, emailModel.Subject, emailModel);
                logger.LogInformation("Branch selection notification sent to admin {AdminEmail} for order {OrderId}",
                    adminEmail, order.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send branch selection notification to admin {AdminEmail} for order {OrderId}",
                    adminEmail, order.Id);
            }
        }
    }
}