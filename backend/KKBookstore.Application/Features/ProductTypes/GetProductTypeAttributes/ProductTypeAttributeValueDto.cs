﻿using KKBookstore.Application.Common.Models;

namespace KKBookstore.Application.Features.ProductTypes.GetProductTypeAttributes;

public record ProductTypeAttributeValueDto : BaseDto
{
    public string Value { get; set; }
    public int ProductTypeAttributeId { get; set; }
}
