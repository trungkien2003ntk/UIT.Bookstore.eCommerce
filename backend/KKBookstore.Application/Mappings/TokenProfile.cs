using AutoMapper;
using KKBookstore.Common.Models.Responses;
using KKBookstore.Features.Users.RefreshAccessToken;
using KKBookstore.Features.Users.SignIn;

namespace KKBookstore.Mappings;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<AuthenticationResponse, SignInResponse>().ReverseMap();
        CreateMap<AuthenticationResponse, RefreshAccessTokenResponse>().ReverseMap();
    }
}
