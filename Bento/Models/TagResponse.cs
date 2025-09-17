using System;
using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Represents the response model for tag operations from Bento API.
/// Contains tag information including its unique identifier, name, and timestamps.
/// </summary>
public record TagResponse
{
    /// <summary>
    /// Gets the unique identifier of the tag.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Gets the type of the resource (typically "tags").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>
    /// Gets the tag attributes containing detailed information.
    /// </summary>
    [JsonPropertyName("attributes")]
    public TagAttributes? Attributes { get; init; }
}

/// <summary>
/// Represents the attributes of a tag containing detailed information.
/// </summary>
public record TagAttributes
{
    /// <summary>
    /// Gets the name of the tag.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Gets the creation timestamp of the tag.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; init; }

    /// <summary>
    /// Gets the timestamp when the tag was discarded.
    /// This value is null if the tag is still active.
    /// </summary>
    [JsonPropertyName("discarded_at")]
    public DateTime? DiscardedAt { get; init; }
}
