
using KKBookstore.Domain.Models;

namespace KKBookstore.Domain.Products;

public static class UnitMeasureErrors
{
    // not found
    public static readonly Error NotFound = Error.NotFound("UnitMeasure.NotFound", "Unit measure was not found.");
}