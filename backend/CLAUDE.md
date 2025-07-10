# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

KKBookstore is a comprehensive e-commerce backend system built with .NET 8.0 following Clean Architecture principles. It's designed for a Vietnamese bookstore with features including product management, shopping cart, orders, payments, shipping integration, AI-powered services, and admin dashboards.

## Core Architecture

### Clean Architecture Layers
- **KKBookstore.API**: Web API layer with controllers, middleware, and entry point
- **KKBookstore.Application**: Application layer with CQRS handlers, services, and business logic
- **KKBookstore.Domain**: Domain layer with entities, value objects, and domain services
- **KKBookstore.Domain.Shared**: Shared domain constants and enums
- **KKBookstore.Infrastructure**: Infrastructure layer with database, external services, and implementations

### Additional Components
- **KKBookstore.DbMigrator**: Console application for database migrations and seeding
- **MailQueueFunction**: Azure Function for processing email queue
- **ProcessImageResizeFunction**: Azure Function for image processing and resizing
- **UpdateThumbnailUrlFunction**: Azure Function for updating product thumbnails

## Common Development Commands

### Build & Run
```bash
# Build entire solution
dotnet build KKBookstore.sln

# Run API application
dotnet run --project KKBookstore.API/KKBookstore.API.csproj

# Run API with hot reload for development
dotnet watch run --project KKBookstore.API/KKBookstore.API.csproj

# Run database migrator
dotnet run --project KKBookstore.DbMigrator/KKBookstore.DbMigrator.csproj
```

### Build Configurations
- **Debug**: Development build with sensitive data logging
- **Release**: Production build
- **Stage**: Staging environment build

### Entity Framework Migrations
```bash
# Add new migration
dotnet ef migrations add MigrationName --project KKBookstore.Infrastructure --startup-project KKBookstore.API

# Update database
dotnet ef database update --project KKBookstore.Infrastructure --startup-project KKBookstore.API
```

### Testing
**Note**: The project currently lacks formal test projects. There are test files in the root directory but no proper test infrastructure.

### Code Formatting
```bash
# Format code using built-in .NET formatter
dotnet format KKBookstore.sln
```

## Key Technical Patterns

### Design Patterns Used
- **CQRS Pattern**: Using MediatR for command/query separation
- **Mediator Pattern**: MediatR for request/response handling
- **Repository Pattern**: Entity Framework with DbContext as repository
- **Result Pattern**: Custom Result<T> for error handling
- **Decorator Pattern**: Used for caching services (shipping, geocoding, related products)
- **Factory Pattern**: Used in various service configurations
- **Domain Events**: For cross-cutting concerns and notifications

### Service Registration
Services are registered in three main DI containers:
- `KKBookstore.Application.DependencyInjection.AddApplicationServices()`
- `KKBookstore.Infrastructure.DependencyInjection.AddInfrastructureServices()`
- `KKBookstore.Domain.DependencyInjection.AddDomainServices()`

## Database & Data Access

### Entity Framework Configuration
- Uses SQL Server with Entity Framework Core
- Auditing interceptor for tracking created/modified timestamps
- Migrations located in `KKBookstore.Infrastructure/Data/Migrations/`
- Configuration classes in `KKBookstore.Infrastructure/Data/Configurations/`

### Database Seeding
- Handled by `KKBookstore.DbMigrator` console application
- Seed data located in `KKBookstore.DbMigrator/Seeders/JsonData/`

## External Service Integrations

### Payment Integration
- **VnPay**: Vietnamese payment gateway integration
- Configuration: `VnPayConfiguration` section in appsettings

### Shipping Integration
- **GHN (Giao Hàng Nhanh)**: Vietnamese shipping service
- Webhook handling for shipping status updates
- Configuration: `ShippingConfiguration` and `GhnConfiguration` sections

### AI Services
- **Google Gemini**: AI content moderation and related products
- **OpenCage Geocoding**: Address to coordinates conversion
- **Azure AI Search**: Product search functionality

### Cloud Services
- **Azure Blob Storage**: File and image storage
- **Azure Queue Storage**: Message queuing
- **Azure Service Bus**: Event messaging
- **Azure Functions**: Serverless image processing and email handling

## Security & Authentication

### JWT Authentication
- Custom JWT token implementation with blacklisting
- Token versioning for security (invalidate all user tokens)
- Middleware: `JwtBlacklistMiddleware` and `JwtTokenVersionMiddleware`

### Role-Based Authorization
- ASP.NET Core Identity with roles: Admin, SalesStaff, CustomerCareStaff, Customer
- Custom authorization attributes in Application layer

## API Structure

### Controllers (23 total)
Key controllers include:
- `AuthenticationController`: User authentication and registration
- `ProductsController`: Product management and catalog
- `OrdersController`: Order processing and management
- `ShoppingCartController`: Shopping cart operations
- `CheckoutController`: Checkout process
- `DashboardController`: Admin dashboard analytics
- `GhnWebhookController`: Shipping webhook handling

### Request/Response Pattern
- Request DTOs in `KKBookstore.API/Contracts/Requests/`
- Response DTOs in `KKBookstore.API/Contracts/Responses/`
- AutoMapper profiles for DTO mapping

## Configuration Management

### Environment-Specific Settings
- `appsettings.json`: Base configuration
- `appsettings.Development.json`: Development overrides
- `appsettings.Production.json`: Production overrides

### Key Configuration Sections
- `JwtSettings`: JWT authentication settings
- `EmailConfiguration`: SMTP email settings
- `VnPayConfiguration`: Payment gateway settings
- `GeminiConfiguration`: AI service settings
- `OpenCageConfiguration`: Geocoding service settings

## Logging & Monitoring

### Structured Logging
- Uses Serilog for structured logging
- Log files stored in `KKBookstore.API/Logs/`
- Request/response logging middleware

### Global Exception Handling
- Custom `GlobalExceptionHandler` for centralized error handling
- Structured error responses with problem details

## Development Notes

### Code Organization
- Features organized by domain in `KKBookstore.Application/Features/`
- Each feature contains Commands, Queries, and Handlers
- Shared interfaces in `KKBookstore.Common/Interfaces/`

### Caching Strategy
- Decorator pattern for caching frequently accessed services
- In-memory caching for shipping rates, geocoding, and related products
- 24-hour cache duration for external API calls

### Email System
- Template-based email system using Scriban templates
- Queue-based email processing via Azure Functions
- Email templates in `KKBookstore.Domain/Emailing/Templates/`

### Image Processing
- Automated image resizing via Azure Functions
- Thumbnail generation for product images
- Integration with Azure Blob Storage for scalable image storage

## Important Files to Review

- `Program.cs`: Application bootstrap and middleware configuration
- `KKBookstore.Infrastructure/Data/KKBookstoreDbContext.cs`: Database context
- `KKBookstore.Application/Features/`: CQRS handlers and business logic
- `KKBookstore.Domain/`: Domain entities and business rules
- `appsettings.json`: Configuration settings