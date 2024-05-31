using AutoMapper;
using KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;
using KKBookstore.Application.Mappings.Helpers;
using KKBookstore.Domain.Aggregates.ProductAggregate;

namespace KKBookstore.Application.Mappings.Resolvers;

public class OptionIndicesResolver : IValueResolver<Sku, SkuForCartDto, List<int>>
{
    public List<int> Resolve(Sku source, SkuForCartDto destination, List<int> destMember, ResolutionContext context)
    {
        var product = source.Product; // Ensure Product is included in the source
        return MappingHelpers.GetOptionIndices(source, product);
    }
}
