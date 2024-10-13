using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Infrastructure.Data;
using KKBookstore.Infrastructure.Data.Interceptors;
using KKBookstore.Infrastructure.Email;
using KKBookstore.Infrastructure.Identity;
using KKBookstore.Infrastructure.Payment;
using KKBookstore.Infrastructure.Search;
using KKBookstore.Infrastructure.Shipping;
using KKBookstore.Infrastructure.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KKBookstore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        /// Config DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");


        services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, SoftDeleteInterceptor>();
        services.AddDbContext<KKBookstoreDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

#if DEBUG
            options.UseSqlServer(connectionString)
                /*.EnableSensitiveDataLogging()*/;
#endif
        });

        services.AddScoped<IApplicationDbContext, KKBookstoreDbContext>();


        /// Config AuthN and AuthZ
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.AddScoped<IIdentityService, IdentityService>();
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        services
            .AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
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


        // More information, see AddIdentityCore vs AddIdentity
        // AddIdentity replace the default authentication scheme with CookieSchemes while AddIdentityCore not
        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole<int>>()
            .AddRoleManager<RoleManager<IdentityRole<int>>>()
            .AddEntityFrameworkStores<KKBookstoreDbContext>()
            .AddApiEndpoints();


        /// Config OTP service
        services.AddScoped<IOtpService, OtpService>();


        /// Config Email
        services.Configure<EmailConfiguration>(configuration.GetSection(nameof(EmailConfiguration)));
        services.AddTransient<IEmailService, EmailService>();

        /// Config Shipping
        services.Configure<ShippingConfiguration>(configuration.GetSection(nameof(ShippingConfiguration)));
        /// old way
        //services.AddScoped<IShippingService, ShippingService>();

        /// new way: added decorator
        services.AddScoped<ShippingService>();
        services.AddScoped<IShippingService>(provider =>
        {
            return new CachedShippingService(
                provider.GetRequiredService<IMemoryCache>(),
                provider.GetRequiredService<ShippingService>()
            );
        });


        /// Additional Config
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddSingleton(TimeProvider.System);
        services.AddHttpClient();

        /// Config VnPay Payment
        services.Configure<VnPayConfiguration>(configuration.GetSection(nameof(VnPayConfiguration)));
        services.AddScoped<IPaymentService, VnPayPaymentService>();


        /// Config Azure Search
        services.Configure<SearchConfiguration>(configuration.GetSection(nameof(SearchConfiguration)));
        services.AddScoped<ISearchService, SearchService>();



        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration["StorageConnectionString:blob"]!, preferMsi: true);
            clientBuilder.AddQueueServiceClient(configuration["StorageConnectionString:queue"]!, preferMsi: true);
        });



        return services;
    }
}
