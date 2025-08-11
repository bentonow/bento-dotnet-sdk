using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Request model for getting segment statistics.
/// Used with the GET /v1/stats/segment endpoint.
/// See <see href="https://docs.bentonow.com/stats#get-segment-stats" />.
/// </summary>
public record SegmentStatsRequest
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
    /// The id of the segment to get stats for.
    /// This parameter is required.
    /// </summary>
    /// <example>123</example>
    [Required]
    [JsonPropertyName("segment_id")]
    public string SegmentId { get; init; } = string.Empty;
}
