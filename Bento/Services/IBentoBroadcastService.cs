using Bento.Models;

namespace Bento.Services;

public interface IBentoBroadcastService
{
    Task<BentoResponse<T>> GetBroadcastsAsync<T>();
    Task<BentoResponse<T>> CreateBroadcastAsync<T>(BroadcastRequest broadcast);
    Task<BentoResponse<T>> CreateBatchBroadcastsAsync<T>(IEnumerable<BroadcastRequest> broadcasts);
}