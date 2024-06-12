using KKBookstore.Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class VoucherUsageConfiguration : IEntityTypeConfiguration<VoucherUsage>
{
    public void Configure(EntityTypeBuilder<VoucherUsage> builder)
    {
        builder.ToTable($"{nameof(VoucherUsage)}s");

        builder.HasKey( x => x.Id)
            .HasName($"{nameof(VoucherUsage)}Id");
        
        builder.HasOne(vu => vu.Voucher)
            .WithMany(v => v.VoucherUsages)
            .HasForeignKey(vu => vu.VoucherId);

        builder.HasOne(vu => vu.Order)
            .WithMany()
            .HasForeignKey(vu => vu.OrderId);

        builder.HasOne(vu => vu.User)
            .WithMany()
            .HasForeignKey(vu => vu.UserId);
    }
}