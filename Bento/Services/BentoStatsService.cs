using System;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for retrieving statistics via Bento API.
/// Uses stats endpoints (<see href="https://docs.bentonow.com/stats" />).
/// Provides site statistics, segment statistics, and report statistics.
/// These endpoints are designed exclusively for backend implementation.
/// </summary>
public class BentoStatsService : IBentoStatsService
{
    private readonly IBentoClient _client;

    /// <summary>
    /// Initializes a new instance of the BentoStatsService.
    /// </summary>
    /// <param name="client">The Bento client to use for API requests</param>
    /// <exception cref="ArgumentNullException">Thrown when client is null</exception>
    public BentoStatsService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Gets site statistics including user, subscriber, and unsubscribed counts.
    /// See <see href="https://docs.bentonow.com/stats#get-site-stats" />.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="request">The site stats request containing site UUID</param>
    /// <returns>A BentoResponse containing the site statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public Task<BentoResponse<T>> GetSiteStatsAsync<T>(SiteStatsRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var parameters = new { site_uuid = Uri.EscapeDataString(request.SiteUuid) };
        return _client.GetAsync<T>("stats/site", parameters);
    }

    /// <summary>
    /// Gets site statistics including user, subscriber, and unsubscribed counts.
    /// See <see href="https://docs.bentonow.com/stats#get-site-stats" />.
    /// </summary>
    /// <param name="request">The site stats request containing site UUID</param>
    /// <returns>A StatsResponse containing the site statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public async Task<StatsResponse> GetSiteStatsAsync(SiteStatsRequest request)
    {
        var response = await GetSiteStatsAsync<StatsResponse>(request);
        return response.Data ?? new StatsResponse();
    }

    /// <summary>
    /// Gets segment statistics including user, subscriber, and unsubscribed counts for a specific segment.
    /// See <see href="https://docs.bentonow.com/stats#get-segment-stats" />.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="request">The segment stats request containing site UUID and segment ID</param>
    /// <returns>A BentoResponse containing the segment statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public Task<BentoResponse<T>> GetSegmentStatsAsync<T>(SegmentStatsRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var parameters = new 
        { 
            site_uuid = Uri.EscapeDataString(request.SiteUuid), 
            segment_id = Uri.EscapeDataString(request.SegmentId) 
        };
        return _client.GetAsync<T>("stats/segment", parameters);
    }

    /// <summary>
    /// Gets segment statistics including user, subscriber, and unsubscribed counts for a specific segment.
    /// See <see href="https://docs.bentonow.com/stats#get-segment-stats" />.
    /// </summary>
    /// <param name="request">The segment stats request containing site UUID and segment ID</param>
    /// <returns>A StatsResponse containing the segment statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public async Task<StatsResponse> GetSegmentStatsAsync(SegmentStatsRequest request)
    {
        var response = await GetSegmentStatsAsync<StatsResponse>(request);
        return response.Data ?? new StatsResponse();
    }

    /// <summary>
    /// Gets report statistics containing detailed report data with chart style, report type and name.
    /// See <see href="https://docs.bentonow.com/stats#get-report-stats" />.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="request">The report stats request containing site UUID and report ID</param>
    /// <returns>A BentoResponse containing the report statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public Task<BentoResponse<T>> GetReportStatsAsync<T>(ReportStatsRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var parameters = new 
        { 
            site_uuid = Uri.EscapeDataString(request.SiteUuid), 
            report_id = Uri.EscapeDataString(request.ReportId) 
        };
        return _client.GetAsync<T>("stats/report", parameters);
    }

    /// <summary>
    /// Gets report statistics containing detailed report data with chart style, report type and name.
    /// See <see href="https://docs.bentonow.com/stats#get-report-stats" />.
    /// </summary>
    /// <param name="request">The report stats request containing site UUID and report ID</param>
    /// <returns>A ReportStatsResponse containing the report statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public async Task<ReportStatsResponse> GetReportStatsAsync(ReportStatsRequest request)
    {
        var response = await GetReportStatsAsync<ReportStatsResponse>(request);
        return response.Data ?? new ReportStatsResponse();
    }
}