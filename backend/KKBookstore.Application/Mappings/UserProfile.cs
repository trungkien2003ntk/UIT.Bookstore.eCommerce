using AutoMapper;
using KKBookstore.Application.Common.Models.Responses;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Application.Features.Users.AddShippingAddress;
using KKBookstore.Application.Features.Users.GetUserList;
using KKBookstore.Application.Features.Users.GetUserShippingAddresses;
using KKBookstore.Application.Features.Users.Register;
using KKBookstore.Application.Features.Users.ReplaceUser;
using KKBookstore.Application.Features.Users.UpdateShippingAddress;
using KKBookstore.Application.Features.Users.UpdateUser;
using KKBookstore.Domain.Customers;
using KKBookstore.Domain.Orders;
using KKBookstore.Domain.Users;

namespace KKBookstore.Application.Mappings;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserListResponse>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<UserStatus>(src.Status)));

        CreateMap<PagedResult<User>, PagedResult<GetUserListResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<AuthenticationResponse, RegisterResponse>().ReverseMap();

        CreateMap<UpdateUserCommand, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ReplaceUserCommand, User>();

        CreateMap<ShippingAddress, GetUserShippingAddressesResponse>();

        CreateMap<ShippingAddress, AddShippingAddressCommand>().ReverseMap();

        CreateMap<AddShippingAddressResponse, ShippingAddress>().ReverseMap();
        CreateMap<UpdateShippingAddressResponse, ShippingAddress>().ReverseMap();
    }
}
