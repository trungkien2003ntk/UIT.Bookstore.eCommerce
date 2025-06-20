using KKBookstore.Common.Interfaces;
using KKBookstore.Features.DiscountVouchers.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.DiscountVouchers.CreateDiscountVoucher;

public record CreateDiscountVoucherCommand : IRequest<Result<DiscountVoucherDto>>
{
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DiscountValueType ValueType { get; init; }
    public DiscountVoucherType VoucherType { get; init; }
    public DiscountStatus Status { get; init; }
    public decimal Value { get; init; }
    public decimal? MaximumDiscountValue { get; init; }
    public decimal MinimumSpend { get; init; }
    public int? UsageLimitPerUser { get; init; }
    public int UsageLimitOverall { get; init; }
    public DateTimeOffset StartTime { get; init; }
    public DateTimeOffset EndTime { get; init; }
    public int? ApplyToProductTypeId { get; init; }
    public List<int> CustomerTypeIds { get; init; } = [];
}

public class CreateDiscountVoucherCommandHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<CreateDiscountVoucherCommand, Result<DiscountVoucherDto>>
{
    public async Task<Result<DiscountVoucherDto>> Handle(CreateDiscountVoucherCommand request, CancellationToken cancellationToken)
    {
        // Validate start time is at least 15 minutes from now
        //var minimumStartTime = DateTimeOffset.Now.AddMinutes(15);
        //if (request.StartTime < minimumStartTime)
        //{
        //    return Result.Failure<DiscountVoucherDto>(
        //        Error.Validation("DiscountVoucher.StartTimeTooSoon", "Start time must be at least 15 minutes from now"));
        //}

        // Validate end time is after start time
        if (request.EndTime <= request.StartTime)
        {
            return Result.Failure<DiscountVoucherDto>(
                Error.Validation("DiscountVoucher.InvalidTimeRange", "End time must be after start time"));
        }

        // Check if code already exists
        var existingVoucher = await dbContext.DiscountVouchers
            .FirstOrDefaultAsync(dv => dv.Code == request.Code, cancellationToken);

        if (existingVoucher != null)
        {
            return Result.Failure<DiscountVoucherDto>(
                Error.Validation("DiscountVoucher.CodeAlreadyExists", "A voucher with this code already exists"));
        }

        // Validate ProductType exists if specified
        if (request.ApplyToProductTypeId.HasValue)
        {
            var productTypeExists = await dbContext.ProductTypes
                .AnyAsync(pt => pt.Id == request.ApplyToProductTypeId.Value, cancellationToken);

            if (!productTypeExists)
            {
                return Result.Failure<DiscountVoucherDto>(
                    Error.NotFound("ProductType.NotFound", "The specified product type does not exist"));
            }
        }

        // Validate CustomerTypes exist if specified
        if (request.CustomerTypeIds.Any())
        {
            var existingCustomerTypeIds = await dbContext.CustomerTypes
                .Where(ct => request.CustomerTypeIds.Contains(ct.Id))
                .Select(ct => ct.Id)
                .ToListAsync(cancellationToken);

            var missingIds = request.CustomerTypeIds.Except(existingCustomerTypeIds).ToList();
            if (missingIds.Any())
            {
                return Result.Failure<DiscountVoucherDto>(
                    Error.NotFound("CustomerType.NotFound", $"Customer types with IDs {string.Join(", ", missingIds)} do not exist"));
            }
        }

        // Create the discount voucher
        var createResult = DiscountVoucher.Create(
            request.Code,
            request.Description,
            request.ValueType,
            request.VoucherType,
            request.Status,
            request.Value,
            request.MaximumDiscountValue,
            request.MinimumSpend,
            request.UsageLimitPerUser,
            request.UsageLimitOverall,
            request.StartTime,
            request.EndTime,
            request.ApplyToProductTypeId
        );

        if (createResult.IsFailure)
        {
            return Result.Failure<DiscountVoucherDto>(createResult.Error);
        }

        var discountVoucher = createResult.Value; dbContext.DiscountVouchers.Add(discountVoucher);
        await dbContext.SaveChangesAsync(cancellationToken);

        // Add CustomerType relationships
        foreach (var customerTypeId in request.CustomerTypeIds)
        {
            var voucherCustomerType = new VoucherCustomerType
            {
                VoucherId = discountVoucher.Id,
                CustomerTypeId = customerTypeId
            };
            dbContext.VoucherCustomerTypes.Add(voucherCustomerType);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        // Load the created voucher with related data
        var createdVoucher = await dbContext.DiscountVouchers
            .Include(dv => dv.ApplyToProductType)
            .Include(dv => dv.VoucherUsages)
            .Include(dv => dv.CustomerTypes)
                .ThenInclude(vct => vct.CustomerType)
            .FirstAsync(dv => dv.Id == discountVoucher.Id, cancellationToken);

        var result = new DiscountVoucherDto
        {
            Id = createdVoucher.Id,
            Name = createdVoucher.Name,
            Code = createdVoucher.Code,
            Description = createdVoucher.Description,
            ValueType = createdVoucher.ValueType,
            VoucherType = createdVoucher.VoucherType,
            Status = createdVoucher.Status,
            Value = createdVoucher.Value,
            MaximumDiscountValue = createdVoucher.MaximumDiscountValue,
            MinimumSpend = createdVoucher.MinimumSpend,
            UsageLimitPerUser = createdVoucher.UsageLimitPerUser,
            UsageLimitOverall = createdVoucher.UsageLimitOverall,
            StartTime = createdVoucher.StartTime,
            EndTime = createdVoucher.EndTime,
            ApplyToProductTypeId = createdVoucher.ApplyToProductTypeId,
            ApplyToProductTypeName = createdVoucher.ApplyToProductType?.DisplayName,
            CustomerTypeIds = createdVoucher.CustomerTypes.Select(vct => vct.CustomerTypeId).ToList(),
            CustomerTypeNames = createdVoucher.CustomerTypes.Select(vct => vct.CustomerType.Name).ToList(),
            UsageCount = createdVoucher.VoucherUsages.Count,
            UsedPercentage = 0,
            CustomerTypes = createdVoucher.CustomerTypes.Select(vct => new CustomerTypeDto
            {
                Id = vct.CustomerType.Id,
                Name = vct.CustomerType.Name
            }).ToList(),
            ApplyToProductType = createdVoucher.ApplyToProductType != null ? new ApplyToProductTypeDto
            {
                Id = createdVoucher.ApplyToProductType.Id,
                DisplayName = createdVoucher.ApplyToProductType.DisplayName
            } : null,
            CreationTime = createdVoucher.CreationTime,
            CreatorId = createdVoucher.CreatorId,
            LastModificationTime = createdVoucher.LastModificationTime,
            LastModifierId = createdVoucher.LastModifierId
        };

        return Result.Success(result);
    }
}
