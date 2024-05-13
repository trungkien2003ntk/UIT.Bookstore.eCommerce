using FluentValidation;
using KKBookstore.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KKBookstore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddAutoMapper(assembly);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>)); 
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
