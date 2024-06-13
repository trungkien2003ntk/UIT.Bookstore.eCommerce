using KKBookstore.Domain.Models;

namespace KKBookstore.Infrastructure.Shipping;

public static class ShippingServiceErrors
{
    public static readonly Error DistrictNotFound = Error.Failure("ShippingService.DistrictNotFound", "District not found");
    public static readonly Error CommuneNotFound = Error.Failure("ShippingService.CommuneNotFound", "Commune not found");
    public static readonly Error ProvinceNotFound = Error.Failure("ShippingService.ProvinceNotFound", "Province not found");
    public static readonly Error InvalidDistrictName = Error.Failure("ShippingService.InvalidDistrictName", "Invalid district name");
    public static readonly Error InvalidCommuneName = Error.Failure("ShippingService.InvalidCommuneName", "Invalid commune name");
    public static readonly Error InvalidProvinceName = Error.Failure("ShippingService.InvalidProvinceName", "Invalid province name");

    public static readonly Error Unknown = Error.Failure("ShippingService.Unknown", "An unknown error occurred");

    public static Error RequestFailed(string message) => Error.Failure("ShippingService.RequestFailed", message);
}
