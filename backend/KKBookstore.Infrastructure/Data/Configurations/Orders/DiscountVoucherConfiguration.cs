using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Shared.Orders;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Infrastructure.Data.Configurations.Orders;

internal class DiscountVoucherConfiguration : IEntityTypeConfiguration<DiscountVoucher>
{
    public void Configure(EntityTypeBuilder<DiscountVoucher> builder)
    {
        builder.ToTable("DiscountVouchers");
        builder.ConfigureAuditing();

        builder.Property(dv => dv.Name).HasColumnName(nameof(DiscountVoucher.Name)).HasMaxLength(DiscountVoucherConsts.NameMaxLength).IsRequired();
        builder.Property(dv => dv.Code).HasColumnName(nameof(DiscountVoucher.Code)).HasMaxLength(DiscountVoucherConsts.CodeMaxLength).IsRequired();
        builder.Property(dv => dv.Description).HasColumnName(nameof(DiscountVoucher.Description)).HasMaxLength(DiscountVoucherConsts.DescriptionMaxLength);
        builder.Property(dv => dv.Value).HasPrecision(18, 2).IsRequired();
        builder.Property(dv => dv.MaximumDiscountValue).HasPrecision(18, 2);
        builder.Property(dv => dv.MinimumSpend).HasPrecision(18, 2).IsRequired();
        builder.Property(dv => dv.UsageLimitOverall).IsRequired();
        builder.Property(dv => dv.StartTime).IsRequired();
        builder.Property(dv => dv.EndTime).IsRequired();
        builder.Property(dv => dv.VoucherType).IsRequired().HasConversion<EnumToStringConverter<DiscountVoucherType>>();
    }
}
