using KKBookstore.Common.Interfaces;
using KKBookstore.Features.DiscountVouchers.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.DiscountVouchers.UpdateDiscountVoucher;

public record UpdateDiscountVoucherCommand : IRequest<Result<DiscountVoucherDto>>
{
    public int Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public DiscountValueType ValueType { get; init; }
    public DiscountVoucherType VoucherType { get; init; }
    public decimal Value { get; init; }
    public decimal? MaximumDiscountValue { get; init; }
    public decimal MinimumSpend { get; init; }
    public int? UsageLimitPerUser { get; init; }
    public int UsageLimitOverall { get; init; }
    public DateTimeOffset StartTime { get; init; }
    public DateTimeOffset EndTime { get; init; }
    public List<int> ApplyToProductTypeIdsList { get; init; } = []; // New field for multiple product type IDs
    public List<int> CustomerTypeIds { get; init; } = [];
}

public class UpdateDiscountVoucherCommandHandler(
    IApplicationDbContext dbContext,
    IProductTypeHierarchyService productTypeHierarchyService
) : IRequestHandler<UpdateDiscountVoucherCommand, Result<DiscountVoucherDto>>
{
    public async Task<Result<DiscountVoucherDto>> Handle(UpdateDiscountVoucherCommand request, CancellationToken cancellationToken)
    {
        var discountVoucher = await dbContext.DiscountVouchers
            .Include(dv => dv.VoucherUsages)
            .Include(dv => dv.CustomerTypes)
            .FirstOrDefaultAsync(dv => dv.Id == request.Id, cancellationToken);

        if (discountVoucher == null)
        {
            return Result.Failure<DiscountVoucherDto>(DiscountVoucherErrors.NotFound);
        }

        // Check if voucher has been used - if so, restrict updates
        var hasBeenUsed = discountVoucher.VoucherUsages.Any();

        // Calculate time until start (15 minutes constraint)
        var timeDifferenceToStart = discountVoucher.StartTime - DateTimeOffset.Now;
        var isWithin15Minutes = timeDifferenceToStart.TotalMinutes <= 15;

        // Apply 15-minute constraint logic
        if (isWithin15Minutes)
        {
            // If within 15 minutes of start, only allow StartTime to be moved further out
            if (request.StartTime <= discountVoucher.StartTime)
            {
                return Result.Failure<DiscountVoucherDto>(
                    Error.BusinessRuleViolation("DiscountVoucher.CanOnlyExtendStartTime",
                        "When within 15 minutes of start time, you can only extend the start time to a later date"));
            }

            // Only update StartTime and EndTime (if EndTime needs to be adjusted)
            if (request.EndTime <= request.StartTime)
            {
                return Result.Failure<DiscountVoucherDto>(
                    Error.Validation("DiscountVoucher.InvalidTimeRange", "End time must be after start time"));
            }

            discountVoucher.StartTime = request.StartTime;
            discountVoucher.EndTime = request.EndTime;
        }
        else
        {
            // Outside 15-minute window - allow all updates except Status

            // Validate start time is at least 15 minutes from now (for new start time)
            var minimumStartTime = DateTimeOffset.Now.AddMinutes(15);
            if (request.StartTime < minimumStartTime)
            {
                return Result.Failure<DiscountVoucherDto>(
                    Error.Validation("DiscountVoucher.StartTimeTooSoon", "Start time must be at least 15 minutes from now"));
            }

            // Validate end time is after start time
            if (request.EndTime <= request.StartTime)
            {
                return Result.Failure<DiscountVoucherDto>(
                    Error.Validation("DiscountVoucher.InvalidTimeRange", "End time must be after start time"));
            }

            // Check if code already exists (excluding current voucher)
            var existingVoucher = await dbContext.DiscountVouchers
                .FirstOrDefaultAsync(dv => dv.Code == request.Code && dv.Id != request.Id, cancellationToken);

            if (existingVoucher != null)
            {
                return Result.Failure<DiscountVoucherDto>(
                    Error.Validation("DiscountVoucher.CodeAlreadyExists", "A voucher with this code already exists"));
            }

            // Validate ProductTypes exist if specified
            var productTypeIdsToValidate = new List<int>();

            // Add multiple IDs from the new field
            productTypeIdsToValidate.AddRange(request.ApplyToProductTypeIdsList);

            if (productTypeIdsToValidate.Any())
            {
                var existingProductTypeIds = await dbContext.ProductTypes
                    .Where(pt => productTypeIdsToValidate.Contains(pt.Id))
                    .Select(pt => pt.Id)
                    .ToListAsync(cancellationToken);

                var missingIds = productTypeIdsToValidate.Except(existingProductTypeIds).ToList();
                if (missingIds.Any())
                {
                    return Result.Failure<DiscountVoucherDto>(
                        Error.NotFound("ProductType.NotFound", $"Product types with IDs [{string.Join(", ", missingIds)}] do not exist"));
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

            // Validate business rules
            if (request.Value < 0)
            {
                return Result.Failure<DiscountVoucherDto>(DiscountVoucherErrors.ValueMustBePositive);
            }

            if (request.ValueType == DiscountValueType.Percentage && request.Value > 1)
            {
                return Result.Failure<DiscountVoucherDto>(DiscountVoucherErrors.InvalidValueRange);
            }

            // Get product type hierarchy IDs if specified
            string? productTypeHierarchyIds = null;
            if (productTypeIdsToValidate.Any())
            {
                productTypeHierarchyIds = await productTypeHierarchyService.GetDescendantProductTypeIdsAsStringAsync(
                    productTypeIdsToValidate,
                    cancellationToken);
            }

            // Update all properties except Status
            discountVoucher.Code = request.Code;
            discountVoucher.Description = request.Description;
            discountVoucher.ValueType = request.ValueType;
            discountVoucher.VoucherType = request.VoucherType;
            discountVoucher.Value = request.Value;
            discountVoucher.MaximumDiscountValue = request.ValueType == DiscountValueType.Fixed ? null : request.MaximumDiscountValue;
            discountVoucher.MinimumSpend = request.MinimumSpend;
            discountVoucher.UsageLimitPerUser = request.UsageLimitPerUser;
            discountVoucher.UsageLimitOverall = request.UsageLimitOverall;
            discountVoucher.StartTime = request.StartTime;
            discountVoucher.EndTime = request.EndTime;
            discountVoucher.ApplyToProductTypeIds = productTypeHierarchyIds;

            // Update name based on new values
            discountVoucher.Name = CreateDiscountName(request.Value, discountVoucher.MaximumDiscountValue, request.ValueType);// Update CustomerType relationships
            // Remove existing relationships
            var existingCustomerTypes = discountVoucher.CustomerTypes.ToList();
            foreach (var existing in existingCustomerTypes)
            {
                dbContext.VoucherCustomerTypes.Remove(existing);
            }

            // Add new relationships
            foreach (var customerTypeId in request.CustomerTypeIds)
            {
                var voucherCustomerType = new VoucherCustomerType
                {
                    VoucherId = discountVoucher.Id,
                    CustomerTypeId = customerTypeId
                };
                dbContext.VoucherCustomerTypes.Add(voucherCustomerType);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);        // Reload the voucher with updated data
        var updatedVoucher = await dbContext.DiscountVouchers
            .Include(dv => dv.VoucherUsages)
            .Include(dv => dv.CustomerTypes)
                .ThenInclude(vct => vct.CustomerType)
            .FirstAsync(dv => dv.Id == discountVoucher.Id, cancellationToken);

        // Get all product type details if ApplyToProductTypeIds is populated
        var allProductTypeDetails = new List<ProductTypeDetail>();
        var productTypeIdsList = new List<int>();

        if (!string.IsNullOrEmpty(updatedVoucher.ApplyToProductTypeIds))
        {
            allProductTypeDetails = await productTypeHierarchyService.GetProductTypeDetailsFromStringAsync(
                updatedVoucher.ApplyToProductTypeIds,
                cancellationToken);

            productTypeIdsList = allProductTypeDetails.Select(pt => pt.Id).ToList();
        }

        var result = new DiscountVoucherDto
        {
            Id = updatedVoucher.Id,
            Name = updatedVoucher.Name,
            Code = updatedVoucher.Code,
            Description = updatedVoucher.Description,
            ValueType = updatedVoucher.ValueType,
            VoucherType = updatedVoucher.VoucherType,
            Status = updatedVoucher.Status,
            Value = updatedVoucher.Value,
            MaximumDiscountValue = updatedVoucher.MaximumDiscountValue,
            MinimumSpend = updatedVoucher.MinimumSpend,
            UsageLimitPerUser = updatedVoucher.UsageLimitPerUser,
            UsageLimitOverall = updatedVoucher.UsageLimitOverall,
            StartTime = updatedVoucher.StartTime,
            EndTime = updatedVoucher.EndTime,

            // Product Type fields - new approach
            ApplyToProductTypeIds = updatedVoucher.ApplyToProductTypeIds,
            ApplyToProductTypeIdsList = productTypeIdsList,
            ApplyToProductTypes = allProductTypeDetails.Select(pt => new ApplyToProductTypeDto
            {
                Id = pt.Id,
                DisplayName = pt.DisplayName
            }).ToList(),

            CustomerTypeIds = updatedVoucher.CustomerTypes.Select(vct => vct.CustomerTypeId).ToList(),
            CustomerTypeNames = updatedVoucher.CustomerTypes.Select(vct => vct.CustomerType.Name).ToList(),
            UsageCount = updatedVoucher.VoucherUsages.Count,
            UsedPercentage = updatedVoucher.UsageLimitOverall == 0 ? 0 : (decimal)updatedVoucher.VoucherUsages.Count / updatedVoucher.UsageLimitOverall,
            CustomerTypes = updatedVoucher.CustomerTypes.Select(vct => new CustomerTypeDto
            {
                Id = vct.CustomerType.Id,
                Name = vct.CustomerType.Name
            }).ToList(),
            CreationTime = updatedVoucher.CreationTime,
            CreatorId = updatedVoucher.CreatorId,
            LastModificationTime = updatedVoucher.LastModificationTime,
            LastModifierId = updatedVoucher.LastModifierId
        };

        return Result.Success(result);
    }

    private static string CreateDiscountName(decimal value, decimal? maximumDiscountValue, DiscountValueType valueType)
    {
        var nameBuilder = new System.Text.StringBuilder("Giảm giá ");

        if (valueType == DiscountValueType.Percentage)
        {
            int percentage = (int)(value * 100);
            nameBuilder.Append($"{percentage}%");
        }
        else
        {
            int fixedValue = (int)(value / 1000);
            nameBuilder.Append($"₫{fixedValue}k");
        }

        if (maximumDiscountValue.HasValue && valueType == DiscountValueType.Percentage)
        {
            int maximumDiscountValueFixed = (int)(maximumDiscountValue.Value / 1000);
            nameBuilder.Append($", tối đa ₫{maximumDiscountValueFixed}k");
        }

        return nameBuilder.ToString();
    }
}
