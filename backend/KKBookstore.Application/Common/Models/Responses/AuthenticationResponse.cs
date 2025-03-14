﻿using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Common.Models.Responses;

public class AuthenticationResponse(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken, BasicUserInfoDto BasicUserInfo)
{
    public string AccessToken { get; init; } = AccessToken;
    public DateTime AccessTokenExpiration { get; init; } = AccessTokenExpiration;
    public string RefreshToken { get; init; } = RefreshToken;
    public BasicUserInfoDto BasicUserInfo { get; init; } = BasicUserInfo;
}
