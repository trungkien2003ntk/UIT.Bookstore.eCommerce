using System.Text.Json.Serialization;

namespace KKBookstore.Application.Common.Models.RequestDtos;

public class OpenCageGeocodingRequest
{
    public string Query { get; set; } = string.Empty;
    public string? Language { get; set; }
    public int Limit { get; set; } = 1;
    public bool NoAnnotations { get; set; } = true;
    public bool Pretty { get; set; } = false;
}

public class OpenCageGeocodingResponse
{
    [JsonPropertyName("results")]
    public List<OpenCageResult> Results { get; set; } = new();

    [JsonPropertyName("status")]
    public OpenCageStatus Status { get; set; } = new();

    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
}

public class OpenCageResult
{
    [JsonPropertyName("formatted")]
    public string Formatted { get; set; } = string.Empty;

    [JsonPropertyName("geometry")]
    public OpenCageGeometry Geometry { get; set; } = new();

    [JsonPropertyName("confidence")]
    public int Confidence { get; set; }

    [JsonPropertyName("components")]
    public OpenCageComponents Components { get; set; } = new();
}

public class OpenCageGeometry
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lng")]
    public double Longitude { get; set; }
}

public class OpenCageComponents
{
    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("state")]
    public string? State { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("postcode")]
    public string? Postcode { get; set; }

    [JsonPropertyName("road")]
    public string? Road { get; set; }
}

public class OpenCageStatus
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}
