using FluentValidation;
using KKBookstore.Application.Common.Behaviours;
using KKBookstore.Application.Features.Checkout.PlaceOrder;
using KKBookstore.Application.Features.ShoppingCarts;
using KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;
using KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KKBookstore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // Manual Mapping Services
        services.AddScoped<IUpdateShoppingCartMappingService, UpdateShoppingCartMappingService>();
        services.AddScoped<IGetShoppingCartMappingService, GetShoppingCartMappingService>();

        services.AddScoped<DefaultOrderProcessor>();

        // todo: Refactor the code to get rid of AutoMapper
        services.AddAutoMapper(assembly);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>)); 
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
