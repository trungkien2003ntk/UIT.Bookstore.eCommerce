using AutoMapper;
using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Features.Users.RefreshAccessToken;
using KKBookstore.Application.Features.Users.SignIn;
using KKBookstore.Application.Features.Users.VerifyOtp;

namespace KKBookstore.Application.Mappings;

public class TokenProfile : Profile
{
    public TokenProfile()
    {
        CreateMap<TokenResponse, SignInResponse>().ReverseMap();
        CreateMap<TokenResponse, VerifyOtpResponse>().ReverseMap();
        CreateMap<TokenResponse, RefreshAccessTokenResponse>().ReverseMap();
    }
}
