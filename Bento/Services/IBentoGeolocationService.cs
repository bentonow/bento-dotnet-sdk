using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for IP geolocation via Bento API.
/// Uses experimental/geolocation endpoint (<see href="https://docs.bentonow.com/utility" />).
/// Geolocates IPv4 addresses with detailed location information.
/// Only IPv4 addresses are currently supported.
/// Response may contain null values for some fields.
/// </summary>
public interface IBentoGeolocationService
{
    /// <summary>
    /// Geolocate IP address (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Geolocation request with IPv4 address</param>
    /// <returns>Generic geolocation response</returns>
    Task<BentoResponse<T>> GeolocateIpAsync<T>(GeolocationRequest request);
    
    /// <summary>
    /// Geolocate IP address
    /// Attempts to geolocate the provided IPv4 address with detailed location data
    /// </summary>
    /// <param name="request">Geolocation request with IPv4 address</param>
    /// <returns>Geolocation data with country, city, coordinates etc. (some fields may be null)</returns>
    Task<GeolocationResponse> GeolocateIpAsync(GeolocationRequest request);
}