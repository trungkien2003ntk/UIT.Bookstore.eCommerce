using AutoMapper;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Users.Commands.Register;
using KKBookstore.Application.Users.Commands.ReplaceUser;
using KKBookstore.Application.Users.Commands.UpdateUser;
using KKBookstore.Application.Users.Queries.Models;
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

        CreateMap<PaginatedResult<User>, PaginatedResult<UserDto>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<User, RegisterResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<UserStatus>(src.Status)));

        CreateMap<UpdateUserCommand, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<ReplaceUserCommand, User>();
    }
}
