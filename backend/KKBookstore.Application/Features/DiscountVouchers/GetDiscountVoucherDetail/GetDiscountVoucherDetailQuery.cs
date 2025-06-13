using KKBookstore.Common.Interfaces;
using KKBookstore.Features.DiscountVouchers.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.DiscountVouchers.GetDiscountVoucherDetail;

public record GetDiscountVoucherDetailQuery(int Id) : IRequest<Result<DiscountVoucherDto>>;

public class GetDiscountVoucherDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetDiscountVoucherDetailQuery, Result<DiscountVoucherDto>>
{
    public async Task<Result<DiscountVoucherDto>> Handle(GetDiscountVoucherDetailQuery request, CancellationToken cancellationToken)
    {
        var discountVoucher = await dbContext.DiscountVouchers
            .Include(dv => dv.ApplyToProductType)
            .Include(dv => dv.VoucherUsages)
            .Include(dv => dv.CustomerTypes)
                .ThenInclude(vct => vct.CustomerType)
            .FirstOrDefaultAsync(dv => dv.Id == request.Id, cancellationToken); if (discountVoucher == null)
        {
            return Result.Failure<DiscountVoucherDto>(DiscountVoucherErrors.NotFound);
        }

        var result = new DiscountVoucherDto
        {
            Id = discountVoucher.Id,
            Name = discountVoucher.Name,
            Code = discountVoucher.Code,
            Description = discountVoucher.Description,
            ValueType = discountVoucher.ValueType,
            VoucherType = discountVoucher.VoucherType,
            Status = discountVoucher.Status,
            Value = discountVoucher.Value,
            MaximumDiscountValue = discountVoucher.MaximumDiscountValue,
            MinimumSpend = discountVoucher.MinimumSpend,
            UsageLimitPerUser = discountVoucher.UsageLimitPerUser,
            UsageLimitOverall = discountVoucher.UsageLimitOverall,
            StartTime = discountVoucher.StartTime,
            EndTime = discountVoucher.EndTime,
            ApplyToProductTypeId = discountVoucher.ApplyToProductTypeId,
            ApplyToProductTypeName = discountVoucher.ApplyToProductType?.DisplayName,
            CustomerTypeIds = discountVoucher.CustomerTypes.Select(vct => vct.CustomerTypeId).ToList(),
            CustomerTypeNames = discountVoucher.CustomerTypes.Select(vct => vct.CustomerType.Name).ToList(),
            UsageCount = discountVoucher.VoucherUsages.Count,
            UsedPercentage = discountVoucher.UsageLimitOverall == 0 ? 0 : (decimal)discountVoucher.VoucherUsages.Count / discountVoucher.UsageLimitOverall,
            CustomerTypes = discountVoucher.CustomerTypes.Select(vct => new CustomerTypeDto
            {
                Id = vct.CustomerType.Id,
                Name = vct.CustomerType.Name
            }).ToList(),
            ApplyToProductType = discountVoucher.ApplyToProductType != null ? new ApplyToProductTypeDto
            {
                Id = discountVoucher.ApplyToProductType.Id,
                DisplayName = discountVoucher.ApplyToProductType.DisplayName
            } : null,
            CreationTime = discountVoucher.CreationTime,
            CreatorId = discountVoucher.CreatorId,
            LastModificationTime = discountVoucher.LastModificationTime,
            LastModifierId = discountVoucher.LastModifierId
        };

        return Result.Success(result);
    }
}
