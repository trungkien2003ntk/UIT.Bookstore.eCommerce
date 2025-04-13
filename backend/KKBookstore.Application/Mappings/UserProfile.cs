using AutoMapper;
using KKBookstore.Common.Models.Responses;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Customers;
using KKBookstore.Features.Users.AddShippingAddress;
using KKBookstore.Features.Users.GetUserList;
using KKBookstore.Features.Users.GetUserShippingAddresses;
using KKBookstore.Features.Users.Register;
using KKBookstore.Features.Users.UpdateShippingAddress;
using KKBookstore.Features.Users.UpdateUser;
using KKBookstore.Features.Users.UpdateUserPartial;
using KKBookstore.Users;

namespace KKBookstore.Mappings;
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

        CreateMap<UpdateUserPartialCommand, User>()
            .ForAllMembers(opts => opts.Condition((UpdateUserPartialCommand src, User dest, object srcMember) => srcMember != null));

        CreateMap<UpdateUserCommand, User>();

        CreateMap<ShippingAddress, GetUserShippingAddressesResponse>();

        CreateMap<ShippingAddress, AddShippingAddressCommand>().ReverseMap();

        CreateMap<AddShippingAddressResponse, ShippingAddress>().ReverseMap();
        CreateMap<UpdateShippingAddressResponse, ShippingAddress>().ReverseMap();
    }
}
