using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.Checkout.PlaceOrder;

public class DefaultOrderProcessor(
    IApplicationDbContext dbContext,
    IPaymentService paymentService,
    IEmailService emailService
) : OrderProcessor(dbContext, paymentService, emailService)
{
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
    }

    protected override async Task ReduceStock(List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken)
    {
        foreach (var item in checkoutItems)
        {
            //item.ProductVariant.Quantity -= item.Quantity;
        }
    }

    protected override async Task RemoveFromCart(List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken)
    {
        _dbContext.ShoppingCartItems.RemoveRange(checkoutItems);
    }

    protected override async Task<Order> CreateOrder(PlaceOrderCommand request, List<ShoppingCartItem> checkoutItems, CancellationToken cancellationToken)
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

        return order;
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
        await _emailService.SendOrderConfirmation(user!.Email ?? "trungkien2003ntk@gmail.com", user!.FullName, order);
    }
}