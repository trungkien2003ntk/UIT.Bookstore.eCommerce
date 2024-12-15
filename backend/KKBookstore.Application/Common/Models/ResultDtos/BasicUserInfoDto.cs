namespace KKBookstore.Application.Common.Models.ResultDtos;

public record BasicUserInfoDto(int UserId, string ImageUrl, string FullName, string Email, string RoleName)
{
    public int UserId { get; init; } = UserId;
    public string ImageUrl { get; init; } = ImageUrl;
    public string FullName { get; init; } = FullName;
    public string Email { get; init; } = Email;
    public string RoleName { get; init; } = RoleName;
}