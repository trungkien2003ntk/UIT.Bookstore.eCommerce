using AutoMapper;
using KKBookstore.Application.Products.Queries.GetProductDetail;
using KKBookstore.Application.Products.Queries.GetProductList;
using KKBookstore.Domain.ProductAggregate;

namespace KKBookstore.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDetailDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.DisplayName))
            .ForMember(dest => dest.UnitMeasureName, opt => opt.MapFrom(src => src.UnitMeasure.Name))
            .ForMember(dest => dest.IsBook, opt => opt.MapFrom(src => src.IsBook))
            .ForMember(dest => dest.Skus, opt => opt.MapFrom(src => src.Skus));

        CreateMap<Product, ProductGeneralDto>()
            .ForMember(dest => dest.MinUnitPrice, opt => opt.MapFrom(src => src.Skus.Min(s => s.UnitPrice)))
            .ForMember(dest => dest.MinRecommendedRetailPrice, opt => opt.MapFrom(src => src.Skus.Min(s => s.RecommendedRetailPrice)))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Ratings.Average(s => s.RatingValue)))
            .ForMember(dest => dest.ThumbnailImageUrl, opt => opt.MapFrom(src =>
                src.Skus != null && src.Skus.Count != 0 &&
                src.Skus.First().SkuImages != null && src.Skus.First().SkuImages.Count != 0 ?
                src.Skus.First().SkuImages.First().ThumbnailImageUrl : null));

        CreateMap<Author, AuthorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        CreateMap<Sku, SkuDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => src.Barcode))
            .ForMember(dest => dest.OptionValues, opt => opt.MapFrom(src => src.SkuOptionValues.Select(sov => sov.OptionValue)));

        CreateMap<OptionValue, OptionValueDto>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Option.Name));
    }
}
