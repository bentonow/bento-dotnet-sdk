using Bento.Models;

namespace Bento.Services;

public class BentoEventService : IBentoEventService
{
    private readonly IBentoClient _client;

    public BentoEventService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> TrackEventAsync<T>(EventRequest eventData)
    {
        return TrackEventsAsync<T>(new[] { eventData });
    }

    public Task<BentoResponse<T>> TrackEventsAsync<T>(IEnumerable<EventRequest> events)
    {
        var request = new
        {
            events = events.Select(e => new
            {
                type = e.Type,
                email = e.Email,
                fields = e.Fields?.Count > 0 ? e.Fields : null,
                details = e.Details?.Count > 0 ? e.Details : null
            })
        };

        return _client.PostAsync<T>("batch/events", request);
    }
}