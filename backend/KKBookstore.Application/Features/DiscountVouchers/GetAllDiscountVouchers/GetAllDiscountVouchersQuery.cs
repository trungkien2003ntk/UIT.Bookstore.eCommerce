using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Application.Features.DiscountVouchers.GetAllDiscountVouchers;

public record GetAllDiscountVouchersQuery : IRequest<Result<GetAllDiscountVouchersResponse>>;

public class GetAllDiscountVouchersHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetAllDiscountVouchersQuery, Result<GetAllDiscountVouchersResponse>>
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Result<GetAllDiscountVouchersResponse>> Handle(GetAllDiscountVouchersQuery request, CancellationToken cancellationToken)
    {
        // Get all discount vouchers that are active
        var discountVouchers = await _dbContext.DiscountVouchers
            .Where(dv => dv.StartWhen <= DateTimeOffset.Now && dv.EndWhen >= DateTimeOffset.Now)
            .Include(dv => dv.VoucherUsages)
            .ToListAsync(cancellationToken);

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
                    StartDate = dv.StartWhen,
                    EndDate = dv.EndWhen,
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
