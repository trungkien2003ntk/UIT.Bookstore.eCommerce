namespace KKBookstore.Application.Common.Models;

public record SearchRequest
{
    public string SearchText { get; init; } = "*";
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 12;
}
