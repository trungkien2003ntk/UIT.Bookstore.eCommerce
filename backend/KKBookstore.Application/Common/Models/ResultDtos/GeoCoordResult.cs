namespace KKBookstore.Application.Common.Models.ResultDtos;

public class GeoCoordResult
{
    public bool Success { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? FormattedAddress { get; set; }
    public int Confidence { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Calculate the distance in kilometers to another coordinate using the Haversine formula
    /// </summary>
    public double DistanceTo(GeoCoordResult other)
    {
        return DistanceTo(other.Latitude, other.Longitude);
    }

    /// <summary>
    /// Calculate the distance in kilometers to another coordinate using the Haversine formula
    /// </summary>
    public double DistanceTo(double latitude, double longitude)
    {
        if (!Success) throw new InvalidOperationException("Cannot calculate distance from invalid coordinates");
        
        return CalculateHaversineDistance(Latitude, Longitude, latitude, longitude);
    }

    /// <summary>
    /// Calculate the distance in meters to another coordinate using the Haversine formula
    /// </summary>
    public double DistanceToInMeters(GeoCoordResult other)
    {
        return DistanceTo(other) * 1000;
    }

    /// <summary>
    /// Calculate the distance in meters to another coordinate using the Haversine formula
    /// </summary>
    public double DistanceToInMeters(double latitude, double longitude)
    {
        return DistanceTo(latitude, longitude) * 1000;
    }

    /// <summary>
    /// Haversine formula implementation for calculating distance between two points on Earth
    /// Returns distance in kilometers
    /// </summary>
    private static double CalculateHaversineDistance(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371; // Earth's radius in kilometers

        var dLat = ToRadians(lat2 - lat1);
        var dLon = ToRadians(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        
        return R * c; // Distance in kilometers
    }

    private static double ToRadians(double degrees) => degrees * Math.PI / 180;

    /// <summary>
    /// Create a GeoCoordResult from latitude and longitude (useful for warehouse coordinates)
    /// </summary>
    public static GeoCoordResult FromCoordinates(double latitude, double longitude, string? formattedAddress = null)
    {
        return new GeoCoordResult
        {
            Success = true,
            Latitude = latitude,
            Longitude = longitude,
            FormattedAddress = formattedAddress,
            Confidence = 10 // Assuming exact coordinates
        };
    }
}
