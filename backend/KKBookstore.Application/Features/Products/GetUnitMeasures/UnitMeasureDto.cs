using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Products.GetUnitMeasures;

public record UnitMeasureDto : BaseDto
{
    public string Name { get; set; }
    public string Description { get; set; }


    public UnitMeasureDto()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public UnitMeasureDto(string name, string description)
    {
        Name = name;
        Description = description;
    }
}