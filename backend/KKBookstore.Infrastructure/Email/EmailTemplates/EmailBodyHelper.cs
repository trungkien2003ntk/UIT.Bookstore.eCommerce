using KKBookstore.Application.Extensions;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using System.Text;

namespace KKBookstore.Infrastructure.Email.EmailTemplates;

internal static class EmailBodyHelper
{
    private static readonly string _orderConfirmationTemplate = "../KKBookstore.Infrastructure/Email/EmailTemplates/OrderConfirmationTemplate.html";
    private static readonly string _orderItemTemplate = "../KKBookstore.Infrastructure/Email/EmailTemplates/OrderItemTemplate.html";
    private static readonly string _deliveryTimePlaceholder = "{{DeliveryTime}}";
    private static readonly string _orderTimePlaceholder = "{{OrderTime}}";
    private static readonly string _orderItemImageSourcePlaceholder = "{{OrderItemImageSource}}";
    private static readonly string _orderItemNamePlaceholder = "{{OrderItemName}}";
    private static readonly string _orderItemPricePlaceholder = "{{OrderItemPrice}}";
    private static readonly string _subtotalPricePlaceholder = "{{SubtotalPrice}}";
    private static readonly string _shippingFeePlaceholder = "{{ShippingFee}}";
    private static readonly string _voucherDiscountPricePlaceholder = "{{VoucherDiscountPrice}}";
    private static readonly string _totalPricePlaceholder = "{{TotalPrice}}";
    private static readonly string _orderItemCellPlaceholder = "{{OrderItemCell}}";


    public static string BuildOrderConfirmationEmailBody(Order orderWithItemsWithSkuWithProductWithProductImagesAndBothVouchers)
    {
        var order = orderWithItemsWithSkuWithProductWithProductImagesAndBothVouchers;

        var orderConfirmationTemplate = File.ReadAllText(_orderConfirmationTemplate);
        var orderItemTemplate = File.ReadAllText(_orderItemTemplate);
        var orderItemCellContent = new StringBuilder();

        orderItemCellContent.Append(string.Join("\n", order.OrderLines.Select(ol =>
            {
                var orderItemName = ol.Sku.Product.Name;
                var unitPriceToString = $"{ol.UnitPrice:#,##0}₫";
                var orderItemImageUrl = ol.Sku.GetThumbnailImageUrl();
                if (string.IsNullOrEmpty(orderItemImageUrl))
                {
                    orderItemImageUrl = ol.Sku.Product.GetFirstThumbnailImageUrl();
                }

                var newOrderItemTemplate = orderItemTemplate
                    .Replace(_orderItemImageSourcePlaceholder, orderItemImageUrl)
                    .Replace(_orderItemNamePlaceholder, orderItemName)
                    .Replace(_orderItemPricePlaceholder, unitPriceToString);

                return newOrderItemTemplate;
            })
            .ToList()
        ));
            

        var orderConfirmationEmailBody = orderConfirmationTemplate
            .Replace(_deliveryTimePlaceholder, order.ExpectedDeliveryWhen.ToGmtPlus7FormattedString())
            .Replace(_orderTimePlaceholder, order.OrderWhen.ToGmtPlus7FormattedString())
            .Replace(_orderItemCellPlaceholder, orderItemCellContent.ToString())
            .Replace(_subtotalPricePlaceholder, order.Subtotal.ToFormattedVietnamesePrice())
            .Replace(_shippingFeePlaceholder, order.ShippingFee.ToFormattedVietnamesePrice())
            .Replace(_voucherDiscountPricePlaceholder, order.PriceDiscountVoucher?.GetDiscountValue(order.Subtotal).ToFormattedVietnamesePrice() ?? "0")
            .Replace(_totalPricePlaceholder, order.CalculateTotal().ToFormattedVietnamesePrice());

        return orderConfirmationEmailBody;
        
        
    }
}
