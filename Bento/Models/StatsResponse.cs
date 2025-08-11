using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Response model containing basic statistics data.
/// Contains user count, subscriber count, and unsubscriber count.
/// Used by site stats and segment stats endpoints.
/// See <see href="https://docs.bentonow.com/stats" />.
/// </summary>
public record StatsResponse
{
    /// <summary>
    /// Total count of users in the account or segment.
    /// </summary>
    [JsonPropertyName("user_count")]
    public int UserCount { get; init; }

    /// <summary>
    /// Active user count for the account or segment.
    /// </summary>
    [JsonPropertyName("subscriber_count")]
    public int SubscriberCount { get; init; }

    /// <summary>
    /// Number of subscribers that have unsubscribed in the account or segment.
    /// </summary>
    [JsonPropertyName("unsubscriber_count")]
    public int UnsubscriberCount { get; init; }
}
