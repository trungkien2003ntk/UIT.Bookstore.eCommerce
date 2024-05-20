using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductTypeAttributeProductValueConfiguration : IEntityTypeConfiguration<ProductTypeAttributeProductValue>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeProductValue> builder)
    {
        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ProductTypeAttributeProductValue)}Id");

        builder.Property(t => t.ProductId)
            .IsRequired();

        builder.Property(t => t.ProductTypeAttributeValueId)
            .IsRequired();

        builder.HasIndex(t => new { t.ProductId, t.ProductTypeAttributeValueId })
            .IsUnique();

        builder.HasOne(t => t.Product)
            .WithMany()
            .HasForeignKey(t => t.ProductId);

        builder.HasOne(t => t.ProductTypeAttributeValue)
            .WithMany(av => av.ProductsAppliedValue)
            .HasForeignKey(t => t.ProductTypeAttributeValueId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
