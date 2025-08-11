namespace Bento.Models;

/// <summary>
/// Request model for IP geolocation.
/// </summary>
/// <param name="IpAddress">IPv4 address to geolocate (e.g., "1.1.1.1")</param>
public record GeolocationRequest(string IpAddress);