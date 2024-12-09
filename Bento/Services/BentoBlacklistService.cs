using Bento.Models;

namespace Bento.Services;

public class BentoBlacklistService : IBentoBlacklistService
{
    private readonly IBentoClient _client;

    public BentoBlacklistService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> GetBlacklistStatusAsync<T>(BlacklistStatusRequest request)
    {
        return _client.GetAsync<T>("experimental/blacklist.json", new
        {
            domain = request.Domain,
            ip = request.IpAddress
        });
    }
}