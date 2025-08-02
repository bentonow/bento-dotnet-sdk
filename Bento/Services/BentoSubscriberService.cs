using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public class BentoSubscriberService : IBentoSubscriberService
{
    private readonly IBentoClient _client;

    public BentoSubscriberService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> FindSubscriberAsync<T>(string email)
    {
        return _client.GetAsync<T>("fetch/subscribers", new { email });
    }

    public Task<BentoResponse<T>> CreateSubscriberAsync<T>(SubscriberRequest subscriber)
    {
        var request = new { subscriber = new { email = subscriber.Email } };
        return _client.PostAsync<T>("fetch/subscribers", request);
    }

    public Task<BentoResponse<T>> ImportSubscribersAsync<T>(IEnumerable<SubscriberRequest> subscribers)
    {
        var request = new
        {
            subscribers = subscribers.Select(s => new
            {
                email = s.Email,
                first_name = s.FirstName,
                last_name = s.LastName,
                tags = s.Tags != null ? string.Join(",", s.Tags) : null,
                remove_tags = s.RemoveTags != null ? string.Join(",", s.RemoveTags) : null,
                fields = s.Fields
            })
        };

        return _client.PostAsync<T>("batch/subscribers", request);
    }
}