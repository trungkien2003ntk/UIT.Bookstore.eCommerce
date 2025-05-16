using KKBookstore.Data.Extensions;
using KKBookstore.StockTransactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.StockTransactions;

public class StockTransactionDetailConfiguration : IEntityTypeConfiguration<StockTransactionDetail>
{
    public void Configure(EntityTypeBuilder<StockTransactionDetail> builder)
    {
        builder.ToTable("StockTransactionDetails");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.VariantId).IsRequired();
        builder.Property(d => d.Quantity).IsRequired();
        builder.Property(d => d.UnitCost).IsRequired().HasPrecision(18, 2);

        builder.ConfigureAuditing();
    }
}
