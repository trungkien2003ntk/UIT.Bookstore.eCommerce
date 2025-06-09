using KKBookstore;
using KKBookstore.Common.Configuration;
using KKBookstore.Common.Interfaces;
using KKBookstore.Data;
using KKBookstore.Data.Interceptors;
using KKBookstore.Emailing;
using KKBookstore.Identity;
using KKBookstore.Payment;
using KKBookstore.Search;
using KKBookstore.Shipping;
using KKBookstore.Storage;
using KKBookstore.Users;
using KKBookstore.Web;
using KKBookstore.Infrastructure.AI;
using KKBookstore.Features.Admin.Services;
using DotnetGeminiSDK;
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
using KKBookstore.AI;

namespace KKBookstore;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        /// Config DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditingInterceptor>();
        services.AddDbContext<KKBookstoreDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

#if DEBUG
            options
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging();
            ;
#else
            options.UseSqlServer(connectionString);
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
        services.AddSingleton<IEmailSender, DefaultEmailSender>();

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
        services.AddScoped<IPaymentService, VnPayPaymentService>();        /// Config Azure Search
        services.Configure<SearchConfiguration>(configuration.GetSection(nameof(SearchConfiguration)));
        services.AddScoped<ISearchService, SearchService>();

        /// Config Gemini AI
        services.Configure<GeminiConfiguration>(configuration.GetSection(nameof(GeminiConfiguration)));
        services.AddGeminiClient(config =>
        {
            var geminiConfig = configuration.GetSection(nameof(GeminiConfiguration)).Get<GeminiConfiguration>();
            config.ApiKey = geminiConfig?.ApiKey ?? throw new InvalidOperationException("Gemini API Key is required");
            config.TextBaseUrl = geminiConfig?.TextBaseUrl ?? "https://generativelanguage.googleapis.com/v1/models/gemini-pro";
            config.ImageBaseUrl = geminiConfig?.ImageBaseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro-vision";
            config.ModelBaseUrl = geminiConfig?.ModelBaseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models";
            config.EmbeddingBaseUrl = geminiConfig?.EmbeddingBaseUrl ?? "https://generativelanguage.googleapis.com/v1beta/models";
        });
        services.AddScoped<IGeminiService, GeminiService>();        /// Config AI Moderation
        services.Configure<ModerationConfiguration>(configuration.GetSection(ModerationConfiguration.SectionName));
        services.AddScoped<ICommentModerationService, CommentModerationService>();
        services.AddScoped<IModerationNotificationService, ModerationNotificationService>();

        /// Config Related Products AI Service
        services.AddScoped<IRelatedProductsService, RelatedProductsService>();


        var storageConnectionString = configuration.GetConnectionString("AzureStorage");
        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient(storageConnectionString);

            builder.AddQueueServiceClient(storageConnectionString);
        });

        services.AddScoped<IBlobStorageService, AzureBlobStorageService>();
        services.AddScoped<IQueueStorageService, AzureQueueStorageService>();
        services.AddScoped<IServiceBus, AzureServiceBus>();
        return services;
    }
}
