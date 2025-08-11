using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for retrieving statistics via Bento API.
/// Uses stats endpoints (<see href="https://docs.bentonow.com/stats" />).
/// Provides site statistics, segment statistics, and report statistics.
/// These endpoints are designed exclusively for backend implementation.
/// </summary>
public interface IBentoStatsService
{
    /// <summary>
    /// Gets site statistics including user, subscriber, and unsubscribed counts.
    /// See <see href="https://docs.bentonow.com/stats#get-site-stats" />.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="request">The site stats request containing site UUID</param>
    /// <returns>A BentoResponse containing the site statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<BentoResponse<T>> GetSiteStatsAsync<T>(SiteStatsRequest request);

    /// <summary>
    /// Gets site statistics including user, subscriber, and unsubscribed counts.
    /// See <see href="https://docs.bentonow.com/stats#get-site-stats" />.
    /// </summary>
    /// <param name="request">The site stats request containing site UUID</param>
    /// <returns>A StatsResponse containing the site statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<StatsResponse> GetSiteStatsAsync(SiteStatsRequest request);

    /// <summary>
    /// Gets segment statistics including user, subscriber, and unsubscribed counts for a specific segment.
    /// See <see href="https://docs.bentonow.com/stats#get-segment-stats" />.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="request">The segment stats request containing site UUID and segment ID</param>
    /// <returns>A BentoResponse containing the segment statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<BentoResponse<T>> GetSegmentStatsAsync<T>(SegmentStatsRequest request);

    /// <summary>
    /// Gets segment statistics including user, subscriber, and unsubscribed counts for a specific segment.
    /// See <see href="https://docs.bentonow.com/stats#get-segment-stats" />.
    /// </summary>
    /// <param name="request">The segment stats request containing site UUID and segment ID</param>
    /// <returns>A StatsResponse containing the segment statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<StatsResponse> GetSegmentStatsAsync(SegmentStatsRequest request);

    /// <summary>
    /// Gets report statistics containing detailed report data with chart style, report type and name.
    /// See <see href="https://docs.bentonow.com/stats#get-report-stats" />.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="request">The report stats request containing site UUID and report ID</param>
    /// <returns>A BentoResponse containing the report statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<BentoResponse<T>> GetReportStatsAsync<T>(ReportStatsRequest request);

    /// <summary>
    /// Gets report statistics containing detailed report data with chart style, report type and name.
    /// See <see href="https://docs.bentonow.com/stats#get-report-stats" />.
    /// </summary>
    /// <param name="request">The report stats request containing site UUID and report ID</param>
    /// <returns>A ReportStatsResponse containing the report statistics</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<ReportStatsResponse> GetReportStatsAsync(ReportStatsRequest request);
}