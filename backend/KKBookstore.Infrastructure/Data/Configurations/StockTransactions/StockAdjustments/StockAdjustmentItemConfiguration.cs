using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Data.Configurations.StockTransactions.StockAdjustments;

public class StockAdjustmentItemConfiguration : IEntityTypeConfiguration<StockAdjustmentItem>
{
    public void Configure(EntityTypeBuilder<StockAdjustmentItem> builder)
    {
        builder.HasBaseType<StockTransactionDetail>();

        builder.Property(x => x.AdjustmentType).IsRequired().HasConversion<EnumToStringConverter<AdjustmentType>>();
    }
}
