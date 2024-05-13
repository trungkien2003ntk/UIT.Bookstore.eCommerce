using AutoMapper;
using KKBookstore.Application.Users.Queries.GetUser;
using KKBookstore.Domain.Users;

namespace KKBookstore.Application.MappingProfiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<UserStatus>(src.Status)));


    }
}
