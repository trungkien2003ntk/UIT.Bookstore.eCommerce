using KKBookstore.Domain.ProductTypes;
using KKBookstore.Domain.Shared.ProductTypes;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.ProductTypes;

internal class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
{
    public void Configure(EntityTypeBuilder<ProductType> builder)
    {
        builder.ToTable("ProductTypes");
        builder.ConfigureAuditing();

        builder.Property(t => t.DisplayName).HasColumnName(nameof(ProductType.DisplayName)).HasMaxLength(ProductTypeConsts.DisplayNameMaxLength).IsRequired();
        builder.Property(t => t.ProductTypeCode).HasColumnName(nameof(ProductType.ProductTypeCode)).HasMaxLength(ProductTypeConsts.ProductTypeCodeMaxLength).IsRequired();
        builder.Property(t => t.Description).HasColumnName(nameof(ProductType.Description)).HasMaxLength(ProductTypeConsts.DescriptionMaxLength);

        builder.Property(t => t.Level).IsRequired();
        builder.HasIndex(t => t.ProductTypeCode).IsUnique();

        builder.HasOne(t => t.ParentProductType).WithMany().HasForeignKey(t => t.ParentProductTypeId).OnDelete(DeleteBehavior.Restrict);
    }
}

