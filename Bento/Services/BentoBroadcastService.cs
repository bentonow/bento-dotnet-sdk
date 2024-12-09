using Bento.Models;

namespace Bento.Services;

public class BentoBroadcastService : IBentoBroadcastService
{
    private readonly IBentoClient _client;

    public BentoBroadcastService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> GetBroadcastsAsync<T>()
    {
        return _client.GetAsync<T>("fetch/broadcasts");
    }

    public Task<BentoResponse<T>> CreateBroadcastAsync<T>(BroadcastRequest broadcast)
    {
        return CreateBatchBroadcastsAsync<T>(new[] { broadcast });
    }

    public Task<BentoResponse<T>> CreateBatchBroadcastsAsync<T>(IEnumerable<BroadcastRequest> broadcasts)
    {
        var request = new
        {
            broadcasts = broadcasts.Select(b => new
            {
                name = b.Name,
                subject = b.Subject,
                content = b.Content,
                type = b.Type,
                from = new
                {
                    email = b.From.EmailAddress,
                    name = b.From.Name
                },
                inclusive_tags = b.InclusiveTags,
                exclusive_tags = b.ExclusiveTags,
                segment_id = b.SegmentId,
                batch_size_per_hour = b.BatchSizePerHour
            })
        };

        return _client.PostAsync<T>("batch/broadcasts", request);
    }
}