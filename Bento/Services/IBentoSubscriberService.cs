using System.Collections.Generic;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public interface IBentoSubscriberService
{
    Task<BentoResponse<T>> FindSubscriberAsync<T>(string email);
    Task<BentoResponse<T>> CreateSubscriberAsync<T>(SubscriberRequest subscriber);
    Task<BentoResponse<T>> ImportSubscribersAsync<T>(IEnumerable<SubscriberRequest> subscribers);
}