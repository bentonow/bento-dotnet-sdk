using System.Collections.Generic;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public interface IBentoEventService
{
    Task<BentoResponse<T>> TrackEventAsync<T>(EventRequest eventData);
    Task<BentoResponse<T>> TrackEventsAsync<T>(IEnumerable<EventRequest> events);
}