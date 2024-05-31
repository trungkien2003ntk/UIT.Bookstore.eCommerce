using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductTypeAttributeProductValueConfiguration : IEntityTypeConfiguration<ProductTypeAttributeProductValue>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeProductValue> builder)
    {
        builder.ToTable($"{nameof(ProductTypeAttributeProductValue)}s");

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ProductTypeAttributeProductValue)}Id");

        builder.Property(t => t.ProductId)
            .IsRequired();

        builder.Property(t => t.AttributeValueId)
            .IsRequired();

        builder.HasIndex(t => new { t.ProductId, t.AttributeValueId })
            .IsUnique();

        builder.HasOne(t => t.Product)
            .WithMany()
            .HasForeignKey(t => t.ProductId);

        builder.HasOne(t => t.AttributeValue)
            .WithMany(av => av.ProductsAppliedValue)
            .HasForeignKey(t => t.AttributeValueId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
