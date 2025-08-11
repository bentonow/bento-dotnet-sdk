using System.ComponentModel.DataAnnotations;

namespace Bento.Models;

/// <summary>
/// Request model for getting site statistics.
/// Used with the GET /v1/stats/site endpoint.
/// See <see href="https://docs.bentonow.com/stats#get-site-stats" />.
/// </summary>
public record SiteStatsRequest
{
    /// <summary>
    /// The UUID of the site to get stats for.
    /// This parameter is required.
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440000</example>
    [Required]
    public string SiteUuid { get; init; } = string.Empty;
}
