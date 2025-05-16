using KKBookstore.Data.Extensions;
using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockAdjustments;
using KKBookstore.StockTransactions.StockTransfers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Data.Configurations.StockTransactions;

public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
{
    public void Configure(EntityTypeBuilder<StockTransaction> builder)
    {
        builder.ToTable("StockTransactions");

        builder.HasKey(st => st.Id);
        builder.Property(st => st.Code).IsRequired().HasMaxLength(StockTransactionConsts.CodeMaxLength);
        builder.Property(st => st.Remarks).HasMaxLength(StockTransactionConsts.RemarksMaxLength);
        builder.Property(st => st.Reason).HasMaxLength(StockTransactionConsts.ReasonMaxLength);
        builder.Property(st => st.TransactionStatus).IsRequired().HasConversion<EnumToStringConverter<StockTransactionStatus>>();

        builder.ConfigureAuditing();
        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.StockTransactionId);

        builder.HasDiscriminator(x => x.TransactionType)
            .HasValue<StockTransaction>(StockTransactionType.Default)
            .HasValue<StockAdjustment>(StockTransactionType.StockAdjustment)
            .HasValue<StockTransfer>(StockTransactionType.StockTransfer);

    }
}
