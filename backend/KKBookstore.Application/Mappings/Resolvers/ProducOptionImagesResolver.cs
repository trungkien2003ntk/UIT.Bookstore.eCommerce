using AutoMapper;
using KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;
using KKBookstore.Application.Mappings.Helpers;
using KKBookstore.Domain.Aggregates.ProductAggregate;

namespace KKBookstore.Application.Mappings.Resolvers
{
    public class ProductOptionImagesResolver : IValueResolver<ProductOption, ProductOptionAttributeDto, List<string>>
    {
        public List<string> Resolve(ProductOption source, ProductOptionAttributeDto destination, List<string> destMember, ResolutionContext context)
        {
            return source.IsOptionWithImage ? MappingHelpers.GetOptionValuesWithImages(source) : [];
        }
    }
}
