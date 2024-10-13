using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class SkuConfigurations : IEntityTypeConfiguration<Sku>
{
    public void Configure(EntityTypeBuilder<Sku> builder)
    {
        var converter = new EnumToStringConverter<SkuStatus>();

        // Property Config
        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(Sku)}Id");

        builder.Property(t => t.ProductId)
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

        builder.Property(t => t.Weight)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(t => t.Quantity)
            .IsRequired();

        builder.Property(t => t.Status)
            .IsRequired()
            .HasConversion(converter);

        builder.Property(t => t.Comment)
            .HasMaxLength(500)
            .IsRequired();

        // Value Objects
        builder.OwnsOne(t => t.SkuValue)
            .Property(sv => sv.Value)
            .HasMaxLength(50)
            .IsRequired();

        builder.OwnsOne(t => t.Dimension)
            .Property(d => d.Length)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.OwnsOne(t => t.Dimension)
            .Property(d => d.Width)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.OwnsOne(t => t.Dimension)
            .Property(d => d.Height)
            .HasPrecision(18, 2)
            .IsRequired();

        // Foreign Key

        builder.HasOne(t => t.Product)
            .WithMany(t => t.Skus)
            .HasForeignKey(t => t.ProductId);


        builder.ConfigureAuditing();

    }
}
