using KKBookstore.Data.Extensions;
using KKBookstore.ProductTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.ProductTypes;

internal class ProductTypeAttributeValueConfiguration : IEntityTypeConfiguration<ProductTypeAttributeValue>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeValue> builder)
    {
        builder.ToTable("ProductTypeAttributeValues");
        builder.ConfigureAuditing();

        builder.Property(x => x.Value).HasColumnName(nameof(ProductTypeAttributeValue.Value)).HasMaxLength(ProductTypeAttributeValueConsts.ValueMaxLength).IsRequired();

        builder.HasIndex(x => new { x.ProductTypeAttributeId, x.Value }).IsUnique();

        builder.HasOne(x => x.ProductTypeAttribute).WithMany(y => y.Values).HasForeignKey(x => x.ProductTypeAttributeId);
    }
}
