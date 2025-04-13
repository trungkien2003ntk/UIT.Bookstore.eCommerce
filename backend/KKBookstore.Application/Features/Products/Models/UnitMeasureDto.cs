using KKBookstore.Common.Models.ResultDtos;

namespace KKBookstore.Features.Products.Models;

public sealed record UnitMeasureDto : BaseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
