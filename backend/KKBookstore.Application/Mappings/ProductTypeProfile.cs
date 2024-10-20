using AutoMapper;
using KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;
using KKBookstore.Domain.Aggregates.ProductTypeAggregate;

namespace KKBookstore.Application.Mappings;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        // createmap for producttype
        CreateMap<ProductType, ProductTypeGeneralDto>();

        CreateMap<ProductTypeAttribute, ProductTypeAttributeDto>()
            .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.Values));

        CreateMap<ProductTypeAttributeMapping, ProductTypeAttributeDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductAttribute.Name))
            .ForMember(dest => dest.Values, opt => opt.MapFrom(src => src.ProductAttribute.Values));

        CreateMap<ProductTypeAttributeValue, ProductTypeAttributeValueDto>();
    }
}
