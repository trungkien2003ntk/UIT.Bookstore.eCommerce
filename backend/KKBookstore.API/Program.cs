using KKBookstore.API.Infrastructure;
using KKBookstore.API.Mappings;
using KKBookstore.Application;
using KKBookstore.Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//HelperConstantsModel.LoadConfig(configuration);

// config to use appSetting.<env>.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add services to the container.
// add mapper profiles
builder.Services.AddAutoMapper(typeof(RequestProfile));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllersWithViews(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                      "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                      "Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});

// add global exceptional handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Congirue CORS to allow all origins
var myAllowAllOrigins = "AllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowAllOrigins,
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);


// add serilog to builder
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(hostingContext.Configuration);
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

Log.Information("Running application");
app.UseCors(myAllowAllOrigins);

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


//using (var scope = app.Services.CreateScope())
//{
//    var identityRoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

//    // Create a list of roles for admin, salesstaff ,customercarestaff and customer and then add to the database
//    var roles = new List<string> { "Admin", "SalesStaff", "CustomerCareStaff", "Customer" };

//    foreach (var role in roles)
//    {
//        if (!await identityRoleManager.RoleExistsAsync(role))
//        {
//            await identityRoleManager.CreateAsync(new IdentityRole<int>(role));
//        }
//    }
//}

app.Run();
