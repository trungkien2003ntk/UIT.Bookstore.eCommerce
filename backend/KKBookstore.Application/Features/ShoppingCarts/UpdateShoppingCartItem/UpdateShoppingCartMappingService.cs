using KKBookstore.Application.Common.Constants;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Models;
using KKBookstore.Domain.Products;
using KKBookstore.Domain.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using static KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem.UpdateShoppingCartResponse;
using static KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem.UpdateShoppingCartResponse.DiscountDetailDto;
using static KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem.UpdateShoppingCartResponse.ShoppingCartItemDto;

namespace KKBookstore.Application.Features.ShoppingCarts.UpdateShoppingCartItem;

public class UpdateShoppingCartMappingService(
    IApplicationDbContext _dbContext
) : IUpdateShoppingCartMappingService
{
    public async Task<Result<UpdateShoppingCartResponse>> MapToResponse(ShoppingCart shoppingCart, decimal discountFromVoucherAmount)
    {
        List<Product> neededProducts = await ExtractDistinctProductInCart(shoppingCart);

        ICollection<DiscountForReason> breakdown = [
            new()
            {
                DiscountName = DiscountConstant.ProductDiscountLabel,
                DiscountValue = shoppingCart.TotalSavedAmount
            }
        ];

        if (discountFromVoucherAmount > 0)
        {
            breakdown.Add(new()
            {
                DiscountName = DiscountConstant.VoucherDiscountLabel,
                DiscountValue = discountFromVoucherAmount
            });
        }


        var response = new UpdateShoppingCartResponse()
        {
            TotalPrice = shoppingCart.TotalUnitPrice,

            UpdatedItems = shoppingCart.Items.Select(ci =>
            {
                var product = neededProducts.First(p => p.Id == ci.ProductVariant.ProductId);
                return MapToShoppingCartItemDto(ci, product);
            }).ToList(),

            DiscountDetail = new()
            {
                Subtotal = shoppingCart.TotalRecommendedRetailPrice,
                TotalSaved = shoppingCart.TotalSavedAmount,
                Total = shoppingCart.TotalUnitPrice,
                Breakdown = breakdown
            }
        };

        return response;
    }

    private async Task<List<Product>> ExtractDistinctProductInCart(ShoppingCart shoppingCart)
    {
        // todo: use projection to reduce the amount of data fetched, increase performance
        var productIds = shoppingCart.Items.Select(ci => ci.ProductVariant.ProductId).Distinct().ToList();
        var neededProducts = await _dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .Include(p => p.Options)                        // these are for 
                .ThenInclude(o => o.OptionValues)           // sku variations
            .Include(p => p.ProductVariants)
                .ThenInclude(s => s.ProductVariantOptionValues)        // these are for
                    .ThenInclude(sov => sov.OptionValue)    // sku thumbnail image
                        .ThenInclude(ov => ov.Option)       // this is for SkuInCart option names
            .Include(p => p.ProductImages)
            .ToListAsync();
        return neededProducts;
    }

    private ShoppingCartItemDto MapToShoppingCartItemDto(ShoppingCartItem ci, Product product)
    {
        var allProductOptionsWithValues = product.GetAllOptionsWithValues();
        var productOptionAttributeDtos = allProductOptionsWithValues
                .Select(opt => MapToProductOptionAttributeDto(opt))
                .ToList();

        return new ShoppingCartItemDto()
        {
            Id = ci.Id,
            ProductId = ci.ProductVariant.ProductId,
            ProductVariantId = ci.ProductVariantId,
            IsSelected = ci.IsSelected,
            ProductVariantName = ci.ProductVariant.VariantName,
            ProductName = product.Name,
            ProductTypeId = product.ProductTypeId,
            UnitPrice = ci.ProductVariant.UnitPrice,
            RecommendedRetailPrice = ci.ProductVariant.RecommendedRetailPrice,
            Quantity = ci.Quantity,
            AvailableQuantity = ci.ProductVariant.Quantity,
            TotalQuantity = ci.ProductVariant.Quantity,
            ImageUrl = ci.ProductVariant.GetThumbnailImageUrl() ?? product.GetFirstThumbnailImageUrl(),
            Description = product.Description,
            CreationTime = ci.CreationTime,
            ProductVariantVariations = product.ProductVariants
                .Select(MapToProductVariantForCartDto)
                .Select(sv => sv.PopulateIndex(productOptionAttributeDtos))
                .ToList(),
            ProductOptions = productOptionAttributeDtos
        };
    }

    private ProductVariantForCartDto MapToProductVariantForCartDto(ProductVariant productVariant)
    {
        return new ProductVariantForCartDto()
        {
            Id = productVariant.Id,
            ProductVariantName = productVariant.VariantName,
            UnitPrice = productVariant.UnitPrice,
            RecommendedRetailPrice = productVariant.RecommendedRetailPrice,
            BasicDiscountRate = productVariant.BasicDiscountRate,
            AvailableQuantity = productVariant.AvailableQuantity,
            TotalQuantity = productVariant.Quantity,
            ProductId = productVariant.ProductId,
            SkuValue = productVariant.SkuValue.Value,
            OptionNames = productVariant.ProductVariantOptionValues.Select(sov => sov.OptionValue.Option.Name).ToList()
        };
    }

    private ProductOptionAttributeDto MapToProductOptionAttributeDto(KeyValuePair<string, IEnumerable<ProductOptionValue>> opt)
    {
        return new ProductOptionAttributeDto()
        {
            Name = opt.Key,
            Values = opt.Value.Select(pov => pov.Value),
            Images = opt.Value.Select(pov => pov.ThumbnailImageUrl)
        };
    }
}
