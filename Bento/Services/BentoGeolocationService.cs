using System;
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
public class BentoGeolocationService : IBentoGeolocationService
{
    private readonly IBentoClient _client;

    public BentoGeolocationService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Geolocate IP address (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Geolocation request with IPv4 address</param>
    /// <returns>Generic geolocation response</returns>
    public Task<BentoResponse<T>> GeolocateIpAsync<T>(GeolocationRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        return _client.GetAsync<T>($"experimental/geolocation?ip={Uri.EscapeDataString(request.IpAddress)}");
    }

    /// <summary>
    /// Geolocate IP address
    /// Attempts to geolocate the provided IPv4 address with detailed location data
    /// </summary>
    /// <param name="request">Geolocation request with IPv4 address</param>
    /// <returns>Geolocation data with country, city, coordinates etc. (some fields may be null)</returns>
    /// <exception cref="BentoException">Thrown when IP geolocation fails</exception>
    public async Task<GeolocationResponse> GeolocateIpAsync(GeolocationRequest request)
    {
        var response = await GeolocateIpAsync<GeolocationResponse>(request);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "IP geolocation failed", response.StatusCode);
    }
}