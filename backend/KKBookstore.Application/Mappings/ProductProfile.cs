using AutoMapper;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Features.Products.GetProductDetail;
using KKBookstore.Application.Features.Products.GetProductList;
using KKBookstore.Application.Features.Products.GetProductRatingList;
using KKBookstore.Application.Features.Products.Models;
using KKBookstore.Application.Mappings.Helpers;
using KKBookstore.Domain.Aggregates.ProductAggregate;

namespace KKBookstore.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, GetProductDetailResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ProductTypeId, opt => opt.MapFrom(src => src.ProductTypeId))
            .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.DisplayName))
            .ForMember(dest => dest.UnitMeasureName, opt => opt.MapFrom(src => src.UnitMeasure.Name))
            .ForMember(dest => dest.LargeImageUrls, opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.LargeImageUrl)))
            .ForMember(dest => dest.ThumbnailImageUrls, opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ThumbnailImageUrl)))
            .ForMember(dest => dest.IsBook, opt => opt.MapFrom(src => src.IsBook))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author).ToList()))
            .ForMember(dest => dest.ProductVariants, opt => opt.MapFrom(src => src.ProductVariants));

        CreateMap<Product, ProductSummary>()
            .ForMember(dest => dest.MinUnitPrice, opt => opt.MapFrom(src => src.ProductVariants.Count != 0 ? src.ProductVariants.Min(s => s.UnitPrice) : 0))
            .ForMember(dest => dest.MinRecommendedRetailPrice, opt => opt.MapFrom(src => src.ProductVariants.Count != 0 ? src.ProductVariants.Min(s => s.RecommendedRetailPrice) : 0))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Ratings.Count != 0 ? src.Ratings.Average(s => s.RatingValue) : 0))
            .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.DisplayName))
            .ForMember(dest => dest.ThumbnailImageUrl, opt => opt.MapFrom(src => src.GetFirstThumbnailImageUrl()));

        CreateMap<PaginatedResult<Product>, PaginatedResult<ProductSummary>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<Author, AuthorDto>();

        CreateMap<ProductVariant, ProductVariantDto>()
            .ForMember(dest => dest.SkuValue, opt => opt.MapFrom(src => src.SkuValue.Value))
            .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Dimension.Height))
            .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Dimension.Width))
            .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Dimension.Length))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.OptionValues, opt => opt.MapFrom(src => src.ProductVariantOptionValues.Select(sov => sov.OptionValue)))
            .ForMember(dest => dest.ThumbnailImageUrl, opt => opt.MapFrom(src => src.GetThumbnailImageUrl()))
            .ForMember(dest => dest.LargeImageUrl, opt => opt.MapFrom(src => src.GetLargeImageUrl()));


        CreateMap<ProductOptionValue, OptionValueDto>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Option.Name));

        CreateMap<(PaginatedResult<Rating>, List<Rating>), ProductRatingSummary>()
            .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Item1))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Item2.Average(r => new decimal(r.RatingValue))))
            .ForMember(dest => dest.TotalApprovedRating, opt => opt.MapFrom(src => src.Item2.Count(r => r.Status == RatingStatus.Approved)))
            .ForMember(dest => dest.TotalRatingWithComment, opt => opt.MapFrom(src => src.Item2.Count(r => !string.IsNullOrEmpty(r.Comment))))
            .ForMember(dest => dest.Total5StarRating, opt => opt.MapFrom(src => src.Item2.Count(r => r.RatingValue == 5)))
            .ForMember(dest => dest.Total4StarRating, opt => opt.MapFrom(src => src.Item2.Count(r => r.RatingValue == 4)))
            .ForMember(dest => dest.Total3StarRating, opt => opt.MapFrom(src => src.Item2.Count(r => r.RatingValue == 3)))
            .ForMember(dest => dest.Total2StarRating, opt => opt.MapFrom(src => src.Item2.Count(r => r.RatingValue == 2)))
            .ForMember(dest => dest.Total1StarRating, opt => opt.MapFrom(src => src.Item2.Count(r => r.RatingValue == 1)))
            .ForMember(dest => dest.TotalRating, opt => opt.MapFrom(src => src.Item2.Count));

        CreateMap<PaginatedResult<Rating>, PaginatedResult<ProductRatingDto>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<Rating, ProductRatingDto>()
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.LastName} {src.User.FirstName}"))
            .ForMember(dest => dest.UserAvatarUrl, opt => opt.MapFrom(src => src.User.ImageUrl))
            .ForMember(dest => dest.ProductVariantName, opt => opt.MapFrom(src => MappingHelpers.GetProductVariantOptionValuesString(src.ProductVariant)))
            .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

    }
}
