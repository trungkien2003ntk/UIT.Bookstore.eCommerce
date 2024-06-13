using AutoMapper;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Features.Users.RefreshAccessToken;
using KKBookstore.Application.Features.Users.SignIn;

namespace KKBookstore.Application.Mappings;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<TokenResponse, SignInResponse>().ReverseMap();
        CreateMap<TokenResponse, RefreshAccessTokenResponse>().ReverseMap();
    }
}
