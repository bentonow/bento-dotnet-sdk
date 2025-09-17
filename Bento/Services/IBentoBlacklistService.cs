using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for checking domains and IP addresses against blacklists via Bento API.
/// Uses experimental/blacklist.json endpoint (<see href="https://docs.bentonow.com/utility" />).
/// Validates IP or domain name with industry email reputation services to check for delivery issues.
/// Returns reputation check results from multiple providers: Spamhaus, Nordspam, Spfbl, Sorbs, Abusix.
/// A true value for any provider indicates a problem with the domain/IP.
/// </summary>
public interface IBentoBlacklistService
{
    /// <summary>
    /// Check blacklist status for domain or IP address (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Blacklist check request with domain and/or IP address</param>
    /// <returns>Generic blacklist check response</returns>
    Task<BentoResponse<T>> GetBlacklistStatusAsync<T>(BlacklistStatusRequest request);
    
    /// <summary>
    /// Check blacklist status for domain or IP address
    /// Provide either domain (without protocol) or IPv4 address to check against reputation services
    /// </summary>
    /// <param name="request">Blacklist check request with domain and/or IP address</param>
    /// <returns>Blacklist check results (Results can be null if all checks return false)</returns>
    Task<BlacklistResponse> GetBlacklistStatusAsync(BlacklistStatusRequest request);
}