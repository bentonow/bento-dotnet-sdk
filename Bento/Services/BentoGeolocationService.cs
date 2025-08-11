using System;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for IP geolocation via Bento API.
/// Uses experimental/geolocation endpoint (<see href="https://docs.bentonow.com/utility#the-geolocate-ip-address-model" />).
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
    /// <param name="request">Geolocation request</param>
    /// <returns>Geolocation response</returns>
    public Task<BentoResponse<T>> GeolocateIpAsync<T>(GeolocationRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        return _client.GetAsync<T>($"experimental/geolocation?ip={Uri.EscapeDataString(request.IpAddress)}");
    }

    /// <summary>
    /// Geolocate IP address
    /// </summary>
    /// <param name="request">Geolocation request</param>
    /// <returns>Geolocation data with country, city, coordinates etc.</returns>
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