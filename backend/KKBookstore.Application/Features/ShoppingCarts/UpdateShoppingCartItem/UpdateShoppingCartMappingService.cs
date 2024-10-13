using KKBookstore.Application.Common.Constants;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Domain.Aggregates.ProductAggregate;
using KKBookstore.Domain.Aggregates.ShoppingCartAggregate;
using KKBookstore.Domain.Models;
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
                var product = neededProducts.First(p => p.Id == ci.Sku.ProductId);
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
        var productIds = shoppingCart.Items.Select(ci => ci.Sku.ProductId).Distinct().ToList();
        var neededProducts = await _dbContext.Products
            .Where(p => productIds.Contains(p.Id))
            .Include(p => p.Options)                        // these are for 
                .ThenInclude(o => o.OptionValues)           // sku variations
            .Include(p => p.Skus)
                .ThenInclude(s => s.SkuOptionValues)        // these are for
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
            ProductId = ci.Sku.ProductId,
            SkuId = ci.SkuId,
            IsSelected = ci.IsSelected,
            SkuName = ci.Sku.SkuName,
            ProductName = product.Name,
            ProductTypeId = product.ProductTypeId,
            UnitPrice = ci.Sku.UnitPrice,
            RecommendedRetailPrice = ci.Sku.RecommendedRetailPrice,
            Quantity = ci.Quantity,
            AvailableQuantity = ci.Sku.Quantity,
            TotalQuantity = ci.Sku.Quantity,
            ImageUrl = ci.Sku.GetThumbnailImageUrl() ?? product.GetFirstThumbnailImageUrl(),
            Description = product.Description,
            CreatedWhen = ci.CreatedWhen,
            SkuVariations = product.Skus
                .Select(MapToSkuForCartDto)
                .Select(sv => sv.PopulateIndex(productOptionAttributeDtos))
                .ToList(),
            ProductOptions = productOptionAttributeDtos
        };
    }

    private SkuForCartDto MapToSkuForCartDto(Sku sku)
    {
        return new SkuForCartDto()
        {
            Id = sku.Id,
            SkuName = sku.SkuName,
            UnitPrice = sku.UnitPrice,
            RecommendedRetailPrice = sku.RecommendedRetailPrice,
            BasicDiscountRate = sku.BasicDiscountRate,
            AvailableQuantity = sku.AvailableQuantity,
            TotalQuantity = sku.Quantity,
            ProductId = sku.ProductId,
            Status = sku.Status.ToString(),
            SkuValue = sku.SkuValue.Value,
            OptionNames = sku.SkuOptionValues.Select(sov => sov.OptionValue.Option.Name).ToList()
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
