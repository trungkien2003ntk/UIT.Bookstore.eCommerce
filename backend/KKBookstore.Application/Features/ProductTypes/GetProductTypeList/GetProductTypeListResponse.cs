using KKBookstore.Application.Common.Models.ResultDtos;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeList;

public record GetProductTypeListResponse
    {
        public List<ProductTypeGeneralDto> ListItem { get; init; }

    public record ProductTypeGeneralDto : BaseDto
    {
        public string ProductTypeCode { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public int Level { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int? ParentProductTypeId { get; set; }
        public List<ProductTypeGeneralDto> ChildProductTypes { get; set; }
    }

}
