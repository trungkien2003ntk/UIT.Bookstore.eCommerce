using KKBookstore.Domain.Products;
using KKBookstore.Domain.Shared.Products;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Products;

internal class UnitMeasureConfiguration : IEntityTypeConfiguration<UnitMeasure>
{
    public void Configure(EntityTypeBuilder<UnitMeasure> builder)
    {
        builder.ToTable("UnitMeasures");
        builder.ConfigureAuditing();
        builder.Property(t => t.Name).HasColumnName(nameof(UnitMeasure.Name)).HasMaxLength(UnitMeasureConsts.NameMaxLength).IsRequired();
        builder.Property(t => t.Description).HasColumnName(nameof(UnitMeasure.Description)).HasMaxLength(UnitMeasureConsts.DescriptionMaxLength);
        
        builder.HasIndex(t => t.Name).IsUnique();
    }
}
