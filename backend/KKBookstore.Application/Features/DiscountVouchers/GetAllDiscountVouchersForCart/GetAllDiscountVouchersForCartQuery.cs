using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Orders;
using KKBookstore.ShoppingCarts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.DiscountVouchers.GetAllDiscountVouchersForCart;

public record GetAllDiscountVouchersForCartQuery : IRequest<Result<GetAllDiscountVouchersForCartResponse>>
{
    public int UserId { get; init; }
    public List<int> SelectedItemIds { get; init; } = [];
}

public class GetAllDiscountVouchersForCartHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetAllDiscountVouchersForCartQuery, Result<GetAllDiscountVouchersForCartResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetAllDiscountVouchersForCartResponse>> Handle(GetAllDiscountVouchersForCartQuery request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var selectedItemIds = request.SelectedItemIds;

        // Get customer to check customer type
        var customer = await _dbContext.Customers
            .Include(c => c.CustomerType)
            .FirstOrDefaultAsync(c => c.Id == userId, cancellationToken);

        if (customer == null)
        {
            return Result.Failure<GetAllDiscountVouchersForCartResponse>(Error.NotFound("Customer.NotFound", "Customer not found"));
        }

        var shoppingCartItems = await _dbContext.ShoppingCartItems
            .Where(sci => sci.CustomerId == userId && selectedItemIds.Contains(sci.Id))
            .Include(sci => sci.ProductVariant)
                .ThenInclude(pv => pv.Product)
            .ToListAsync(cancellationToken);

        var createShoppingCartResult = ShoppingCart.Create(userId, shoppingCartItems);
        if (createShoppingCartResult.IsFailure)
        {
            return Result.Failure<GetAllDiscountVouchersForCartResponse>(createShoppingCartResult.Error);
        }
        var shoppingCart = createShoppingCartResult.Value;
        shoppingCart.SelectItems(selectedItemIds);
        var totalAmount = shoppingCart.TotalUnitPrice;

        var distinctProductTypeIds = shoppingCart.Items
            .Select(item => item.ProductVariant?.Product.ProductTypeId ?? 0)
            .Distinct()
            .ToList();


        // Get all discount vouchers that are active
        var discountVouchers = await _dbContext.DiscountVouchers
            .Where(dv => dv.StartTime <= DateTimeOffset.Now && dv.EndTime >= DateTimeOffset.Now)
            .Include(dv => dv.VoucherUsages)
            .Include(dv => dv.CustomerTypes)
            .ToListAsync(cancellationToken);

        // Set redeemable status for all vouchers
        foreach (var discountVoucher in discountVouchers)
        {
            // Check if voucher is applicable to current shopping cart
            bool isApplicable = discountVoucher.IsApplicable(totalAmount, userId, distinctProductTypeIds);
            
            // Check if customer type is allowed for this voucher
            bool isCustomerTypeAllowed = !discountVoucher.CustomerTypes.Any() || 
                                       discountVoucher.CustomerTypes.Any(vct => vct.CustomerTypeId == customer.CustomerTypeId);
            
            // Voucher is redeemable only if both conditions are met
            discountVoucher.IsRedeemable = isApplicable && isCustomerTypeAllowed;
        }

        var allVouchers = discountVouchers
            .Select(dv =>
            {
                var usageLimitOverall = dv.UsageLimitOverall;
                return new GetAllDiscountVouchersForCartResponse.DiscountVoucherDto
                {
                    Id = dv.Id,
                    Name = dv.Name,
                    Code = dv.Code,
                    ValueType = dv.ValueType,
                    Value = dv.Value,
                    MaximumDiscountValue = dv.MaximumDiscountValue,
                    MinimumSpend = dv.MinimumSpend,
                    StartDate = dv.StartTime,
                    EndDate = dv.EndTime,
                    UsageLimitOverall = usageLimitOverall,
                    UsageLimitPerUser = dv.UsageLimitPerUser,
                    UsageCount = dv.VoucherUsages.Count,
                    UsedPercentage = usageLimitOverall == 0 ? 0 : (decimal)dv.VoucherUsages.Count / usageLimitOverall,
                    IsRedeemable = dv.IsRedeemable,
                    VoucherType = dv.VoucherType.ToString()
                };
            })
            .ToList();

        var response = new GetAllDiscountVouchersForCartResponse
        {
            OrderVouchers = allVouchers.Where(v => v.VoucherType == DiscountVoucherType.Order.ToString()),
            ShippingVouchers = allVouchers.Where(v => v.VoucherType == DiscountVoucherType.Shipping.ToString())
        };

        return response;
    }
}
