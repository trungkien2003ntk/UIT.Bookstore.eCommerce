using AutoMapper;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Application.Features.Orders.GetOrderDetail;
using KKBookstore.Application.Features.Orders.GetOrderList;
using KKBookstore.Application.Features.Orders.Models;
using KKBookstore.Application.Mappings.Helpers;
using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Orders;

namespace KKBookstore.Application.Mappings;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<PagedResult<Order>, PagedResult<OrderGeneralInformation>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<Order, GetOrderDetailResponse>()
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

        CreateMap<ShippingAddress, ShippingAddressDto>()
            .ForMember(dest => dest.DetailedFullAddress, opt => opt.MapFrom(src => $"{src.DetailAddress}, {src.Commune}, {src.District}, {src.Province}")); ;

        CreateMap<Order, OrderGeneralInformation>()
            .ForMember(dest => dest.DeliveryMethodName, opt => opt.MapFrom(src => src.DeliveryMethod.Name))
            .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CalculateTotal()))
            .ForMember(dest => dest.ShippingVoucherId, opt => opt.MapFrom(src => src.ShippingDiscountVoucherId))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines));

        CreateMap<OrderLine, OrderLineDto>()
            .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.ProductVariantId))
            .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => MappingHelpers.GetProductVariantThumbnailImageUrl(src.ProductVariant)))
            .ForMember(dest => dest.ProductVariantOptionName, opt => opt.MapFrom(src => MappingHelpers.GetProductVariantOptionValuesString(src.ProductVariant)))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVariant.Product.Name))
            .ForMember(dest => dest.RecommendedRetailPrice, opt => opt.MapFrom(src => src.ProductVariant.RecommendedRetailPrice))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<PaymentMethod, PaymentMethodDto>().ReverseMap();
        CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
    }


}
