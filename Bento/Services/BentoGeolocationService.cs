using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public class BentoGeolocationService : IBentoGeolocationService
{
    private readonly IBentoClient _client;

    public BentoGeolocationService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> GeolocateIpAsync<T>(GeolocationRequest request)
    {
        return _client.GetAsync<T>("experimental/geolocation", new { ip = request.IpAddress });
    }
}