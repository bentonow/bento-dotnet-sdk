using Bento.Models;

namespace Bento.Services;

public interface IBentoGeolocationService
{
    Task<BentoResponse<T>> GeolocateIpAsync<T>(GeolocationRequest request);
}