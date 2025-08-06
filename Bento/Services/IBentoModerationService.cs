using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public interface IBentoModerationService
{
    Task<BentoResponse<T>> ModerateContentAsync<T>(ContentModerationRequest request);
}