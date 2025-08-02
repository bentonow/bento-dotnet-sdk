using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public class BentoTagService : IBentoTagService
{
    private readonly IBentoClient _client;

    public BentoTagService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> GetTagsAsync<T>()
    {
        return _client.GetAsync<T>("fetch/tags");
    }

    public Task<BentoResponse<T>> CreateTagAsync<T>(TagRequest tag)
    {
        var request = new { tag = new { name = tag.Name } };
        return _client.PostAsync<T>("fetch/tags", request);
    }
}