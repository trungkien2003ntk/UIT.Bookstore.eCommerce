using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Shared.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Orders;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable($"Transactions");

        builder.Property(t => t.Amount).IsRequired();

        builder.Property(t => t.BankCode).HasColumnName(nameof(Transaction.BankCode)).IsUnicode(false).HasMaxLength(TransactionConsts.BankCodeMaxLength).IsRequired();
        builder.Property(t => t.BankTranNo).HasColumnName(nameof(Transaction.BankTranNo)).IsUnicode(false).HasMaxLength(TransactionConsts.BankTranNoMaxLength);
        builder.Property(t => t.CardType).HasColumnName(nameof(Transaction.CardType)).IsUnicode(false).HasMaxLength(TransactionConsts.CardTypeMaxLength).IsRequired();
        builder.Property(t => t.PayDate).HasColumnName(nameof(Transaction.PayDate)).IsRequired();
        builder.Property(t => t.OrderInfo).HasColumnName(nameof(Transaction.OrderInfo)).IsUnicode(false).HasMaxLength(TransactionConsts.OrderInfoMaxLength).IsRequired();
        builder.Property(t => t.TransactionNo).HasColumnName(nameof(Transaction.TransactionNo)).IsRequired();
        builder.Property(t => t.ResponseCode).HasColumnName(nameof(Transaction.ResponseCode)).IsRequired();
        builder.Property(t => t.TransactionStatus).HasColumnName(nameof(Transaction.TransactionStatus)).IsRequired();
        builder.Property(t => t.OrderId).HasColumnName(nameof(Transaction.OrderId)).IsRequired();
        builder.HasOne(t => t.Order).WithMany(o => o.Transactions).HasForeignKey(t => t.OrderId);
    }
}

