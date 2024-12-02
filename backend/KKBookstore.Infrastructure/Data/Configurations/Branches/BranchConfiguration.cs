using KKBookstore.Domain.Branches;
using KKBookstore.Domain.Shared.Branches;
using KKBookstore.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Infrastructure.Data.Configurations.Branches;

internal class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branchs");

        builder.Property(t => t.Name).HasColumnName(nameof(Branch.Name)).HasMaxLength(BranchConsts.NameMaxLength).IsRequired();
        builder.Property(t => t.Email).HasColumnName(nameof(Branch.Email)).HasMaxLength(BranchConsts.EmailMaxLength).IsRequired();
        builder.Property(t => t.Description).HasColumnName(nameof(Branch.Description)).HasMaxLength(BranchConsts.DescriptionMaxLength).IsRequired();
        
        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.ConfigureAuditing();
    }
}