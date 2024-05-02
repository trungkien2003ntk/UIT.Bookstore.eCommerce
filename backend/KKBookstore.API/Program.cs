using KKBookstore.API.HelperModels;
using KKBookstore.Data.EntitiesContext;
using KKBookstore.Data.Interceptors;
using KKBookstore.Model.Base;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

HelperConstantsModel.LoadConfig(configuration);

// Add services to the container.
builder.Services.AddDbContext<ProjectContext>(
    opt => opt
        .UseSqlServer(ProjectConfig.ConnectionString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        .EnableSensitiveDataLogging()
        .AddInterceptors(new SoftDeleteInterceptor()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
