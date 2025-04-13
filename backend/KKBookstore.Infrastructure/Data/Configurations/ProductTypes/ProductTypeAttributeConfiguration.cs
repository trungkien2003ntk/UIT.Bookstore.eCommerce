using KKBookstore.Data.Extensions;
using KKBookstore.ProductTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.ProductTypes;

internal class ProductTypeAttributeConfiguration : IEntityTypeConfiguration<ProductTypeAttribute>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttribute> builder)
    {
        builder.ToTable("ProductTypeAttributes");
        builder.ConfigureAuditing();

        builder.Property(x => x.Name).HasColumnName(nameof(ProductTypeAttribute.Name)).HasMaxLength(ProductTypeAttributeConsts.NameMaxLength).IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();
    }
}