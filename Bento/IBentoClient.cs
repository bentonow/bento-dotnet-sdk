using System.Threading.Tasks;

namespace Bento;

public interface IBentoClient
{
    Task<BentoResponse<T>> GetAsync<T>(string endpoint, object? queryParams = null);
    Task<BentoResponse<T>> PostAsync<T>(string endpoint, object? data = null);
}