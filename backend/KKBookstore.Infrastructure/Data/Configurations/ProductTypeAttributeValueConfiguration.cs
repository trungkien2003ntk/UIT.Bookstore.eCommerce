using KKBookstore.Domain.Aggregates.ProductTypeAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductTypeAttributeValueConfiguration : IEntityTypeConfiguration<ProductTypeAttributeValue>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeValue> builder)
    {
        builder.ToTable($"{nameof(ProductTypeAttributeValue)}s");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName($"{nameof(ProductTypeAttributeValue)}Id");

        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => new { x.ProductTypeAttributeId, x.Value })
            .IsUnique();

        builder.HasOne(x => x.ProductTypeAttribute)
            .WithMany(y => y.Values)
            .HasForeignKey(x => x.ProductTypeAttributeId);


        builder.ConfigureAuditing();
    }
}
