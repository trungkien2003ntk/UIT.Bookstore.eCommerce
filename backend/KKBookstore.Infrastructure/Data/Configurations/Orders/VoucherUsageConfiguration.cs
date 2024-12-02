using KKBookstore.Domain.Orders;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Orders;

internal class VoucherUsageConfiguration : IEntityTypeConfiguration<VoucherUsage>
{
    public void Configure(EntityTypeBuilder<VoucherUsage> builder)
    {
        builder.ToTable("VoucherUsages");
        builder.ConfigureAuditing();

        builder.Property(vu => vu.VoucherId).HasColumnName(nameof(VoucherUsage.VoucherId)).IsRequired();
        builder.Property(vu => vu.CustomerId).HasColumnName(nameof(VoucherUsage.CustomerId)).IsRequired();
        builder.Property(vu => vu.OrderId).HasColumnName(nameof(VoucherUsage.OrderId)).IsRequired();

        builder.HasOne(vu => vu.Voucher).WithMany(v => v.VoucherUsages).HasForeignKey(vu => vu.VoucherId);
        builder.HasOne(vu => vu.Order).WithMany().HasForeignKey(vu => vu.OrderId);
        builder.HasOne(vu => vu.Customer).WithMany().HasForeignKey(vu => vu.CustomerId);
    }
}