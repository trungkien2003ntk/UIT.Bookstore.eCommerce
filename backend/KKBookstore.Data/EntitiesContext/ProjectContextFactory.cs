using KKBookstore.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KKBookstore.Data.EntitiesContext;

public class ProjectContextFactory : IDesignTimeDbContextFactory<ProjectContext>
{
    public ProjectContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProjectContext>();
        optionsBuilder
            .UseSqlServer("Server=(local)\\SQLEXPRESS;Database=KKBookstoreDb;User Id=kien;Password=ntk0108;TrustServerCertificate=True;")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .EnableSensitiveDataLogging()
            .AddInterceptors(new SoftDeleteInterceptor());

        return new ProjectContext(optionsBuilder.Options);
    }
}
