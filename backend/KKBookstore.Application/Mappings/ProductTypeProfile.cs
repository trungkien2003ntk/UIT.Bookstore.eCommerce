using AutoMapper;
using KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;
using KKBookstore.Domain.ProductTypes;

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
