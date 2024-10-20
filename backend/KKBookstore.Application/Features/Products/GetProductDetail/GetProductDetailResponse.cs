﻿using KKBookstore.Application.Common.Models;
using KKBookstore.Application.Features.Products.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace KKBookstore.Application.Features.Products.GetProductDetail;

public record GetProductDetailResponse : BaseDto
{
    public string Name { get; set; }
    public decimal MinUnitPrice { get; set; }
    public decimal MaxUnitPrice { get; set; }
    public decimal MinRecommendedRetailPrice { get; set; }
    public decimal MaxRecommendedRetailPrice { get; set; }
    public string UnitMeasureName { get; set; }
    public string Description { get; set; }
    public int ProductTypeId { get; set; }
    public string ProductTypeName { get; set; }
    public bool IsBook { get; set; }
    public IEnumerable<string> ThumbnailImageUrls { get; set; } = [];
    public IEnumerable<string> LargeImageUrls { get; set; } = [];
    public IEnumerable<ProductTypeAttribute> productTypeAttributes { get; set; }
    public IEnumerable<AuthorDto>? Authors { get; set; }
    public IEnumerable<ProductVariantDto> ProductVariants { get; set; } = [];

    // list of options that each variant can have
    public IEnumerable<ProductVariantOption> ProductVariantOptions { get; set; } = [];

    // other properties as needed
    public sealed class ProductTypeAttribute
    {
        public int ProductTypeId { get; set; }
        public int AttributeId { get; set; }
        public int AttributeValueId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public sealed class ProductVariantOption
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IEnumerable<string> Values { get; set; } = [];
        public IEnumerable<string>? ThumbnailImageUrls { get; set; }
        public IEnumerable<string>? LargeImageUrls { get; set; }


    }
}
