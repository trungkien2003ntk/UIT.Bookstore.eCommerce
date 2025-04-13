using AutoMapper;
using KKBookstore.Features.ProductTypes.GetProductTypeAttributes;
using KKBookstore.ProductTypes;

namespace KKBookstore.Mappings;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        CreateMap<ProductType, ProductTypeGeneralDto>();
        CreateMap<ProductTypeAttribute, ProductTypeAttributeDto>();
        CreateMap<ProductTypeAttributeValue, ProductTypeAttributeValueDto>();
    }
}
