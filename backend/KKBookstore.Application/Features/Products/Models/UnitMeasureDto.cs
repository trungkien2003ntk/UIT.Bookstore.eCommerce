using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Products.Models;

public sealed record UnitMeasureDto : BaseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
