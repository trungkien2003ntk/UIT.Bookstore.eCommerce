using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class SkuConfigurations : IEntityTypeConfiguration<Sku>
{
    public void Configure(EntityTypeBuilder<Sku> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName("SkuId");

        builder.Property(t => t.ProductId)
            .IsRequired();

        builder.ComplexProperty(t => t.SkuValue)
            .Property(sv => sv.Value)
            .HasMaxLength(50)
            .IsRequired();


        builder.Property(t => t.Barcode)
            .HasMaxLength(200);

        builder.Property(t => t.RecommendedRetailPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.TaxRate)
            .HasPrecision(18, 2);

        builder.Property(t => t.Quantity)
            .IsRequired();

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.Comment)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasOne(t => t.Product)
            .WithMany(t => t.Skus)
            .HasForeignKey(t => t.ProductId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);

    }
}
