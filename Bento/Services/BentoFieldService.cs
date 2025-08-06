using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public class BentoFieldService : IBentoFieldService
{
    private readonly IBentoClient _client;

    public BentoFieldService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> GetFieldsAsync<T>()
    {
        return _client.GetAsync<T>("fetch/fields");
    }

    public Task<BentoResponse<T>> CreateFieldAsync<T>(FieldRequest field)
    {
        var request = new { field = new { key = field.Key } };
        return _client.PostAsync<T>("fetch/fields", request);
    }
}