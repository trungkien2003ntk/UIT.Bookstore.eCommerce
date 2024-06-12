using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using KKBookstore.Infrastructure.Web;
using KKBookstore.Infrastructure.Data;
using KKBookstore.Infrastructure.Identity;
using KKBookstore.Application.Common.Interfaces;
using System.Text;
using KKBookstore.Infrastructure.Data.Interceptors;
using KKBookstore.Infrastructure.Email;
using Microsoft.EntityFrameworkCore.Diagnostics;
using KKBookstore.Domain.Aggregates.UserAggregate;
using KKBookstore.Infrastructure.Shipping;
using KKBookstore.Infrastructure.Payment;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Caching.Memory;

namespace KKBookstore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        /// Config DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        

        services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, SoftDeleteInterceptor>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging();
        });
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


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
            .AddEntityFrameworkStores<ApplicationDbContext>()
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


        services.AddAzureClients(clientBuilder =>
        {
            clientBuilder.AddBlobServiceClient(configuration["StorageConnectionString:blob"]!, preferMsi: true);
            clientBuilder.AddQueueServiceClient(configuration["StorageConnectionString:queue"]!, preferMsi: true);
        });



        return services;
    }
}
