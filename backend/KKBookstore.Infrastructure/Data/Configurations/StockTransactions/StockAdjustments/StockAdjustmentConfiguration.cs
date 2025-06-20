using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.StockTransactions.StockAdjustments;

public class StockAdjustmentConfiguration : IEntityTypeConfiguration<StockAdjustment>
{
    public void Configure(EntityTypeBuilder<StockAdjustment> builder)
    {
        builder.HasBaseType<StockTransaction>();

        builder.HasOne(x => x.Warehouse)
            .WithMany()
            .HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.Restrict);
    }
}
