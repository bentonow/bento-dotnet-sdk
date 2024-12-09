using Bento.Models;

namespace Bento.Services;

public class BentoGenderService : IBentoGenderService
{
    private readonly IBentoClient _client;

    public BentoGenderService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> PredictGenderAsync<T>(GenderRequest request)
    {
        return _client.PostAsync<T>("experimental/gender", new { name = request.FullName });
    }
}