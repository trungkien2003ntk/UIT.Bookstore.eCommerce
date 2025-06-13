---
applyTo: "**"
---

# Domain-to-API Implementation Workflow

## Overview

This document provides a systematic approach for implementing complete features from domain entities to API endpoints in the KKBookstore system, following Clean Architecture and DDD principles.

## 🏗️ Architecture Layers (Bottom-Up Implementation)

### 1. **Domain Layer** (`KKBookstore.Domain`)

**Files to create/modify:**

-   `Orders/[EntityName].cs` - Domain entity with business logic
-   `Orders/[EntityName]Errors.cs` - Domain-specific errors
-   `Orders/[JunctionEntity].cs` - Junction entities for many-to-many relationships

**Entity Implementation Checklist:**

-   [ ] Inherit from appropriate base class (`BaseAuditedEntity`, `BaseEntity`)
-   [ ] Add domain methods for business operations (e.g., `Start()`, `Pause()`, `Cancel()`)
-   [ ] Implement static factory method `Create()` with validation
-   [ ] Add navigation properties for relationships
-   [ ] Include private constructors and public parameterless constructor

**Example Structure:**

```csharp
public class DiscountVoucher : BaseFullAuditedEntity
{
    // Properties
    public string Name { get; set; }

    // Navigation properties
    public ICollection<VoucherCustomerType> CustomerTypes { get; set; } = [];

    // Domain methods
    public Result Start() { /* business logic */ }

    // Factory method
    public static Result<DiscountVoucher> Create(...) { /* validation */ }
}
```

### 2. **Domain.Shared Layer** (`KKBookstore.Domain.Shared`)

**Files to create/modify:**

-   `Orders/[EnumName].cs` - Shared enums and value objects

### 3. **Infrastructure Layer** (`KKBookstore.Infrastructure`)

**Files to create/modify:**

-   `Data/Configurations/Orders/[EntityName]Configuration.cs` - EF Core configuration
-   `Data/KKBookstoreDbContext.cs` - Add DbSet properties
-   `Common/Interfaces/IApplicationDbContext.cs` - Add DbSet to interface
-   `[ServiceCategory]/[ServiceName]Service.cs` - External service implementations
-   `DependencyInjection.cs` - Service registration

**Configuration Implementation Checklist:**

-   [ ] Configure table name with `builder.ToTable()`
-   [ ] Add auditing with `builder.ConfigureAuditing()`
-   [ ] Configure properties (max lengths, precision, required fields)
-   [ ] Set up relationships (foreign keys, navigation properties)
-   [ ] Add indexes for performance
-   [ ] Configure enum conversions

**Example Structure:**

```csharp
internal class DiscountVoucherConfiguration : IEntityTypeConfiguration<DiscountVoucher>
{
    public void Configure(EntityTypeBuilder<DiscountVoucher> builder)
    {
        builder.ToTable("DiscountVouchers");
        builder.ConfigureAuditing();
        // Properties, relationships, indexes...
    }
}
```

### **External Service Implementation Pattern**

#### **File Structure:**

-   **Interface**: `Application/Common/Interfaces/I[ServiceName]Service.cs`
-   **Implementation**: `Infrastructure/[Category]/[ServiceName]Service.cs`
-   **Configuration**: `Infrastructure/[Category]/[ServiceName]Configuration.cs`
-   **Models**: `Application/Common/Models/RequestDtos|ResultDtos/`
-   **Registration**: `Infrastructure/DependencyInjection.cs`

#### **Naming Conventions:**

-   Interface: `I[ServiceName]Service` (e.g., `IGeminiService`)
-   Implementation: `[ServiceName]Service` (e.g., `GeminiService`, `VnPayPaymentService`)
-   Configuration: `[ServiceName]Configuration` (e.g., `GeminiConfiguration`)
-   Models: `[ServiceName][Purpose]Result/Request` (e.g., `GeminiTextResult`)

#### **Folder Categories:**

```
Infrastructure/
  AI/        // Gemini, OpenAI
  Payment/   // VnPay, Stripe
  Shipping/  // Delivery services
  Search/    // Azure Search, Elasticsearch
  Storage/   // Blob, File storage
  Emailing/  // SMTP, SendGrid
```

#### **Service Implementation Pattern:**

```csharp
public class GeminiService : IGeminiService
{
    private readonly IGeminiClient _client;
    private readonly ILogger<GeminiService> _logger;

    public GeminiService(IGeminiClient client, ILogger<GeminiService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<GeminiTextResult> GenerateTextAsync(string prompt)
    {
        try
        {
            _logger.LogInformation("Generating text with Gemini AI");
            var response = await _client.TextPrompt(prompt);

            return new GeminiTextResult
            {
                Success = true,
                Text = response?.Candidates?.First()?.Content?.Parts?.FirstOrDefault()?.Text ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating text with Gemini AI");
            return new GeminiTextResult { Success = false, ErrorMessage = ex.Message };
        }
    }
}
```

#### **DI Registration Pattern:**

```csharp
// In DependencyInjection.cs
/// Config [ServiceName]
services.Configure<ServiceConfiguration>(configuration.GetSection(nameof(ServiceConfiguration)));
services.AddScoped<IServiceInterface, ServiceImplementation>();

// With decorator for caching
services.AddScoped<BaseService>();
services.AddScoped<IServiceInterface>(provider =>
    new CachedServiceDecorator(
        provider.GetRequiredService<IMemoryCache>(),
        provider.GetRequiredService<BaseService>()
    ));
```

### 4. **Application Layer** (`KKBookstore.Application`)

**Folder Structure Pattern:**

```
Features/
  [EntityName]s/
    Models/
      [EntityName]Dto.cs
    Get[EntityName]List/
      Get[EntityName]ListQuery.cs
    Get[EntityName]Detail/
      Get[EntityName]DetailQuery.cs
    Create[EntityName]/
      Create[EntityName]Command.cs
    Update[EntityName]/
      Update[EntityName]Command.cs
    Delete[EntityName]/
      Delete[EntityName]Command.cs
    [EntityName]Action/
      [EntityName]ActionCommand.cs
```

**Command/Query Implementation Checklist:**

#### **DTOs:**

-   [ ] Include all necessary properties
-   [ ] Add navigation property data (Ids and Names)
-   [ ] Include computed properties (usage percentages, etc.)

#### **Queries (GET):**

-   [ ] Include all necessary `.Include()` statements
-   [ ] Map domain entities to DTOs
-   [ ] Handle pagination for list queries
-   [ ] Add filtering and sorting capabilities

#### **Commands (POST/PUT/PATCH/DELETE):**

-   [ ] Validate business rules
-   [ ] Check entity existence
-   [ ] Validate foreign key relationships
-   [ ] Handle junction table operations for many-to-many relationships
-   [ ] Use domain methods for state changes
-   [ ] Return appropriate DTOs

### 5. **API Layer** (`KKBookstore.API`)

**Files to create/modify:**

-   `Controllers/[EntityName]Controller.cs` - API endpoints

**Controller Implementation Checklist:**

-   [ ] Follow RESTful conventions
-   [ ] Use appropriate HTTP methods:
    -   `GET` for queries
    -   `POST` for creation
    -   `PUT` for full updates
    -   `PATCH` for partial updates/actions
    -   `DELETE` for deletion
-   [ ] Extract route parameters and merge with command objects
-   [ ] Use consistent response patterns
-   [ ] Add proper using statements

**HTTP Method Guidelines:**

```csharp
[HttpGet]                     // List/Query
[HttpGet("{id}")]            // Detail/Single
[HttpPost]                   // Create
[HttpPut("{id}")]           // Full Update
[HttpPatch("{id}/action")]  // State Changes/Actions
[HttpDelete("{id}")]        // Delete
```

## 🔄 Implementation Sequence

### Phase 1: Foundation

1. **Domain Entity** - Core business logic
2. **Domain Errors** - Error definitions
3. **Junction Entities** - Many-to-many relationships
4. **Shared Enums** - Cross-layer value objects

### Phase 2: Infrastructure

1. **EF Configurations** - Database mapping
2. **External Service Interfaces** - Application layer service contracts
3. **External Service Implementations** - Infrastructure layer implementations
4. **Service Configurations** - Settings and options classes
5. **Service Registration** - Dependency injection setup
6. **DbContext Updates** - Register new entities
7. **Generate Migration** - Database schema changes
8. **Apply Migration** - Update database

### Phase 3: Application Layer

1. **DTOs** - Data transfer objects
2. **Query Handlers** - Read operations (GET)
3. **Command Handlers** - Write operations (POST/PUT/PATCH/DELETE)

### Phase 4: API Layer

1. **Controller** - HTTP endpoints
2. **Request/Response Models** - API contracts

### Phase 5: Testing & Validation

1. **Build Solution** - Check for compilation errors
2. **Test Endpoints** - Verify functionality
3. **Update Documentation** - API documentation

## 🔍 Key Patterns & Best Practices

### **Domain-Driven Design:**

-   Business logic in domain entities
-   Use domain methods for state transitions
-   Validate business rules in domain layer
-   Factory methods for entity creation

### **Clean Architecture:**

-   Dependencies point inward
-   Application layer orchestrates domain
-   Infrastructure implements interfaces
-   API layer handles HTTP concerns

### **Entity Framework:**

-   Junction entities for many-to-many relationships
-   Always configure auditing for tracked entities
-   Use `Include()` for loading related data
-   Batch related changes in single SaveChanges()

### **External Service Integration:**

-   Define interfaces in Application layer for clean architecture
-   Implement services in Infrastructure layer with proper error handling
-   Use configuration pattern for external service settings
-   Register services with dependency injection container
-   Consider decorator pattern for cross-cutting concerns (caching, logging)
-   Follow consistent naming conventions across service categories

### **Error Handling:**

-   Domain-specific error classes
-   Result pattern for operation outcomes
-   Consistent error response format

### **Relationship Management:**

-   Load existing relationships before updates
-   Remove old junction records before adding new ones
-   Use navigation properties appropriately

## 📝 Common Commands & Snippets

### **Generate Migration:**

```powershell
dotnet ef migrations add [MigrationName] --project KKBookstore.Infrastructure --startup-project KKBookstore.API
```

### **Apply Migration:**

```powershell
dotnet ef database update --project KKBookstore.Infrastructure --startup-project KKBookstore.API
```

### **Build Solution:**

```powershell
dotnet build
```

### **Many-to-Many Update Pattern:**

```csharp
// Remove existing relationships
var existing = entity.RelatedItems.ToList();
foreach (var item in existing)
{
    dbContext.JunctionTable.Remove(item);
}

// Add new relationships
foreach (var id in request.RelatedIds)
{
    var junction = new JunctionEntity
    {
        EntityId = entity.Id,
        RelatedId = id
    };
    dbContext.JunctionTable.Add(junction);
}
```

### **External Service Configuration:**

```json
{
	"ServiceConfiguration": {
		"ApiKey": "your-api-key",
		"BaseUrl": "https://api.service.com"
	}
}
```

### **External Service Registration Pattern:**

```csharp
// Basic registration
services.Configure<ServiceConfiguration>(configuration.GetSection(nameof(ServiceConfiguration)));
services.AddScoped<IServiceInterface, ServiceImplementation>();

// With decorator pattern
services.AddScoped<BaseService>();
services.AddScoped<IServiceInterface>(provider =>
    new CachedServiceDecorator(
        provider.GetRequiredService<IMemoryCache>(),
        provider.GetRequiredService<BaseService>()
    ));
```

This workflow ensures consistent, maintainable, and properly architected implementations across the entire application stack.
