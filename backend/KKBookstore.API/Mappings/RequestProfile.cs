using AutoMapper;
using KKBookstore.Common.Models;
using KKBookstore.Contracts.Requests;
using KKBookstore.Features.Orders.GetOrderList;
using KKBookstore.Features.Products.GetProductList;
using KKBookstore.Features.Products.GetProductRatingList;
using KKBookstore.Features.Products.GetRelatedProductsByImage;

namespace KKBookstore.Mappings;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<GetProductListRequest, GetProductListQuery>()
            .ForMember(dest => dest.PriceRange, opt => opt.MapFrom(src => src.MinPrice.HasValue && src.MaxPrice.HasValue ? new PriceRange(src.MinPrice.Value, src.MaxPrice.Value) : null));

        // OrderStatuses is a list of integers in string, so we need to check if it's null before mapping it
        CreateMap<GetOrderListRequest, GetOrderListQuery>()
            .ForMember(dest => dest.OrderStatuses, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.OrderStatuses) ? src.OrderStatuses.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() : new List<string>()));

        CreateMap<GetProductRatingListRequest, GetProductRatingListQuery>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.Statuses, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Statuses) ? src.Statuses.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() : new List<string>()));

        CreateMap<GetRelatedProductsByImageRequest, GetRelatedProductsByImageQuery>()
            .ForMember(dest => dest.Base64Image, opt => opt.MapFrom(src => src.Base64Image));
    }
}
