using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.DiscountVouchers.GetAllDiscountVouchersForCart;

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

        var shoppingCartItems = await _dbContext.ShoppingCartItems
            .Where(sci => sci.CustomerId == userId && selectedItemIds.Contains(sci.Id))
            .Include(sci => sci.ProductVariant)
            .ToListAsync(cancellationToken);

        var createShoppingCartResult = ShoppingCart.Create(userId, shoppingCartItems);
        if (createShoppingCartResult.IsFailure)
        {
            return Result.Failure<GetAllDiscountVouchersForCartResponse>(createShoppingCartResult.Error);
        }
        var shoppingCart = createShoppingCartResult.Value;
        shoppingCart.SelectItems(selectedItemIds);
        var totalAmount = shoppingCart.TotalUnitPrice;


        // Get all discount vouchers that are active
        var discountVouchers = await _dbContext.DiscountVouchers
            .Where(dv => dv.StartWhen <= DateTimeOffset.Now && dv.EndWhen >= DateTimeOffset.Now)
            .Include(dv => dv.VoucherUsages)
            .ToListAsync(cancellationToken);

        // Filter out vouchers that are not applicable to the current shopping cart
        foreach (var discountVoucher in from discountVoucher in discountVouchers
                                        where discountVoucher.IsApplicable(totalAmount, userId)
                                        select discountVoucher)
        {
            discountVoucher.IsRedeemable = true;
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
                    StartDate = dv.StartWhen,
                    EndDate = dv.EndWhen,
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
