using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Branches.Models;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Models;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KKBookstore.Features.StockAdjustments.GetStockAdjustmentDetail;

public record GetStockAdjustmentDetailQuery(int Id) : IRequest<Result<StockAdjustmentDetail>>;

public class GetStockAdjustmentDetailQueryHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetStockAdjustmentDetailQuery, Result<StockAdjustmentDetail>>
{
    public async Task<Result<StockAdjustmentDetail>> Handle(GetStockAdjustmentDetailQuery request, CancellationToken cancellationToken)
    {
        var stockAdjustment = await dbContext.StockAdjustments
            .AsNoTracking()
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.Product)
                        .ThenInclude(p => p.ProductImages)
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.ProductVariantOptionValues)!
                        .ThenInclude(vov => vov.OptionValue)
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.ProductVariantOptionValues)!
                        .ThenInclude(vov => vov.Option)
            .Include(sa => sa.Items)!
                .ThenInclude(i => i.Variant!)
                    .ThenInclude(v => v.Inventories)
            .Include(sa => sa.Warehouse!)
                .ThenInclude(w => w.Address)
            .FirstOrDefaultAsync(sa => sa.Id == request.Id, cancellationToken);

        if (stockAdjustment is null)
        {
            return Result.Failure<StockAdjustmentDetail>(
                Error.NotFound("StockAdjustment.NotFound", $"Stock adjustment with ID {request.Id} was not found."));
        }

        var stockAdjustmentDetail = new StockAdjustmentDetail
        {
            Id = stockAdjustment.Id,
            Code = stockAdjustment.Code,
            Remarks = stockAdjustment.Remarks,
            Reason = stockAdjustment.Reason,
            TransactionStatus = stockAdjustment.TransactionStatus,
            TransactionDate = stockAdjustment.TransactionDate,
            WarehouseId = stockAdjustment.WarehouseId,
            IsDeleted = stockAdjustment.IsDeleted,
            Items = stockAdjustment.Items?.Select(item => new StockAdjustmentItemDetail
            {
                Id = item.Id,
                VariantId = item.VariantId,
                TotalQuantityBefore = item.TotalQuantityBefore,
                Quantity = item.Quantity,
                UnitCost = item.UnitCost,
                AdjustmentType = item.AdjustmentType,
                Remarks = item.Remarks,
                Reason = item.Reason,
                ThumbnailImageUrl = item.Variant!.Product.GetFirstThumbnailImageUrl() ?? string.Empty,
                VariantName = MappingHelpers.GetProductVariantOptionValuesString(item.Variant),
                OptionValues = item.Variant.ProductVariantOptionValues?.Select(pov => new ProductVariantOptionDto
                {
                    ProductOptionId = pov.OptionId,
                    ProductOptionValueId = pov.OptionValueId,
                    Name = pov.Option?.Name ?? string.Empty,
                    Value = pov.OptionValue?.Value ?? string.Empty
                }).ToList() ?? [],
                LastestUnitCost = item.Variant.LastestUnitCost,
                ProductName = item.Variant.Product.Name
            }).ToList() ?? [],
            Warehouse = new BranchDetail
            {
                Id = stockAdjustment.Warehouse!.Id,
                Name = stockAdjustment.Warehouse.Name,
                Description = stockAdjustment.Warehouse.Description,
                Email = stockAdjustment.Warehouse.Email,
                IsDefault = stockAdjustment.Warehouse.IsDefault,
                IsDeleted = stockAdjustment.Warehouse.IsDeleted,
                Address = new AddressDetail
                {
                    Id = stockAdjustment.Warehouse.Address.Id,
                    PhoneNumber = stockAdjustment.Warehouse.Address.PhoneNumber,
                    ProvinceId = stockAdjustment.Warehouse.Address.ProvinceId,
                    ProvinceName = stockAdjustment.Warehouse.Address.ProvinceName,
                    DistrictId = stockAdjustment.Warehouse.Address.DistrictId,
                    DistrictName = stockAdjustment.Warehouse.Address.DistrictName,
                    CommuneCode = stockAdjustment.Warehouse.Address.CommuneCode,
                    CommuneName = stockAdjustment.Warehouse.Address.CommuneName,
                    DetailAddress = stockAdjustment.Warehouse.Address.DetailAddress,
                    AddressType = stockAdjustment.Warehouse.Address.Type,
                    FormattedAddress = $"{stockAdjustment.Warehouse.Address.DetailAddress}, {stockAdjustment.Warehouse.Address.CommuneName},  {stockAdjustment.Warehouse.Address.DistrictName}, {stockAdjustment.Warehouse.Address.ProvinceName}"
                },
                CreationTime = stockAdjustment.Warehouse.CreationTime,
                CreatorId = stockAdjustment.Warehouse.CreatorId,
                LastModificationTime = stockAdjustment.Warehouse.LastModificationTime,
                LastModifierId = stockAdjustment.Warehouse.LastModifierId
            }
        };

        return Result.Success(stockAdjustmentDetail);
    }
}

public class StockAdjustmentDetail
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public string? Reason { get; set; }
    public StockTransactionStatus? TransactionStatus { get; set; }
    public DateTimeOffset TransactionDate { get; set; }
    public int WarehouseId { get; set; }
    public bool IsDeleted { get; set; }
    public List<StockAdjustmentItemDetail> Items { get; set; } = new();
    public DateTimeOffset? CreationTime { get; set; }
    public int? CreatorId { get; set; }
    public DateTimeOffset? LastModificationTime { get; set; }
    public int? LastModifierId { get; set; }

    public BranchDetail? Warehouse { get; set; }
}

public class StockAdjustmentItemDetail
{
    public int Id { get; set; }
    public int VariantId { get; set; }
    public int? TotalQuantityBefore { get; set; }
    public int Quantity { get; set; }
    public decimal UnitCost { get; set; }
    public string? Reason { get; set; }
    public string? Remarks { get; set; }
    public AdjustmentType AdjustmentType { get; set; }
    public decimal TotalCost => UnitCost * Quantity;

    public string ThumbnailImageUrl { get; set; } = string.Empty;
    public string VariantName { get; set; } = string.Empty;
    public List<ProductVariantOptionDto> OptionValues { get; set; } = [];
    public string ProductName { get; set; } = string.Empty;
    public decimal LastestUnitCost { get; set; } = 0;
}

public class ProductVariantOptionDto
{
    public int ProductOptionId { get; set; }
    public int ProductOptionValueId { get; set; }
    public string Name { get; set; }
    public string Value { get; set; }
}
