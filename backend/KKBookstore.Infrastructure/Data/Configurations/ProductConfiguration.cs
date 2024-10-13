using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable($"{nameof(Product)}s");

        builder.Property(t => t.Id)
            .HasColumnName("ProductId");

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasColumnType("nvarchar(max)");

        builder.Property(t => t.ProductTypeId)
            .IsRequired();

        //builder.Property(t => t.UnitMeasureId)
        //    .IsRequired();

        builder.Property(t => t.IsDeleted)
            .IsRequired();

        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.HasIndex(t => t.ProductTypeId);

        builder.HasOne(t => t.ProductType)
            .WithMany()
            .HasForeignKey(t => t.ProductTypeId);

        builder.HasOne(t => t.UnitMeasure)
            .WithMany()
            .HasForeignKey(t => t.UnitMeasureId);


        builder.ConfigureAuditing();
    }
}
