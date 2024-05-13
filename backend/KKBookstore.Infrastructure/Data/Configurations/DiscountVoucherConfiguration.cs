using KKBookstore.Domain.DiscountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class DiscountVoucherConfiguration : IEntityTypeConfiguration<DiscountVoucher>
{
    public void Configure(EntityTypeBuilder<DiscountVoucher> builder)
    {
        builder.Property(dv => dv.Id)
            .HasColumnName("DiscountVoucherId");

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

        // complex type: QuantityRange and ValueRange
        builder.ComplexProperty(dv => dv.QuantityRange)
            .Property(qr => qr.MinApplyQuantity)
            .IsRequired();

        builder.ComplexProperty(dv => dv.ValueRange);


        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
