using AutoMapper;
using KKBookstore.API.Contracts.Requests;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Features.Orders.GetOrderList;
using KKBookstore.Application.Features.Products.GetProductList;
using KKBookstore.Application.Features.Products.GetProductRatingList;

namespace KKBookstore.API.Mappings;

public class RequestProfile : Profile
{
    public RequestProfile()
    {
        CreateMap<GetProductListRequest, GetProductListQuery>()
            .ForMember(dest => dest.ProductTypeIds, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ProductTypeIds) ? src.ProductTypeIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList() : new List<int>()))
            .ForMember(dest => dest.PriceRange, opt => opt.MapFrom(src => src.MinPrice.HasValue && src.MaxPrice.HasValue ? new PriceRange(src.MinPrice.Value, src.MaxPrice.Value) : null))
            .ForMember(dest => dest.ExcludeProductIds, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.ExcludeProductIds) ? src.ExcludeProductIds.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList() : new List<int>()))
            .ForMember(dest => dest.CustomFilters, opt => opt.MapFrom(src => src.CustomFilters != null ? src.CustomFilters.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()) : new Dictionary<string, List<string>>()));
    
        // OrderStatuses is a list of integers in string, so we need to check if it's null before mapping it
        CreateMap<Contracts.Requests.GetOrderListRequest, Application.Features.Orders.GetOrderList.GetOrderListQuery>()
            .ForMember(dest => dest.OrderStatuses, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.OrderStatuses) ? src.OrderStatuses.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() : new List<string>()));

        CreateMap<GetProductRatingListRequest, GetProductRatingListQuery>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId!.Value))
            .ForMember(dest => dest.Statuses, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Statuses) ? src.Statuses.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() : new List<string>()));
    }
}
