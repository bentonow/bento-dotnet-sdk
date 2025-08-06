using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public interface IBentoBlacklistService
{
    Task<BentoResponse<T>> GetBlacklistStatusAsync<T>(BlacklistStatusRequest request);
}