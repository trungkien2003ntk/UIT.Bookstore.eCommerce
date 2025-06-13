# DiscountVoucher API Endpoints

This document describes the API endpoints for managing discount vouchers in the KK Bookstore system.

## Overview

The DiscountVoucher API provides comprehensive functionality for managing discount vouchers, including:

-   List vouchers with filtering and pagination
-   Get voucher details
-   Create new vouchers (with validation rules)
-   Perform status actions (Start, Pause, Cancel)
-   Delete vouchers (with business rules)

## Base URL

```
/api/discount-vouchers
```

## Endpoints

### 1. Get Discount Vouchers List

**GET** `/api/discount-vouchers`

Returns a paginated list of discount vouchers with optional filtering.

#### Query Parameters

-   `PageNumber` (int, default: 1): Page number for pagination
-   `PageSize` (int, default: 10): Number of items per page
-   `SortBy` (string, default: "CreationTime"): Field to sort by
-   `SortDirection` (string, default: "desc"): Sort direction ("asc" or "desc")
-   `SearchQuery` (string, optional): Search in name, code, and description
-   `Status` (DiscountStatus, optional): Filter by status (Draft, Active, Paused, Expired, Cancelled)
-   `VoucherType` (DiscountVoucherType, optional): Filter by voucher type (Order, Shipping)
-   `ValueType` (DiscountValueType, optional): Filter by value type (Fixed, Percentage)
-   `ApplyToProductTypeId` (int, optional): Filter by product type
-   `StartTimeFrom` (DateTimeOffset, optional): Filter vouchers starting from this date
-   `StartTimeTo` (DateTimeOffset, optional): Filter vouchers starting before this date
-   `EndTimeFrom` (DateTimeOffset, optional): Filter vouchers ending from this date
-   `EndTimeTo` (DateTimeOffset, optional): Filter vouchers ending before this date
-   `MinValue` (decimal, optional): Filter by minimum value
-   `MaxValue` (decimal, optional): Filter by maximum value

#### Response

```json
{
	"items": [
		{
			"id": 1,
			"name": "Giảm giá 15%",
			"code": "SAVE15",
			"description": "Save 15% on all orders",
			"valueType": "Percentage",
			"voucherType": "Order",
			"status": "Active",
			"value": 0.15,
			"maximumDiscountValue": 50000,
			"minimumSpend": 100000,
			"usageLimitPerUser": 1,
			"usageLimitOverall": 1000,
			"startTime": "2025-06-14T10:00:00Z",
			"endTime": "2025-12-31T23:59:59Z",
			"applyToProductTypeId": null,
			"applyToProductTypeName": null,
			"usageCount": 123,
			"usedPercentage": 0.123,
			"creationTime": "2025-06-14T09:00:00Z",
			"creatorId": 1,
			"lastModificationTime": null,
			"lastModifierId": null
		}
	],
	"totalCount": 50,
	"pageSize": 10,
	"pageNumber": 1
}
```

### 2. Get Discount Voucher Detail

**GET** `/api/discount-vouchers/{id}`

Returns detailed information about a specific discount voucher.

#### Path Parameters

-   `id` (int): The ID of the discount voucher

#### Response

Same as individual item in the list response above.

### 3. Create Discount Voucher

**POST** `/api/discount-vouchers`

Creates a new discount voucher with validation rules.

#### Request Body

```json
{
	"code": "NEWVOUCHER",
	"description": "Description of the voucher",
	"valueType": "Percentage",
	"voucherType": "Order",
	"status": "Draft",
	"value": 0.1,
	"maximumDiscountValue": 30000,
	"minimumSpend": 50000,
	"usageLimitPerUser": 1,
	"usageLimitOverall": 500,
	"startTime": "2025-06-14T12:00:00Z",
	"endTime": "2025-12-31T23:59:59Z",
	"applyToProductTypeId": null
}
```

#### Validation Rules

-   `StartTime` must be at least 15 minutes from now
-   `EndTime` must be after `StartTime`
-   `Code` must be unique
-   If `ApplyToProductTypeId` is specified, the ProductType must exist
-   Business rules from `DiscountVoucher.Create()` method apply

#### Response

Returns the created voucher (same format as detail response).

### 4. Discount Voucher Action

**PATCH** `/api/discount-vouchers/{id}/action`

Performs status actions on a discount voucher (Start, Pause, Cancel).

#### Path Parameters

-   `id` (int): The ID of the discount voucher

#### Request Body

```json
{
	"action": "Start"
}
```

#### Valid Actions

-   `Start`: Changes status to Active (from Draft or Paused)
-   `Pause`: Changes status to Paused (from Active)
-   `Cancel`: Changes status to Cancelled (from Draft, Active, or Paused)

#### Business Rules

-   **Start**: Can only start Draft or Paused vouchers
-   **Pause**: Can only pause Active vouchers
-   **Cancel**: Can cancel Draft, Active, or Paused vouchers
-   Cannot perform actions on Expired or already Cancelled vouchers

#### Response

Returns 200 OK on success, or error details on failure.

### 5. Delete Discount Voucher

**DELETE** `/api/discount-vouchers/{id}`

Deletes a discount voucher (with business rule restrictions).

#### Path Parameters

-   `id` (int): The ID of the discount voucher

#### Business Rules

-   Can only delete vouchers with status Draft or Cancelled
-   Cannot delete vouchers that have been used (have VoucherUsages)

#### Response

Returns 200 OK on success, or error details on failure.

## Error Responses

All endpoints return standard error responses with the following format:

```json
{
	"type": "Validation",
	"code": "DiscountVoucher.StartTimeTooSoon",
	"description": "Start time must be at least 15 minutes from now"
}
```

## Common Error Codes

-   `DiscountVoucher.NotFound`: Voucher not found
-   `DiscountVoucher.StartTimeTooSoon`: Start time too close to current time
-   `DiscountVoucher.InvalidTimeRange`: End time before start time
-   `DiscountVoucher.CodeAlreadyExists`: Voucher code already exists
-   `DiscountVoucher.CannotDeleteUsed`: Cannot delete used vouchers
-   `DiscountVoucher.CannotDeleteActive`: Can only delete Draft/Cancelled vouchers
-   `DiscountVoucher.AlreadyActive`: Voucher is already active
-   `DiscountVoucher.AlreadyPaused`: Voucher is already paused
-   `DiscountVoucher.AlreadyCancelled`: Voucher is already cancelled

## Enums

### DiscountStatus

-   `Draft`: Initial status, not yet active
-   `Active`: Currently active and can be used
-   `Paused`: Temporarily disabled
-   `Expired`: Past end date (system-managed)
-   `Cancelled`: Manually cancelled

### DiscountVoucherType

-   `Order`: Applies to order total
-   `Shipping`: Applies to shipping cost

### DiscountValueType

-   `Fixed`: Fixed amount discount
-   `Percentage`: Percentage-based discount

### DiscountVoucherActionType

-   `Start`: Activate the voucher
-   `Pause`: Temporarily disable the voucher
-   `Cancel`: Permanently cancel the voucher
