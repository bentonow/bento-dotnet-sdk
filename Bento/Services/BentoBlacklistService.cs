using System;
using System.Collections.Generic;
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
public class BentoBlacklistService : IBentoBlacklistService
{
    private readonly IBentoClient _client;

    public BentoBlacklistService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Check blacklist status for domain or IP address (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Blacklist check request with domain and/or IP address</param>
    /// <returns>Generic blacklist check response</returns>
    public Task<BentoResponse<T>> GetBlacklistStatusAsync<T>(BlacklistStatusRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(request.Domain))
            queryParams.Add($"domain={Uri.EscapeDataString(request.Domain)}");
        if (!string.IsNullOrEmpty(request.IpAddress))
            queryParams.Add($"ip={Uri.EscapeDataString(request.IpAddress)}");
            
        var query = string.Join("&", queryParams);
        return _client.GetAsync<T>($"experimental/blacklist.json?{query}");
    }

    /// <summary>
    /// Check blacklist status for domain or IP address
    /// Provide either domain (without protocol) or IPv4 address to check against reputation services
    /// </summary>
    /// <param name="request">Blacklist check request with domain and/or IP address</param>
    /// <returns>Blacklist check results (Results can be null if all checks return false)</returns>
    /// <exception cref="BentoException">Thrown when blacklist check fails</exception>
    public async Task<BlacklistResponse> GetBlacklistStatusAsync(BlacklistStatusRequest request)
    {
        var response = await GetBlacklistStatusAsync<BlacklistResponse[]>(request);
        if (response.Success && response.Data != null && response.Data.Length > 0)
        {
            return response.Data[0];
        }
        throw new BentoException(response.Error ?? "Blacklist check failed", response.StatusCode);
    }
}