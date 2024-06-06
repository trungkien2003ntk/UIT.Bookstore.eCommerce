namespace KKBookstore.Application.Features.Locations.GetCommuneList;

public record GetCommuneListResponse
{
    public List<CommuneDto> Items { get; init; } = [];

    public sealed record CommuneDto
    {
        public string Code { get; init; }
        public string Name { get; init; }
    }
}