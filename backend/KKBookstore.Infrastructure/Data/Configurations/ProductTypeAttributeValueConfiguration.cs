using KKBookstore.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

internal class ProductTypeAttributeValueConfiguration : IEntityTypeConfiguration<ProductTypeAttributeValue>
{
    public void Configure(EntityTypeBuilder<ProductTypeAttributeValue> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName($"{nameof(ProductTypeAttributeValue)}Id");

        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(200);


        builder.HasIndex(x => x.Value)
            .IsUnique();

        builder.HasOne(x => x.ProductTypeAttribute)
            .WithMany(y => y.Values)
            .HasForeignKey(x => x.ProductTypeAttributeId);

        builder.HasOne(t => t.LastEditedByUser)
            .WithMany()
            .HasForeignKey(t => t.LastEditedBy);

        builder.HasOne(t => t.CreatedByUser)
            .WithMany()
            .HasForeignKey(t => t.CreatedBy);
    }
}
