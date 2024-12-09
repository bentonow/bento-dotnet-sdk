using Bento.Models;

namespace Bento.Services;

public interface IBentoTagService
{
    Task<BentoResponse<T>> GetTagsAsync<T>();
    Task<BentoResponse<T>> CreateTagAsync<T>(TagRequest tag);
}