using KKBookstore.StockTransactions;
using KKBookstore.StockTransactions.StockTransfers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.StockTransactions.StockTransfers;

public class StockTransferItemConfiguration : IEntityTypeConfiguration<StockTransferItem>
{
    public void Configure(EntityTypeBuilder<StockTransferItem> builder)
    {
        builder.HasBaseType<StockTransactionDetail>();
    }
}
