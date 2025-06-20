using System.Text.Json;
using System.Web;
using KKBookstore.Application.Common.Interfaces;
using KKBookstore.Application.Common.Models.RequestDtos;
using KKBookstore.Application.Common.Models.ResultDtos;
using KKBookstore.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KKBookstore.Infrastructure.Geolocation;

public class OpenCageGeocodingService : IGeoCoordService
{
    private readonly HttpClient _httpClient;
    private readonly OpenCageConfiguration _configuration;
    private readonly ILogger<OpenCageGeocodingService> _logger;

    public OpenCageGeocodingService(
        HttpClient httpClient,
        IOptions<OpenCageConfiguration> configuration,
        ILogger<OpenCageGeocodingService> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration.Value;
        _logger = logger;
        
        _httpClient.BaseAddress = new Uri(_configuration.BaseUrl);
        _httpClient.Timeout = TimeSpan.FromSeconds(_configuration.TimeoutSeconds);
    }

    public async Task<GeoCoordResult> GetCoordinatesAsync(Address address, CancellationToken cancellationToken = default)
    {
        var fullAddress = BuildAddressString(address);
        return await GetCoordinatesAsync(fullAddress, cancellationToken);
    }

    public async Task<GeoCoordResult> GetCoordinatesAsync(string fullAddress, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Geocoding address: {Address}", fullAddress);

            if (string.IsNullOrWhiteSpace(fullAddress))
            {
                return new GeoCoordResult
                {
                    Success = false,
                    ErrorMessage = "Address cannot be empty",
                    ErrorCode = "INVALID_INPUT"
                };
            }

            var requestUrl = BuildRequestUrl(fullAddress);
            var response = await _httpClient.GetAsync(requestUrl, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("OpenCage API returned error status: {StatusCode}", response.StatusCode);
                return new GeoCoordResult
                {
                    Success = false,
                    ErrorMessage = $"API request failed with status: {response.StatusCode}",
                    ErrorCode = "API_ERROR"
                };
            }

            var jsonContent = await response.Content.ReadAsStringAsync(cancellationToken);
            var openCageResponse = JsonSerializer.Deserialize<OpenCageGeocodingResponse>(jsonContent);

            if (openCageResponse == null)
            {
                _logger.LogError("Failed to deserialize OpenCage API response");
                return new GeoCoordResult
                {
                    Success = false,
                    ErrorMessage = "Failed to parse API response",
                    ErrorCode = "PARSE_ERROR"
                };
            }

            if (openCageResponse.Status.Code != 200)
            {
                _logger.LogError("OpenCage API returned error: {Code} - {Message}", 
                    openCageResponse.Status.Code, openCageResponse.Status.Message);
                return new GeoCoordResult
                {
                    Success = false,
                    ErrorMessage = openCageResponse.Status.Message,
                    ErrorCode = openCageResponse.Status.Code.ToString()
                };
            }

            if (openCageResponse.Results.Count == 0)
            {
                _logger.LogWarning("No geocoding results found for address: {Address}", fullAddress);
                return new GeoCoordResult
                {
                    Success = false,
                    ErrorMessage = "No coordinates found for the provided address",
                    ErrorCode = "NO_RESULTS"
                };
            }

            var result = openCageResponse.Results.First();
            _logger.LogInformation("Successfully geocoded address. Lat: {Lat}, Lng: {Lng}, Confidence: {Confidence}",
                result.Geometry.Latitude, result.Geometry.Longitude, result.Confidence);

            return new GeoCoordResult
            {
                Success = true,
                Latitude = result.Geometry.Latitude,
                Longitude = result.Geometry.Longitude,
                FormattedAddress = result.Formatted,
                Confidence = result.Confidence
            };
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
        {
            _logger.LogError(ex, "Timeout occurred while geocoding address: {Address}", fullAddress);
            return new GeoCoordResult
            {
                Success = false,
                ErrorMessage = "Request timed out",
                ErrorCode = "TIMEOUT"
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while geocoding address: {Address}", fullAddress);
            return new GeoCoordResult
            {
                Success = false,
                ErrorMessage = "An error occurred while processing the request",
                ErrorCode = "INTERNAL_ERROR"
            };
        }
    }

    private string BuildAddressString(Address address)
    {
        var addressParts = new List<string>();

        if (!string.IsNullOrWhiteSpace(address.DetailAddress))
            addressParts.Add(address.DetailAddress);

        if (!string.IsNullOrWhiteSpace(address.CommuneName))
            addressParts.Add(address.CommuneName);

        if (!string.IsNullOrWhiteSpace(address.DistrictName))
            addressParts.Add(address.DistrictName);

        if (!string.IsNullOrWhiteSpace(address.ProvinceName))
            addressParts.Add(address.ProvinceName);

        // Assuming this is for Vietnam based on the address structure
        addressParts.Add("Vietnam");

        return string.Join(", ", addressParts);
    }

    private string BuildRequestUrl(string address)
    {
        var encodedAddress = HttpUtility.UrlEncode(address);
        var url = $"/geocode/v1/json?q={encodedAddress}&key={_configuration.ApiKey}";
        
        if (!string.IsNullOrWhiteSpace(_configuration.Language))
            url += $"&language={_configuration.Language}";
        
        url += "&limit=1&no_annotations=1";
        
        return url;
    }
}
