using AutoMapper;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Customers;
using KKBookstore.Features.Orders.GetOrderDetail;
using KKBookstore.Features.Orders.GetOrderList;
using KKBookstore.Features.Orders.Models;
using KKBookstore.Mappings.Helpers;
using KKBookstore.Orders;

namespace KKBookstore.Mappings;

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
            .ForMember(dest => dest.DetailedFullAddress, opt => opt.MapFrom(src => $"{src.DetailAddress}, {src.CommuneName}, {src.DistrictName}, {src.ProvinceName}"));

        CreateMap<Order, OrderGeneralInformation>()
            .ForMember(dest => dest.DeliveryMethodName, opt => opt.MapFrom(src => src.DeliveryMethod != null ? src.DeliveryMethod.Name : string.Empty))
            .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod != null ? src.PaymentMethod.Name : string.Empty))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.CalculateTotal()))
            .ForMember(dest => dest.ShippingVoucherId, opt => opt.MapFrom(src => src.ShippingDiscountVoucherId))
            .ForMember(dest => dest.OrderLines, opt => opt.MapFrom(src => src.OrderLines))
            // Customer Information
            .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}".Trim() : string.Empty))
            .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.Email ?? string.Empty : string.Empty))
            .ForMember(dest => dest.CustomerPhoneNumber, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.PhoneNumber ?? string.Empty : string.Empty))
            .ForMember(dest => dest.CustomerAvartarUrl, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.ImageUrl ?? string.Empty : string.Empty))
            // Shipping Address Information
            .ForMember(dest => dest.ShippingReceiverName, opt => opt.MapFrom(src => src.ShippingAddress != null ? src.ShippingAddress.ReceiverName : string.Empty))
            .ForMember(dest => dest.ShippingPhoneNumber, opt => opt.MapFrom(src => src.ShippingAddress != null ? src.ShippingAddress.PhoneNumber : string.Empty))
            .ForMember(dest => dest.ShippingDetailedAddress, opt => opt.MapFrom(src => src.ShippingAddress != null ? src.ShippingAddress.DetailAddress : string.Empty))
            .ForMember(dest => dest.ShippingProvinceName, opt => opt.MapFrom(src => src.ShippingAddress != null ? src.ShippingAddress.ProvinceName : string.Empty))
            .ForMember(dest => dest.ShippingDistrictName, opt => opt.MapFrom(src => src.ShippingAddress != null ? src.ShippingAddress.DistrictName : string.Empty))
            .ForMember(dest => dest.ShippingCommuneName, opt => opt.MapFrom(src => src.ShippingAddress != null ? src.ShippingAddress.CommuneName : string.Empty));        CreateMap<OrderLine, OrderLineDto>()
            .ForMember(dest => dest.ProductVariantId, opt => opt.MapFrom(src => src.ProductVariantId))
            .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => MappingHelpers.GetProductThumbnailImageUrl(src.ProductVariant.Product)))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductVariant.Product.Name))
            .ForMember(dest => dest.RecommendedRetailPrice, opt => opt.MapFrom(src => src.ProductVariant.RecommendedRetailPrice))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

        CreateMap<PaymentMethod, PaymentMethodDto>().ReverseMap();
        CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
    }


}
