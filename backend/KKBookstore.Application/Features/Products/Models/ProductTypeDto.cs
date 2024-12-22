using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.Products.Models;

public sealed record ProductTypeDto : BaseDto
{
    public string ProductTypeCode { get; set; }
    public int Level { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public int? ParentProductTypeId { get; set; }
}
