using AutoMapper;
using KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;
using KKBookstore.Domain.ProductTypes;

namespace KKBookstore.Application.Mappings;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        CreateMap<ProductType, ProductTypeGeneralDto>();
        CreateMap<ProductTypeAttribute, ProductTypeAttributeDto>();
        CreateMap<ProductTypeAttributeValue, ProductTypeAttributeValueDto>();
    }
}
