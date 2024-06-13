using KKBookstore.Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable($"{nameof(Transaction)}s");

        builder.Property(t => t.Id)
            .HasColumnName("TransactionId");

        builder.Property(t => t.Amount)
            .IsRequired();

        builder.Property(t => t.BankCode)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(t => t.BankTranNo)
            .HasColumnType("varchar(260)");

        builder.Property(t => t.CardType)
            .HasColumnType("varchar(25)");

        builder.Property(t => t.PayDate)
            .IsRequired();

        builder.Property(t => t.OrderInfo)
            .HasColumnType("varchar(260)")
            .IsRequired();

        builder.Property(t => t.TransactionNo)
            .IsRequired();

        builder.Property(t => t.ResponseCode)
            .IsRequired();

        builder.Property(t => t.TransactionStatus)
            .IsRequired();

        builder.Property(t => t.OrderId)
            .IsRequired();

        builder.Ignore("SecureHash");

        builder.HasOne(t => t.Order)
            .WithMany(o => o.Transactions)
            .HasForeignKey(t => t.OrderId);
    }
}

