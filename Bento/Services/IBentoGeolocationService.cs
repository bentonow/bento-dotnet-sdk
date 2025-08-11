using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for IP geolocation via Bento API.
/// Uses experimental/geolocation endpoint (<see href="https://docs.bentonow.com/utility#the-geolocate-ip-address-model" />).
/// Geolocates IPv4 addresses with detailed location information.
/// </summary>
public interface IBentoGeolocationService
{
    /// <summary>
    /// Geolocate IP address (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Geolocation request</param>
    /// <returns>Geolocation response</returns>
    Task<BentoResponse<T>> GeolocateIpAsync<T>(GeolocationRequest request);
    
    /// <summary>
    /// Geolocate IP address
    /// </summary>
    /// <param name="request">Geolocation request</param>
    /// <returns>Geolocation data with country, city, coordinates etc.</returns>
    Task<GeolocationResponse> GeolocateIpAsync(GeolocationRequest request);
}