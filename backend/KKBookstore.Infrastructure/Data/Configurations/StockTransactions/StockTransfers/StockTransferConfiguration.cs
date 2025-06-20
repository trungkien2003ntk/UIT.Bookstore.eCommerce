using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockTransfers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Data.Configurations.StockTransactions.StockTransfers;

public class StockTransferConfiguration : IEntityTypeConfiguration<StockTransfer>
{
    public void Configure(EntityTypeBuilder<StockTransfer> builder)
    {
        builder.HasBaseType<StockTransaction>();
        builder.Property(x => x.SourceWarehouseId).IsRequired();
        builder.Property(x => x.DestinationWarehouseId).IsRequired();
        builder.Property(x => x.TransferStatus).HasConversion<EnumToStringConverter<StockTransferStatus>>();

        builder.HasOne(x => x.SourceWarehouse)
            .WithMany()
            .HasForeignKey(x => x.SourceWarehouseId).OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DestinationWarehouse)
            .WithMany()
            .HasForeignKey(x => x.DestinationWarehouseId).OnDelete(DeleteBehavior.Restrict);
    }
}
