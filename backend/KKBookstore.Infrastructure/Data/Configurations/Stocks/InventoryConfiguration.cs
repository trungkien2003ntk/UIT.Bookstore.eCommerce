using KKBookstore.StockTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
namespace
    KKBookstore.Data.Configurations.Stocks;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.ProductVariantId).IsRequired();
        builder.Property(e => e.PurchaseOrderLineId).IsRequired(false);
        builder.Property(e => e.WarehouseId).IsRequired(false);
        builder.Property(e => e.StockQuantity).IsRequired();
        builder.Property(e => e.UnitCost).IsRequired().HasPrecision(18, 2);
        builder.Property(e => e.IsActive).IsRequired();
        builder.Property(e => e.SourceType).IsRequired().HasConversion<EnumToStringConverter<InventorySource>>().HasDefaultValue(InventorySource.None);
        builder.HasOne(e => e.ProductVariant).WithMany(pv => pv.Inventories).HasForeignKey(e => e.ProductVariantId);
        builder.HasOne(e => e.Warehouse).WithMany().HasForeignKey(e => e.WarehouseId);
    }
}