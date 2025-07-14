using KKBookstore.Data.Extensions;
using KKBookstore.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKBookstore.Data.Configurations.Settings;

internal class ApplicationSettingConfiguration : IEntityTypeConfiguration<ApplicationSetting>
{
    public void Configure(EntityTypeBuilder<ApplicationSetting> builder)
    {
        builder.ToTable("ApplicationSettings");

        builder.ConfigureAuditing();
        builder.Property(x => x.Key).HasMaxLength(100);
        builder.Property(x => x.Value);
    }
}
