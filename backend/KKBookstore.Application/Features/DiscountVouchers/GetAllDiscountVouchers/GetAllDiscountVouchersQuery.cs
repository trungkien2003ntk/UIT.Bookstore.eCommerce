using KKBookstore.Common.Interfaces;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.DiscountVouchers.GetAllDiscountVouchers;

public record GetAllDiscountVouchersQuery : IRequest<Result<GetAllDiscountVouchersResponse>>;

public class GetAllDiscountVouchersHandler(
    IApplicationDbContext dbContext,
    ICurrentUser currentUser
) : IRequestHandler<GetAllDiscountVouchersQuery, Result<GetAllDiscountVouchersResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Result<GetAllDiscountVouchersResponse>> Handle(GetAllDiscountVouchersQuery request, CancellationToken cancellationToken)
    {
        // Get customer information if user is logged in
        var currentUserId = _currentUser.Id;
        int? customerTypeId = null;
        
        if (currentUserId.HasValue)
        {
            var customer = await _dbContext.Customers
                .Where(c => c.Id == currentUserId.Value)
                .Select(c => new { c.CustomerTypeId })
                .FirstOrDefaultAsync(cancellationToken);
            
            customerTypeId = customer?.CustomerTypeId;
        }

        // Get all discount vouchers that are active
        var discountVouchers = await _dbContext.DiscountVouchers
            .Where(dv => dv.StartTime <= DateTimeOffset.Now && dv.EndTime >= DateTimeOffset.Now)
            .Include(dv => dv.VoucherUsages)
            .Include(dv => dv.CustomerTypes)
            .ToListAsync(cancellationToken);

        // Filter by customer type if user is logged in
        if (customerTypeId.HasValue)
        {
            discountVouchers = discountVouchers
                .Where(dv => !dv.CustomerTypes.Any() || dv.CustomerTypes.Any(vct => vct.CustomerTypeId == customerTypeId.Value))
                .ToList();
        }

        var allVouchers = discountVouchers
            .Select(dv =>
            {
                var usageLimitOverall = dv.UsageLimitOverall;
                return new GetAllDiscountVouchersResponse.DiscountVoucherDto
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
                    VoucherType = dv.VoucherType.ToString()
                };
            })
            .ToList();

        var response = new GetAllDiscountVouchersResponse
        {
            OrderVouchers = allVouchers.Where(v => v.VoucherType == DiscountVoucherType.Order.ToString()),
            ShippingVouchers = allVouchers.Where(v => v.VoucherType == DiscountVoucherType.Shipping.ToString())
        };

        return response;
    }
}
