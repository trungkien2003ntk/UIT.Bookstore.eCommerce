using KKBookstore.Common.Interfaces;
using KKBookstore.Common.Models.ResultDtos;
using KKBookstore.Extensions;
using KKBookstore.Features.Orders.Models;
using KKBookstore.Models;
using KKBookstore.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KKBookstore.Features.Orders.GetOrderList;

public record GetOrderListQuery : IRequest<Result<PagedResult<OrderGeneralInformation>>>
{
    public string SortBy { get; init; } = "CreationTime";
    public string SortDirection { get; init; } = "desc";
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 12;
    public List<string> OrderStatuses { get; init; } = [];
    public string SearchQuery { get; init; } = string.Empty;

    // Date Range Filtering
    public DateTimeOffset? FromDate { get; init; }
    public DateTimeOffset? ToDate { get; init; }

    // Customer Filtering
    public int? CustomerId { get; init; }

    // Price Range Filtering
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
}

public class GetOrderListHandler(
    IApplicationDbContext dbContext
) : IRequestHandler<GetOrderListQuery, Result<PagedResult<OrderGeneralInformation>>>
{
    public async Task<Result<PagedResult<OrderGeneralInformation>>> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
    {        // Create base query with necessary includes for projection
        var baseQuery = dbContext.Orders
            //.Include(o => o.Customer) // Customer information
            //.Include(o => o.ShippingAddress) // Shipping address information
            //.Include(o => o.DeliveryMethod) // Delivery method name
            //.Include(o => o.PaymentMethod) // Payment method name
            //.Include(o => o.OrderLines) // Order lines for projection
            //    .ThenInclude(ol => ol.ProductVariant) // Product variant information
            //        .ThenInclude(pv => pv!.Product) // Product information
            //.Include(o => o.OrderLines)
            //    .ThenInclude(ol => ol.ProductVariant)
            //        .ThenInclude(pv => pv!.Product)
            //            .ThenInclude(p => p!.ProductType) // Product type name
            //.Include(o => o.OrderLines)
            //    .ThenInclude(ol => ol.ProductVariant)
            //        .ThenInclude(pv => pv!.Product)
            //            .ThenInclude(p => p!.ProductImages) // Product images
            //.Include(o => o.OrderLines)
            //    .ThenInclude(ol => ol.DiscountVoucher) // Discount voucher
            .AsSplitQuery()
            .AsNoTracking();

        // Apply all filters first
        baseQuery = ApplyOrderStatusFilter(baseQuery, request);
        baseQuery = ApplySearchFilter(baseQuery, request);
        baseQuery = ApplyDateRangeFilter(baseQuery, request);
        baseQuery = ApplyCustomerFilter(baseQuery, request);
        baseQuery = ApplyPriceRangeFilter(baseQuery, request);

        // Count total items for pagination
        var totalItems = await baseQuery.CountAsync(cancellationToken);

        // Apply sorting and pagination
        var sortProperty = request.SortBy;
        var validSortProperties = new List<string> { nameof(Order.CreationTime), nameof(Order.Id), nameof(Order.Status) };

        try
        {
            // Apply sorting
            IQueryable<Order> sortedQuery = sortProperty.ToLower() switch
            {
                "creationtime" => request.SortDirection.ToLower() == "desc"
                    ? baseQuery.OrderByDescending(o => o.CreationTime)
                    : baseQuery.OrderBy(o => o.CreationTime),
                "id" => request.SortDirection.ToLower() == "desc"
                    ? baseQuery.OrderByDescending(o => o.Id)
                    : baseQuery.OrderBy(o => o.Id),
                "status" => request.SortDirection.ToLower() == "desc"
                    ? baseQuery.OrderByDescending(o => o.Status)
                    : baseQuery.OrderBy(o => o.Status),
                _ => baseQuery.OrderByDescending(o => o.CreationTime)
            };            // First get the paginated order IDs to load variant options for
            var paginatedOrderIds = await sortedQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(o => o.Id)
                .ToListAsync(cancellationToken);

            // Load variant options for all product variants in these orders
            var variantOptionsLookup = await LoadProductVariantOptionsLookupAsync(paginatedOrderIds, cancellationToken);

            // Apply pagination and projection with variant options data
            var projectedOrders = await sortedQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(o => o.OrderLines)
                .Include(o => o.PriceDiscountVoucher)
                .Include(o => o.ShippingDiscountVoucher)
                .Select(o => new OrderGeneralInformation
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    DueWhen = o.DueWhen,
                    ExpectedDeliveryWhen = o.ExpectedDeliveryWhen,
                    Subtotal = o.Subtotal,
                    Total = o.CalculateTotal(),
                    TaxRate = o.TaxRate,
                    Comment = o.Comment,
                    DeliveryInstruction = o.DeliveryInstruction,
                    CustomerId = o.CustomerId,
                    ShippingAddressId = o.ShippingAddressId ?? 0,
                    DeliveryMethodName = o.DeliveryMethodId.HasValue ? o.DeliveryMethod.Name : "N/A",
                    DiscountVoucherId = o.PriceDiscountVoucherId,
                    ShippingVoucherId = o.ShippingDiscountVoucherId,
                    PaymentMethodName = o.PaymentMethodId.HasValue ? o.PaymentMethod.Name : "N/A",
                    Status = o.Status.ToString(),
                    PickingCompletedWhen = o.PickingCompletedWhen,
                    ConfirmedDeliveryWhen = o.ConfirmedDeliveryWhen,
                    ConfirmedReceivedWhen = o.ConfirmedReceivedWhen,
                    OrderWhen = o.OrderWhen,

                    // Customer Information
                    CustomerFullName = (o.Customer.FullName ?? $"{o.Customer.FirstName} {o.Customer.LastName}"),
                    CustomerEmail = (o.Customer.Email ?? ""),
                    CustomerPhoneNumber = (o.Customer.PhoneNumber ?? ""),
                    CustomerAvartarUrl = (o.Customer.ImageUrl ?? ""),

                    // Shipping Address Information
                    ShippingReceiverName = o.ShippingAddressId.HasValue ? o.ShippingAddress.ReceiverName : "",
                    ShippingPhoneNumber = o.ShippingAddressId.HasValue ? o.ShippingAddress.PhoneNumber : "",
                    ShippingDetailedAddress = o.ShippingAddressId.HasValue ? o.ShippingAddress.DetailAddress : "",
                    ShippingProvinceName = o.ShippingAddressId.HasValue ? o.ShippingAddress.ProvinceName : "",
                    ShippingDistrictName = o.ShippingAddressId.HasValue ? o.ShippingAddress.DistrictName : "",
                    ShippingCommuneName = o.ShippingAddressId.HasValue ? o.ShippingAddress.CommuneName : "",

                    // Optimized Order Lines - Load images in single query
                    OrderLines = o.OrderLines.Select(ol => new OrderLineDto
                    {
                        Id = ol.Id,
                        OrderId = ol.OrderId,
                        ProductId = ol.ProductVariantId.HasValue ? ol.ProductVariant.ProductId : 0,
                        ProductVariantId = ol.ProductVariantId,
                        ProductName = ol.ProductVariantId.HasValue && ol.ProductVariant.Product != null ? ol.ProductVariant.Product.Name : "Unknown Product",
                        ProductDescription = ol.ProductVariantId.HasValue && ol.ProductVariant.Product != null ? ol.ProductVariant.Product.Description : null,
                        ProductTypeName = ol.ProductVariantId.HasValue && ol.ProductVariant.Product != null && ol.ProductVariant.Product.ProductType != null ? ol.ProductVariant.Product.ProductType.DisplayName : "Unknown Type",
                        ProductVariantName = ol.ProductVariantId.HasValue ? ol.ProductVariant.VariantName : null,
                        UnitPrice = ol.UnitPrice,
                        RecommendedRetailPrice = ol.ProductVariantId.HasValue ? ol.ProductVariant.RecommendedRetailPrice : null,
                        Quantity = ol.Quantity,
                        DiscountAmount = ol.DiscountVoucher != null ? ol.DiscountVoucher.GetDiscountValue(ol.UnitPrice * ol.Quantity) : 0,
                        Rated = ol.ProductVariantId.HasValue ? ol.ProductVariant.Ratings.Any(r => r.CustomerId == o.CustomerId) : false,
                        // Get first image directly in projection to avoid separate queries
                        ThumbnailUrl = ol.ProductVariant.Product.ProductImages
                            .OrderBy(pi => pi.Id) // Ensure consistent ordering
                            .Select(pi => pi.ThumbnailImageUrl)
                            .FirstOrDefault() ?? "/images/placeholder.jpg",
                        LargeImageUrl = "",
                        //LargeImageUrl = ol.ProductVariant.Product.ProductImages
                        //    .OrderBy(pi => pi.Id)
                        //    .Select(pi => pi.LargeImageUrl)
                        //    .FirstOrDefault(),

                        // Load variant options efficiently
                        VariantOptions = ol.ProductVariantId.HasValue && variantOptionsLookup.ContainsKey(ol.ProductVariantId.Value)
                            ? variantOptionsLookup[ol.ProductVariantId.Value]
                            : new List<OrderLineDto.ProductOptionDto>(),
                        ProductAttributes = new List<OrderLineDto.ProductAttributeDto>()
                    }).ToList()
                })
            .ToListAsync(cancellationToken);

            var pagedResult = new PagedResult<OrderGeneralInformation>(
                projectedOrders,
                totalItems,
                request.PageSize,
                request.PageNumber
            );

            return Result.Success(pagedResult);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return Result.Failure<PagedResult<OrderGeneralInformation>>(Error.InvalidSortProperty(sortProperty, string.Join(',', validSortProperties)));
        }
    }
    private static IQueryable<Order> ApplyOrderStatusFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (request.OrderStatuses.Any())
        {
            var orderStatusEnums = request.OrderStatuses
                .Select(status => Enum.Parse<OrderStatus>(status))
                .ToList();

            query = query.Where(o => orderStatusEnums.Contains(o.Status));
        }

        return query;
    }
    private static IQueryable<Order> ApplySearchFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            var searchQuery = request.SearchQuery.Trim().ToLower();

            query = query.Where(o =>
                // Search by order number
                o.OrderNumber.ToLower().Contains(searchQuery) ||
                // Search by customer name
                (o.Customer != null && (o.Customer.FirstName + " " + o.Customer.LastName).ToLower().Contains(searchQuery)) ||
                // Search by customer email
                (o.Customer != null && o.Customer.Email != null && o.Customer.Email.ToLower().Contains(searchQuery))
            );
        }

        return query;
    }

    private static IQueryable<Order> ApplyDateRangeFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (request.FromDate.HasValue)
        {
            query = query.Where(o => o.CreationTime >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            // Add one day to include the entire end date
            var endDate = request.ToDate.Value.Date.AddDays(1);
            query = query.Where(o => o.CreationTime < endDate);
        }

        return query;
    }

    private static IQueryable<Order> ApplyCustomerFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (request.CustomerId.HasValue)
        {
            query = query.Where(o => o.CustomerId == request.CustomerId.Value);
        }

        return query;
    }

    private static IQueryable<Order> ApplyPriceRangeFilter(IQueryable<Order> query, GetOrderListQuery request)
    {
        if (request.MinPrice.HasValue)
        {
            query = query.Where(o => o.CalculateTotal() >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(o => o.CalculateTotal() <= request.MaxPrice.Value);
        }

        return query;
    }    /// <summary>
         /// Loads ProductVariantOptionValues lookup to avoid complex include issues
         /// </summary>
    private async Task<Dictionary<int, List<OrderLineDto.ProductOptionDto>>> LoadProductVariantOptionsLookupAsync(List<int> orderIds, CancellationToken cancellationToken)
    {
        // Get all ProductVariantIds from the specified orders
        var productVariantIds = await dbContext.OrderLines
            .Where(ol => orderIds.Contains(ol.OrderId) && ol.ProductVariantId.HasValue)
            .Select(ol => ol.ProductVariantId!.Value)
            .Distinct()
            .ToListAsync(cancellationToken);

        if (!productVariantIds.Any())
            return new Dictionary<int, List<OrderLineDto.ProductOptionDto>>();

        // Load ProductVariantOptionValues in a separate query
        var variantOptionsLookup = await dbContext.ProductVariants
            .Where(pv => productVariantIds.Contains(pv.Id))
            .Select(pv => new
            {
                ProductVariantId = pv.Id,
                Options = pv.ProductVariantOptionValues!.Select(pvov => new OrderLineDto.ProductOptionDto
                {
                    OptionName = pvov.Option != null ? pvov.Option.Name : "Unknown Option",
                    OptionValue = pvov.OptionValue != null ? pvov.OptionValue.Value : "Unknown Value"
                }).ToList()
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        // Create and return lookup dictionary for efficient access
        return variantOptionsLookup.ToDictionary(x => x.ProductVariantId, x => x.Options);
    }


}
