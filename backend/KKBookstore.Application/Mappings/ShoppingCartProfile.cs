using AutoMapper;
using KKBookstore.Application.Features.ShoppingCarts.GetShoppingCartItemList;
using KKBookstore.Application.Mappings.Helpers;
using KKBookstore.Application.Mappings.Resolvers;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;

namespace KKBookstore.Application.Mappings;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<ShoppingCartItem, ShoppingCartItemDto>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Sku.ProductId))
            .ForMember(dest => dest.SkuName, opt => opt.MapFrom(src => MappingHelpers.GetSkuOptionValuesString(src.Sku)))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Sku.Product.Name))
            .ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => src.Sku.Product.ProductType.Id))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Sku.UnitPrice))
            .ForMember(dest => dest.RecommendedRetailPrice, opt => opt.MapFrom(src => src.Sku.RecommendedRetailPrice))
            .ForMember(dest => dest.AvailableQuantity, opt => opt.MapFrom(src => src.Sku.Quantity))
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => MappingHelpers.GetSkuThumbnailImageUrl(src.Sku))) //todo: handle the possible null here
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Sku.Product.Description))
            .ForMember(dest => dest.CreatedWhen, opt => opt.MapFrom(src => src.CreatedWhen))
            .ForMember(dest => dest.SkuVariations, opt => opt.MapFrom(src => src.Sku.Product.Skus.ToList()))
            .ForMember(dest => dest.ProductOptions, opt => opt.MapFrom(src => src.Sku.Product.Options.ToList()));

        CreateMap<ProductOption, ProductOptionAttributeDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.OptionValues.Select(ov => ov.Value).ToList()))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => /*src.IsOptionWithImage ? MappingHelpers.GetOptionValuesWithImages(src) :*/ new List<string>()));

        CreateMap<Sku, SkuForCartDto>()
            .ForMember(dest => dest.SkuValue, opt => opt.MapFrom(src => src.SkuValue.Value))
            .ForMember(dest => dest.SkuName, opt => opt.MapFrom(src => MappingHelpers.GetSkuOptionValuesString(src)))
            .ForMember(dest => dest.AvailableQuantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            //.ForMember(dest => dest.OptionIndex, opt => opt.MapFrom(src => MappingHelpers.GetOptionIndices(src, src.Product)));
    }
}
