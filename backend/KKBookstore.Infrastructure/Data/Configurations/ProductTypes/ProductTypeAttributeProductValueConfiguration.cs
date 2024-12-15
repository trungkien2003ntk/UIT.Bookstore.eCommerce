using KKBookstore.Domain.ProductTypes;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.ProductTypes;

internal class ProductTypeAttributeProductValueConfiguration : IEntityTypeConfiguration<ProductTypeAttributeProductValue>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeProductValue> builder)
    {
        builder.ToTable("ProductTypeAttributeProductValues");
        builder.ConfigureAuditing();

        builder.Property(t => t.ProductId).HasColumnName(nameof(ProductTypeAttributeProductValue.ProductId)).IsRequired();
        builder.Property(t => t.AttributeValueId).HasColumnName(nameof(ProductTypeAttributeProductValue.AttributeValueId)).IsRequired();

        builder.HasIndex(t => new { t.ProductId, t.AttributeValueId }).IsUnique();

        builder.HasOne(t => t.Product).WithMany().HasForeignKey(t => t.ProductId);
        builder.HasOne(t => t.AttributeValue).WithMany(av => av.ProductsAppliedValue).HasForeignKey(t => t.AttributeValueId).OnDelete(DeleteBehavior.Cascade);
    }
}
