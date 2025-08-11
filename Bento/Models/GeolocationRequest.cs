namespace Bento.Models;

/// <summary>
/// Request model for IP geolocation using experimental/geolocation endpoint.
/// Attempts to geolocate the provided IP address.
/// Only works with IPv4 IP addresses.
/// </summary>
/// <param name="IpAddress">The IPv4 address you wish to geolocate (e.g., "1.1.1.1")</param>
public record GeolocationRequest(string IpAddress);