using Bento.Models;

namespace Bento.Services;

public class BentoModerationService : IBentoModerationService
{
    private readonly IBentoClient _client;

    public BentoModerationService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> ModerateContentAsync<T>(ContentModerationRequest request)
    {
        return _client.PostAsync<T>("experimental/content_moderation", new { content = request.Content });
    }
}