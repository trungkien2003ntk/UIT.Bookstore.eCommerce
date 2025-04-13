using KKBookstore.Data.Extensions;
using KKBookstore.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Products;

public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
{
    public void Configure(EntityTypeBuilder<ProductOption> builder)
    {
        builder.ToTable("ProductOptions");
        builder.ConfigureAuditing();

        builder.Property(t => t.Name).HasColumnName(nameof(ProductOption.Name)).HasMaxLength(ProductOptionConsts.NameMaxLength).IsRequired();

        builder.Property(t => t.IsOptionWithImage).HasColumnName(nameof(ProductOption.IsOptionWithImage)).IsRequired();

        builder.HasOne(t => t.Product)
            .WithMany(t => t.Options)
            .HasForeignKey(t => t.ProductId);
    }
}
