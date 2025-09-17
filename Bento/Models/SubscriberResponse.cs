using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Response model for subscriber data from Bento API.
/// Contains subscriber information including UUID, email, fields and tags.
/// </summary>
public record SubscriberResponse
{
    /// <summary>
    /// Unique identifier for the subscriber
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }
    
    /// <summary>
    /// Type of the record (usually "visitors")
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }
    
    /// <summary>
    /// Subscriber attributes containing detailed information
    /// </summary>
    [JsonPropertyName("attributes")]
    public SubscriberAttributes? Attributes { get; init; }
}

/// <summary>
/// Subscriber attributes containing detailed subscriber information
/// </summary>
public record SubscriberAttributes
{
    /// <summary>
    /// Unique UUID for the subscriber
    /// </summary>
    [JsonPropertyName("uuid")]
    public string? Uuid { get; init; }
    
    /// <summary>
    /// Email address of the subscriber
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }
    
    /// <summary>
    /// Custom fields associated with the subscriber
    /// </summary>
    [JsonPropertyName("fields")]
    public Dictionary<string, object>? Fields { get; init; }
    
    /// <summary>
    /// Array of cached tag IDs associated with the subscriber
    /// </summary>
    [JsonPropertyName("cached_tag_ids")]
    public IEnumerable<string>? CachedTagIds { get; init; }
    
    /// <summary>
    /// Date when the subscriber was unsubscribed (null if subscribed)
    /// </summary>
    [JsonPropertyName("unsubscribed_at")]
    public string? UnsubscribedAt { get; init; }
    
    /// <summary>
    /// Navigation URL for the subscriber in Bento interface
    /// </summary>
    [JsonPropertyName("navigation_url")]
    public string? NavigationUrl { get; init; }
}

/// <summary>
/// Response for search subscribers operation containing paginated results
/// </summary>
public record SearchSubscribersResponse
{
    /// <summary>
    /// Array of subscriber data matching the search criteria
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<SubscriberResponse>? Data { get; init; }
    
    /// <summary>
    /// Metadata about the search including pagination and query info
    /// </summary>
    [JsonPropertyName("meta")]
    public SearchMetadata? Meta { get; init; }
}

/// <summary>
/// Metadata for search results including pagination and query details
/// </summary>
public record SearchMetadata
{
    /// <summary>
    /// Current page number
    /// </summary>
    [JsonPropertyName("page")]
    public int Page { get; init; }
    
    /// <summary>
    /// Query parameters used for the search
    /// </summary>
    [JsonPropertyName("query")]
    public object? Query { get; init; }
}

/// <summary>
/// Response for batch import operations containing result count
/// </summary>
public record ImportSubscribersResponse
{
    /// <summary>
    /// Number of subscribers successfully processed
    /// </summary>
    [JsonPropertyName("result")]
    public int Result { get; init; }
}
