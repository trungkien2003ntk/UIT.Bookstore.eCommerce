using KKBookstore.Application.Extensions;
using KKBookstore.Domain.Aggregates.OrderAggregate;
using System.Reflection;
using System.Text;

namespace KKBookstore.Infrastructure.Email.EmailTemplates;

internal static class EmailBodyHelper
{
    //private static readonly string _orderConfirmationTemplate = "../KKBookstore.Infrastructure/Email/EmailTemplates/OrderConfirmationTemplate.html";
    //private static readonly string _orderItemTemplate = "../KKBookstore.Infrastructure/Email/EmailTemplates/OrderItemTemplate.html";
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

        string orderConfirmationTemplate;
        var assembly = Assembly.GetExecutingAssembly();
        using (var stream = assembly.GetManifestResourceStream("KKBookstore.Infrastructure.Email.EmailTemplates.OrderConfirmationTemplate.html"))
        using (var reader = new StreamReader(stream))
        {
            orderConfirmationTemplate = reader.ReadToEnd();
        }

        string orderItemTemplate;
        using (var stream = assembly.GetManifestResourceStream("KKBookstore.Infrastructure.Email.EmailTemplates.OrderItemTemplate.html"))
        using (var reader = new StreamReader(stream))
        {
            orderItemTemplate = reader.ReadToEnd();
        }


        //var orderConfirmationTemplate = File.ReadAllText(_orderConfirmationTemplate);
        //var  = File.ReadAllText(_orderItemTemplate);
        var orderItemCellContent = new StringBuilder();

        orderItemCellContent.Append(string.Join("\n", order.OrderLines.Select(ol =>
            {
                var orderItemName = ol.ProductVariant.Product.Name;
                var unitPriceToString = $"{ol.UnitPrice:#,##0}₫";
                var orderItemImageUrl = ol.ProductVariant.GetThumbnailImageUrl();
                if (string.IsNullOrEmpty(orderItemImageUrl))
                {
                    orderItemImageUrl = ol.ProductVariant.Product.GetFirstThumbnailImageUrl();
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
