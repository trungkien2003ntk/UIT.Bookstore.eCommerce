using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductTypeAttributeMappingConfiguration : IEntityTypeConfiguration<ProductTypeAttributeMapping>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeMapping> builder)
    {
        builder.ToTable($"{nameof(ProductTypeAttributeMapping)}s");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName($"{nameof(ProductTypeAttributeMapping)}Id");

        builder.HasOne(x => x.ProductType)
            .WithMany(y => y.Attributes)
            .HasForeignKey(x => x.ProductTypeId);

        builder.HasOne(x => x.ProductAttribute)
            .WithMany(y => y.ProductTypes)
            .HasForeignKey(x => x.ProductAttributeId);


        builder.ConfigureAuditing();
    }
}
