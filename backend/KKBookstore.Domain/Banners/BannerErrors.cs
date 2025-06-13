using KKBookstore.Models;

namespace KKBookstore.Banners;

public static class BannerErrors
{
    public static readonly Error TitleRequired = Error.Validation(
        "Banner.TitleRequired",
        "Banner title is required."
    );

    public static readonly Error ImageUrlRequired = Error.Validation(
        "Banner.ImageUrlRequired",
        "Banner image URL is required."
    ); public static readonly Error BannerNotFound = Error.NotFound(
        "Banner.NotFound",
        "Banner not found."
    );

    public static readonly Error NotFound = Error.NotFound(
        "Banner.NotFound",
        "Banner not found."
    );

    public static readonly Error BannerAlreadyActive = Error.BusinessRuleViolation(
        "Banner.AlreadyActive",
        "Banner is already active."
    );

    public static readonly Error BannerAlreadyInactive = Error.BusinessRuleViolation(
        "Banner.AlreadyInactive",
        "Banner is already inactive."
    );

    public static readonly Error InvalidProductType = Error.NotFound(
        "Banner.InvalidProductType",
        "Invalid product type specified."
    );
}
