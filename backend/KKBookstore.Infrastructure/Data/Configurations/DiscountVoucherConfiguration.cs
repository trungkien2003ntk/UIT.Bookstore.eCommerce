using KKBookstore.Domain.DiscountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class DiscountVoucherConfiguration : IEntityTypeConfiguration<DiscountVoucher>
{
    public void Configure(EntityTypeBuilder<DiscountVoucher> builder)
    {
        builder.Property(dv => dv.Id)
            .HasColumnName($"{nameof(DiscountVoucher)}Id");

        builder.Property(dv => dv.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(dv => dv.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(dv => dv.Value)
            .IsRequired();

        builder.Property(dv => dv.EndWhen)
            .IsRequired();

        builder.Property(dv => dv.StartWhen)
            .IsRequired();

        builder.OwnsOne(dv => dv.QuantityRange)
            .Property(qr => qr.MinApplyQuantity)
            .IsRequired();

        builder.OwnsOne(dv => dv.ValueRange)
            .Property(dv => dv.MinValue)
            .HasPrecision(18, 2);
        
        builder.OwnsOne(dv => dv.ValueRange)
            .Property(dv => dv.MaxValue)
            .HasPrecision(18, 2);

        builder.OwnsOne(dv => dv.QuantityRange)
            .Property(dv => dv.MinApplyQuantity)
            .HasPrecision(18, 2);
        
        builder.OwnsOne(dv => dv.QuantityRange)
            .Property(dv => dv.MaxApplyQuantity)
            .HasPrecision(18, 2);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
