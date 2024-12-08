namespace KKBookstore.Application.Common.Models;

public record BasicUserInfoDto(int UserId, string ImageUrl, string FullName, string RoleName)
{
    public int UserId { get; init; } = UserId;
    public string ImageUrl { get; init; } = ImageUrl;
    public string FullName { get; init; } = FullName;
    public string RoleName { get; init; } = RoleName;
}