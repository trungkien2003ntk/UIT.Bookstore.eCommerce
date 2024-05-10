using KKBookstore.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KKBookstore.Infrastructure.Data;

public class ApplicationDbContextFactory : IdesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder
            .UseSqlServer("Server=(local)\\SQLEXPRESS;Database=KKBookstoreDb;User Id=kien;Password=ntk0108;TrustServerCertificate=True;")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .EnableSensitiveDataLogging()
            .AddInterceptors(new SoftDeleteInterceptor());

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
