# GetRatingListQuery Custom Sorting Implementation

## Summary

Successfully modified `GetRatingListQuery.cs` to implement custom property sorting following the pattern used in other handlers for custom sorting that goes beyond simple entity properties.

## Changes Made

### 1. Replaced SortAndPaginateAsync with Custom Implementation

-   Removed the generic `SortAndPaginateAsync` extension method call
-   Implemented custom sorting logic with manual pagination
-   Added proper total count calculation before pagination

### 2. Custom ApplySorting Method

-   Created `ApplySorting` method that handles both entity properties and custom properties
-   Uses switch expressions to map sort properties to appropriate LINQ OrderBy expressions
-   Supports both ascending and descending sort directions

### 3. Updated Valid Sort Properties

**Removed:**

-   `LikesCount` (no longer supported)

**Added:**

-   `ProductId` - Custom property that sorts by `Rating.ProductVariant.Product.Id`
-   `Status` - Direct Rating entity property for `Rating.Status` (RatingStatus enum)

**Retained:**

-   `CreationTime` - Direct Rating entity property
-   `RatingValue` - Direct Rating entity property

### 4. Validation and Error Handling

-   Added `ValidateSortProperty` method for proper validation
-   Defaults to `CreationTime` descending if invalid sort property is provided
-   Returns descriptive error messages for invalid sort properties

### 5. Custom Property Handling

The implementation properly handles:

-   **ProductId**: Sorts by `r.ProductVariant.Product.Id` (navigation through ProductVariant to Product)
-   **Status**: Sorts by `r.Status` (RatingStatus enum - Posted, PendingReview, Hidden)

## Technical Details

### Sort Property Mapping

```csharp
return sortBy switch
{
    "ProductId" => isAscending
        ? query.OrderBy(r => r.ProductVariant.Product.Id)
        : query.OrderByDescending(r => r.ProductVariant.Product.Id),

    nameof(Rating.Status) => isAscending
        ? query.OrderBy(r => r.Status)
        : query.OrderByDescending(r => r.Status),

    // ... other properties
};
```

### Database Relationships

-   `Rating` → `ProductVariant` (via ProductVariantId)
-   `ProductVariant` → `Product` (via ProductId)
-   `Rating.Status` → `RatingStatus` enum (Posted=1, PendingReview=2, Hidden=3)

## Verification

-   ✅ Code compiles successfully
-   ✅ Follows established patterns from GetProductListQuery
-   ✅ Maintains existing functionality for standard properties
-   ✅ Adds support for custom properties (ProductId, Status)
-   ✅ Removes deprecated LikesCount sorting
-   ✅ Includes proper validation and error handling

## Usage Examples

Valid sort parameters:

-   `sortBy=CreationTime&sortDirection=desc` (default)
-   `sortBy=RatingValue&sortDirection=asc`
-   `sortBy=Status&sortDirection=desc`
-   `sortBy=ProductId&sortDirection=asc`

Invalid sort parameters will default to `CreationTime` descending with validation error returned.
