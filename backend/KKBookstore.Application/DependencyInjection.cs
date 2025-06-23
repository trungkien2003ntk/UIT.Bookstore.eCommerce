using FluentValidation;
using KKBookstore.Common.Behaviours;
using KKBookstore.Common.Interfaces;
using KKBookstore.Features.Checkout.PlaceOrder;
using KKBookstore.Features.ShoppingCarts.GetShoppingCartItemList;
using KKBookstore.Features.ShoppingCarts.UpdateShoppingCartItem;
using KKBookstore.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace KKBookstore;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        // Manual Mapping Services
        services.AddScoped<IUpdateShoppingCartMappingService, UpdateShoppingCartMappingService>();
        services.AddScoped<IGetShoppingCartMappingService, GetShoppingCartMappingService>();
        services.AddScoped<DefaultOrderProcessor>();        // Services
        services.AddScoped<ProductTypeAttributeService>();
        services.AddScoped<IProductTypeHierarchyService, ProductTypeHierarchyService>();

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
