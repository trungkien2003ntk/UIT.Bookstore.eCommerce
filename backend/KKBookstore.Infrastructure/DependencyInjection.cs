using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using KKBookstore.Domain.Users;
using KKBookstore.Infrastructure.Web;
using KKBookstore.Infrastructure.Data;
using KKBookstore.Infrastructure.Identity;
using KKBookstore.Application.Common.Interfaces;
using System.Text;
using KKBookstore.Infrastructure.Data.Interceptors;

namespace KKBookstore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<ProjectContext>(options =>
        //{
        //    options.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=KKBookstoreDb;User Id=kien;Password=ntk0108;TrustServerCertificate=True;")
        //        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        //        .EnableSensitiveDataLogging()
        //        .AddInterceptors(new SoftDeleteInterceptor());
        //});

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddScoped<IIdentityService, IdentityService>();
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            //options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .AddInterceptors(new SoftDeleteInterceptor());
        });


        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

        services.AddAuthorization();

        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddSingleton(TimeProvider.System);



        return services;
    }
}
