namespace Bento.Models;

/// <summary>
/// Request model for searching subscribers with date-based filtering.
/// Used with the Search Subscribers API endpoint.
/// See <see href="https://docs.bentonow.com/subscribers#search-subscribers" /> for details.
/// Note: This endpoint is limited to Bento Enterprise customers only.
/// </summary>
public record SearchSubscribersRequest
{
    /// <summary>
    /// Page number for the search (required)
    /// </summary>
    public int Page { get; init; } = 1;
    
    /// <summary>
    /// Filter by creation date of the subscriber (optional)
    /// </summary>
    public DateFilter? CreatedAt { get; init; }
    
    /// <summary>
    /// Filter by update date of the subscriber (optional)
    /// </summary>
    public DateFilter? UpdatedAt { get; init; }
    
    /// <summary>
    /// Filter by last event date of the subscriber (optional)
    /// </summary>
    public DateFilter? LastEventAt { get; init; }
    
    /// <summary>
    /// Filter by unsubscription date of the subscriber (optional)
    /// </summary>
    public DateFilter? UnsubscribedAt { get; init; }
}

/// <summary>
/// Date filter for subscriber search with greater than and less than conditions
/// </summary>
public record DateFilter
{
    /// <summary>
    /// Greater than date string (ISO format)
    /// </summary>
    public string? Gt { get; init; }
    
    /// <summary>
    /// Less than date string (ISO format)
    /// </summary>
    public string? Lt { get; init; }
}

/// <summary>
/// Request model for finding a specific subscriber by email or UUID
/// </summary>
public record FindSubscriberRequest
{
    /// <summary>
    /// Email address to search for a match (required if UUID not provided)
    /// </summary>
    public string? Email { get; init; }
    
    /// <summary>
    /// UUID of the subscriber (optional, can be used in place of email)
    /// </summary>
    public string? Uuid { get; init; }
}
