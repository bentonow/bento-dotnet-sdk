using System.Collections.Generic;

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
    public string? Id { get; init; }
    
    /// <summary>
    /// Type of the record (usually "visitors")
    /// </summary>
    public string? Type { get; init; }
    
    /// <summary>
    /// Subscriber attributes containing detailed information
    /// </summary>
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
    public string? Uuid { get; init; }
    
    /// <summary>
    /// Email address of the subscriber
    /// </summary>
    public string? Email { get; init; }
    
    /// <summary>
    /// Custom fields associated with the subscriber
    /// </summary>
    public Dictionary<string, object>? Fields { get; init; }
    
    /// <summary>
    /// Array of cached tag IDs associated with the subscriber
    /// </summary>
    public IEnumerable<string>? CachedTagIds { get; init; }
    
    /// <summary>
    /// Date when the subscriber was unsubscribed (null if subscribed)
    /// </summary>
    public string? UnsubscribedAt { get; init; }
    
    /// <summary>
    /// Navigation URL for the subscriber in Bento interface
    /// </summary>
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
    public IEnumerable<SubscriberResponse>? Data { get; init; }
    
    /// <summary>
    /// Metadata about the search including pagination and query info
    /// </summary>
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
    public int Page { get; init; }
    
    /// <summary>
    /// Query parameters used for the search
    /// </summary>
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
    public int Result { get; init; }
}
