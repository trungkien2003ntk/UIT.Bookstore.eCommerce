using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations;

public class ProductOptionConfiguration : IEntityTypeConfiguration<ProductOption>
{
    public void Configure(EntityTypeBuilder<ProductOption> builder)
    {
        builder.ToTable($"{nameof(ProductOption)}s");

        builder.Property(t => t.Id)
            .HasColumnName($"{nameof(ProductOption)}Id");

        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.IsOptionWithImage)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(t => t.Product)
            .WithMany(t => t.Options)
            .HasForeignKey(t => t.ProductId);


        builder.ConfigureAuditing();
    }
}
