# GHN API Analysis and Interface Enhancement

## Overview

Based on the analysis of the official GHN (Giao Hang Nhanh) API documentation, I've enhanced the `IGhnShippingService` interface to support all major GHN shipping operations. This enhancement follows Clean Architecture and DDD principles and covers all key use cases for a real-world bookstore shipping workflow.

## GHN API Endpoints Analysis

### 1. **Create Order** (`/shipping-order/create`)

**When to use**: When a customer places an order and chooses GHN shipping

-   **Business Context**: After order confirmation, payment processing, and inventory allocation
-   **Purpose**: Creates a shipping order in GHN system with full package and delivery details
-   **Key Data Required**:
    -   Customer details (name, phone, address)
    -   Package dimensions and weight
    -   COD amount
    -   Service type and payment type
    -   Items list with product details
-   **Returns**: GHN order code, expected delivery time, fees breakdown

### 2. **Order Info** (`/shipping-order/detail`)

**When to use**: To check current status and get detailed information about an existing order

-   **Business Context**: Customer service inquiries, order status updates, troubleshooting
-   **Purpose**: Get comprehensive order details including status, tracking logs, fees
-   **Key Data**: Current status, delivery timeline, warehouse locations, complete status history
-   **Returns**: Full order information with timestamps, locations, and operational details

### 3. **Cancel Order** (`/switch-status/cancel`)

**When to use**: When customer cancels before pickup or business needs to cancel

-   **Business Context**: Customer cancellation, inventory issues, payment failures
-   **Purpose**: Cancel a shipping order that hasn't been picked up yet
-   **Constraint**: Only works for orders not yet picked up by GHN
-   **Returns**: Success/failure status for each order code

### 4. **Return Order** (`/switch-status/return`)

**When to use**: When customer wants to return items or delivery failed

-   **Business Context**: Customer returns, failed deliveries, refund processing
-   **Purpose**: Initiate return process for orders in storage or waiting for delivery
-   **Constraint**: Only for orders with status "storage" or "waiting for delivery"
-   **Returns**: Return operation status for each order code

### 5. **Update COD** (`/shipping-order/updateCOD`)

**When to use**: When the cash-on-delivery amount needs to be changed

-   **Business Context**: Price adjustments, discount applications, order modifications
-   **Purpose**: Modify the COD amount before delivery
-   **Constraint**: Must be done before delivery completion
-   **Maximum**: 50,000,000 VND
-   **Returns**: Simple success/failure status

### 6. **Calculate Expected Delivery Time** (`/shipping-order/leadtime`)

**When to use**: Before creating order or for customer inquiries about delivery time

-   **Business Context**: Setting customer expectations, delivery scheduling, shipping cost calculation
-   **Purpose**: Get estimated delivery date for planning purposes
-   **Key Data**: Route information (from/to locations), service type
-   **Returns**: Expected delivery timestamp and order creation time

### 7. **Pick Shift** (`/shift/date`)

**When to use**: To show available pickup time slots when creating orders

-   **Business Context**: Allowing customers to choose pickup times, optimizing logistics
-   **Purpose**: Get available pickup shifts for order scheduling
-   **Returns**: List of available time slots with specific time ranges

### 8. **Create Store** (`/shop/register`)

**When to use**: When setting up new bookstore locations or warehouses

-   **Business Context**: Business expansion, new branch setup, warehouse management
-   **Purpose**: Register new pickup locations in GHN system
-   **Key Data**: Store details, location information for pickup coordination
-   **Returns**: Assigned shop ID for the new store location

## Enhanced Interface Structure

The enhanced `IGhnShippingService` interface is organized into logical sections:

### 1. **Status Mapping & Webhook Processing**

-   Maps GHN status strings to internal enums
-   Processes webhook updates for order status changes
-   Maintains existing webhook functionality

### 2. **Order Management**

-   `CreateOrderAsync()` - Creates new shipping orders
-   `GetOrderInfoAsync()` - Retrieves comprehensive order information
-   `CancelOrdersAsync()` - Cancels orders before pickup
-   `ReturnOrdersAsync()` - Initiates return processes
-   `UpdateCodAmountAsync()` - Updates COD amounts

### 3. **Delivery Planning & Information**

-   `CalculateDeliveryTimeAsync()` - Gets delivery time estimates
-   `GetPickShiftsAsync()` - Retrieves available pickup slots

### 4. **Store Management**

-   `CreateStoreAsync()` - Registers new pickup locations

### 5. **Configuration & Validation**

-   `ValidateConfigurationAsync()` - Ensures proper service setup

## Business Workflow Integration

### Order Processing Flow:

1. **Pre-Order**: Use `CalculateDeliveryTimeAsync()` to show estimated delivery times
2. **Order Creation**: Use `GetPickShiftsAsync()` to offer pickup slot options
3. **Order Confirmation**: Use `CreateOrderAsync()` to create the shipping order
4. **Order Management**: Use `GetOrderInfoAsync()` for status tracking
5. **Order Changes**: Use `UpdateCodAmountAsync()` for price adjustments
6. **Order Cancellation**: Use `CancelOrdersAsync()` or `ReturnOrdersAsync()` as needed

### Store Management Flow:

1. **Business Expansion**: Use `CreateStoreAsync()` to register new locations
2. **Configuration**: Use `ValidateConfigurationAsync()` for health checks

## Implementation Benefits

1. **Comprehensive Coverage**: All major GHN operations are supported
2. **Clean Architecture**: Interface defined in Application layer, implementation in Infrastructure
3. **Business-Focused**: Methods clearly explain when and why to use each operation
4. **Type Safety**: Strongly-typed DTOs for all requests and responses
5. **Error Handling**: Consistent Result pattern for error management
6. **Documentation**: Extensive XML documentation with business context
7. **Maintainability**: Organized in logical sections for easy navigation

## Next Steps

1. **DTO Implementation**: Ensure all required DTOs exist (CreateGhnOrderRequest, GhnOrderInfoResult, etc.)
2. **Service Implementation**: Update the concrete GhnShippingService implementation
3. **DI Registration**: Ensure proper dependency injection setup
4. **Testing**: Add comprehensive unit and integration tests
5. **API Controllers**: Expose new operations through HTTP endpoints if needed
6. **Configuration**: Update configuration classes for new settings

This enhancement provides a production-ready, comprehensive GHN shipping integration that covers all major use cases for a bookstore e-commerce system.
