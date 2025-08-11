using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for checking domains and IP addresses against blacklists via Bento API.
/// Uses experimental/blacklist.json endpoint (<see href="https://docs.bentonow.com/utility#the-blacklist-model" />).
/// Returns reputation check results from multiple providers: Spamhaus, Nordspam, Spfbl, Sorbs, Abusix.
/// </summary>
public interface IBentoBlacklistService
{
    /// <summary>
    /// Check blacklist status for domain or IP address (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Blacklist check request</param>
    /// <returns>Blacklist check response</returns>
    Task<BentoResponse<T>> GetBlacklistStatusAsync<T>(BlacklistStatusRequest request);
    
    /// <summary>
    /// Check blacklist status for domain or IP address
    /// </summary>
    /// <param name="request">Blacklist check request</param>
    /// <returns>Blacklist check results</returns>
    Task<BlacklistResponse> GetBlacklistStatusAsync(BlacklistStatusRequest request);
}