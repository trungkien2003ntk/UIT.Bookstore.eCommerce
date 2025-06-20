# OpenCage Geocoding Service

This service provides geocoding functionality using the OpenCage Geocoding API to convert addresses to coordinates. It's designed to be used for finding the nearest warehouse from a customer's address using the Haversine formula.

## Features

-   Convert `Address` entities to geographic coordinates
-   Convert address strings to geographic coordinates
-   Built-in caching for improved performance
-   Error handling and logging
-   Configurable timeout and retry settings

## Configuration

Add the following configuration to your `appsettings.json`:

```json
{
	"OpenCageConfiguration": {
		"ApiKey": "YOUR-OPENCAGE-API-KEY",
		"BaseUrl": "https://api.opencagedata.com",
		"Language": "vi",
		"TimeoutSeconds": 30,
		"MaxRetries": 3
	}
}
```

### Getting an API Key

1. Visit [OpenCage Geocoding API](https://opencagedata.com/)
2. Sign up for a free account
3. Get your API key from the dashboard
4. Replace `YOUR-OPENCAGE-API-KEY` in the configuration

### Free Trial Limits

-   2,500 requests per day
-   1 request per second
-   No credit card required for trial

## Usage

### Dependency Injection

The service is automatically registered in `DependencyInjection.cs` with caching decorator:

```csharp
services.Configure<OpenCageConfiguration>(configuration.GetSection(nameof(OpenCageConfiguration)));
services.AddHttpClient<OpenCageGeocodingService>();
services.AddScoped<OpenCageGeocodingService>();
services.AddScoped<IGeoCoordService>(provider =>
    new CachedGeoCoordService(
        provider.GetRequiredService<IMemoryCache>(),
        provider.GetRequiredService<OpenCageGeocodingService>(),
        provider.GetRequiredService<ILogger<CachedGeoCoordService>>()
    ));
```

### In Controllers

```csharp
[ApiController]
public class LocationController : ApiController
{
    private readonly IGeoCoordService _geoCoordService;

    public LocationController(IGeoCoordService geoCoordService)
    {
        _geoCoordService = geoCoordService;
    }

    [HttpPost("geocode")]
    public async Task<IActionResult> GetCoordinates([FromBody] GeocodeRequest request)
    {
        var result = await _geoCoordService.GetCoordinatesAsync(request.Address);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
```

### In Services

```csharp
public class WarehouseService
{
    private readonly IGeoCoordService _geoCoordService;

    public WarehouseService(IGeoCoordService geoCoordService)
    {
        _geoCoordService = geoCoordService;
    }    public async Task<Warehouse> FindNearestWarehouse(Address customerAddress)
    {
        // Get customer coordinates
        var customerCoords = await _geoCoordService.GetCoordinatesAsync(customerAddress);

        if (!customerCoords.Success)
            throw new Exception($"Failed to geocode customer address: {customerCoords.ErrorMessage}");

        // Find nearest warehouse using built-in distance calculation
        var nearestWarehouse = warehouses
            .OrderBy(w => customerCoords.DistanceTo(w.Latitude, w.Longitude))
            .FirstOrDefault();

        return nearestWarehouse;
    }
}
```

## API Response

### Success Response

```json
{
	"success": true,
	"latitude": 10.762622,
	"longitude": 106.660172,
	"formattedAddress": "1 Nguyen Hue, Ben Nghe, District 1, Ho Chi Minh City, Vietnam",
	"confidence": 9,
	"errorMessage": null,
	"errorCode": null
}
```

### Error Response

```json
{
	"success": false,
	"latitude": 0,
	"longitude": 0,
	"formattedAddress": null,
	"confidence": 0,
	"errorMessage": "No coordinates found for the provided address",
	"errorCode": "NO_RESULTS"
}
```

## Confidence Scores

OpenCage returns confidence scores from 0-10:

-   **10**: < 0.25km (house/building level)
-   **9**: < 0.5km (street level)
-   **8**: < 1km (street level)
-   **7**: < 5km (suburb/village level)
-   **6**: < 7.5km
-   **5**: < 10km (city level)
-   **4**: < 15km
-   **3**: < 20km
-   **2**: < 25km (large city level)
-   **1**: > 25km (country/state level)
-   **0**: Unable to determine

## Caching

The service includes automatic caching with a 24-hour duration:

-   Address-based requests are cached using address components
-   String-based requests are cached using string hash
-   Cache keys are automatically generated
-   Failed requests are not cached

## Error Codes

-   `INVALID_INPUT`: Empty or null address
-   `API_ERROR`: HTTP request failed
-   `PARSE_ERROR`: Failed to parse API response
-   `NO_RESULTS`: No coordinates found for address
-   `TIMEOUT`: Request timed out
-   `INTERNAL_ERROR`: Unexpected error occurred

## Address Building Logic

For Vietnamese addresses, the service constructs the query in this format:

```
{DetailAddress}, {CommuneName}, {DistrictName}, {ProvinceName}, Vietnam
```

Example:

```
123 Nguyen Hue, Ben Nghe Ward, District 1, Ho Chi Minh City, Vietnam
```

## Testing

You can test the geocoding service using the provided endpoint:

```bash
POST /api/locations/geocode
Content-Type: application/json

{
  "address": "1 Nguyen Hue, District 1, Ho Chi Minh City"
}
```

## Performance Considerations

-   Use caching to avoid repeated API calls for the same addresses
-   The service has built-in timeout protection (30 seconds by default)
-   Consider implementing rate limiting for high-volume applications
-   Monitor your API usage to avoid hitting free trial limits

## Distance Calculation

The `GeoCoordResult` class includes built-in distance calculation methods using the Haversine formula:

```csharp
public class WarehouseService
{
    private readonly IGeoCoordService _geoCoordService;

    public async Task<Warehouse> FindNearestWarehouse(Address customerAddress)
    {
        // Get customer coordinates
        var customerCoords = await _geoCoordService.GetCoordinatesAsync(customerAddress);

        if (!customerCoords.Success)
            throw new Exception($"Failed to geocode customer address: {customerCoords.ErrorMessage}");

        // Create warehouse coordinates (you'd get these from your database)
        var warehouse1 = GeoCoordResult.FromCoordinates(10.762622, 106.660172, "Warehouse 1 - District 1");
        var warehouse2 = GeoCoordResult.FromCoordinates(10.785623, 106.681323, "Warehouse 2 - District 3");

        // Calculate distances and find nearest
        var nearestWarehouse = new[]
        {
            new { Warehouse = warehouse1, Distance = customerCoords.DistanceTo(warehouse1) },
            new { Warehouse = warehouse2, Distance = customerCoords.DistanceTo(warehouse2) }
        }
        .OrderBy(x => x.Distance)
        .First();

        Console.WriteLine($"Nearest warehouse: {nearestWarehouse.Warehouse.FormattedAddress}");
        Console.WriteLine($"Distance: {nearestWarehouse.Distance:F2} km");

        return nearestWarehouse.Warehouse;
    }
}
```

### Available Distance Methods

```csharp
var customerCoords = await _geoCoordService.GetCoordinatesAsync(address);
var warehouseCoords = GeoCoordResult.FromCoordinates(10.762622, 106.660172);

// Distance in kilometers
double distanceKm = customerCoords.DistanceTo(warehouseCoords);
double distanceKm2 = customerCoords.DistanceTo(10.762622, 106.660172);

// Distance in meters
double distanceM = customerCoords.DistanceToInMeters(warehouseCoords);
double distanceM2 = customerCoords.DistanceToInMeters(10.762622, 106.660172);
```

## Troubleshooting

### Common Issues

1. **401 Unauthorized**: Check your API key configuration
2. **429 Too Many Requests**: You've hit the rate limit, implement retry logic
3. **No Results**: The address might be too vague or incorrect
4. **Low Confidence**: Address geocoded but may not be precise

### Logging

The service logs important events:

-   Geocoding requests and responses
-   Cache hits and misses
-   API errors and timeouts
-   Successful geocoding with confidence scores

Check your application logs for troubleshooting information.
