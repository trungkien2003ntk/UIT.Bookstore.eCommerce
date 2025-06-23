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
    public int? ApplyToProductTypeId { get; init; } // Legacy field for backward compatibility
    public List<int> ApplyToProductTypeIdsList { get; init; } = []; // New field for multiple product type IDs
    public List<int> CustomerTypeIds { get; init; } = [];
}

public class CreateDiscountVoucherCommandHandler(
    IApplicationDbContext dbContext,
    IProductTypeHierarchyService productTypeHierarchyService
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

        // Validate ProductTypes exist if specified
        var productTypeIdsToValidate = new List<int>();

        // Legacy support: if single ID is provided, add it to the list
        if (request.ApplyToProductTypeId.HasValue)
        {
            productTypeIdsToValidate.Add(request.ApplyToProductTypeId.Value);
        }

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

        // Get product type hierarchy IDs if specified
        string? productTypeHierarchyIds = null;
        if (productTypeIdsToValidate.Any())
        {
            productTypeHierarchyIds = await productTypeHierarchyService.GetDescendantProductTypeIdsAsStringAsync(
                productTypeIdsToValidate,
                cancellationToken);
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
            productTypeHierarchyIds
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

        await dbContext.SaveChangesAsync(cancellationToken);        // Load the created voucher with related data
        var createdVoucher = await dbContext.DiscountVouchers
            .Include(dv => dv.VoucherUsages)
            .Include(dv => dv.CustomerTypes)
                .ThenInclude(vct => vct.CustomerType)
            .FirstAsync(dv => dv.Id == discountVoucher.Id, cancellationToken);

        // Get all product type details if ApplyToProductTypeIds is populated
        var allProductTypeDetails = new List<ProductTypeDetail>();
        var productTypeIdsList = new List<int>();
        if (!string.IsNullOrEmpty(createdVoucher.ApplyToProductTypeIds))
        {
            allProductTypeDetails = await productTypeHierarchyService.GetProductTypeDetailsFromStringAsync(
                createdVoucher.ApplyToProductTypeIds,
                cancellationToken);

            productTypeIdsList = allProductTypeDetails.Select(pt => pt.Id).ToList();
        }

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

            // Product Type fields - new approach
            ApplyToProductTypeIds = createdVoucher.ApplyToProductTypeIds,
            ApplyToProductTypeIdsList = productTypeIdsList,
            ApplyToProductTypes = allProductTypeDetails.Select(pt => new ApplyToProductTypeDto
            {
                Id = pt.Id,
                DisplayName = pt.DisplayName
            }).ToList(),

            CustomerTypeIds = createdVoucher.CustomerTypes.Select(vct => vct.CustomerTypeId).ToList(),
            CustomerTypeNames = createdVoucher.CustomerTypes.Select(vct => vct.CustomerType.Name).ToList(),
            UsageCount = createdVoucher.VoucherUsages.Count,
            UsedPercentage = 0,
            CustomerTypes = createdVoucher.CustomerTypes.Select(vct => new CustomerTypeDto
            {
                Id = vct.CustomerType.Id,
                Name = vct.CustomerType.Name
            }).ToList(),
            CreationTime = createdVoucher.CreationTime,
            CreatorId = createdVoucher.CreatorId,
            LastModificationTime = createdVoucher.LastModificationTime,
            LastModifierId = createdVoucher.LastModifierId
        };

        return Result.Success(result);
    }
}
