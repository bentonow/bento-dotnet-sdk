using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Request model for getting report statistics.
/// Used with the GET /v1/stats/report endpoint.
/// See <see href="https://docs.bentonow.com/stats#get-report-stats" />.
/// </summary>
public record ReportStatsRequest
{
    /// <summary>
    /// The UUID of the site to get stats for.
    /// This parameter is required.
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440000</example>
    [Required]
    [JsonPropertyName("site_uuid")]
    public string SiteUuid { get; init; } = string.Empty;

    /// <summary>
    /// The id of the report to get stats for.
    /// This parameter is required.
    /// </summary>
    /// <example>456</example>
    [Required]
    [JsonPropertyName("report_id")]
    public string ReportId { get; init; } = string.Empty;
}
