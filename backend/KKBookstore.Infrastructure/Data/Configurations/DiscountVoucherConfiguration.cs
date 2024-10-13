using KKBookstore.Domain.Aggregates.OrderAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class DiscountVoucherConfiguration : IEntityTypeConfiguration<DiscountVoucher>
{
    public void Configure(EntityTypeBuilder<DiscountVoucher> builder)
    {
        var converter = new EnumToStringConverter<DiscountVoucherType>();

        builder.ToTable($"{nameof(DiscountVoucher)}s");

        builder.Property(dv => dv.Id)
            .HasColumnName($"{nameof(DiscountVoucher)}Id");

        builder.Property(dv => dv.Name)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(dv => dv.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(dv => dv.Description)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(dv => dv.Value)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(dv => dv.MaximumDiscountValue)
            .HasPrecision(18, 2);

        builder.Property(dv => dv.MinimumSpend)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(dv => dv.UsageLimitOverall)
            .IsRequired();

        builder.Property(dv => dv.StartWhen)
            .IsRequired();

        builder.Property(dv => dv.EndWhen)
            .IsRequired();

        builder.Property(dv => dv.VoucherType)
            .IsRequired()
            .HasConversion(converter);

        builder.ConfigureAuditing();
    }
}
