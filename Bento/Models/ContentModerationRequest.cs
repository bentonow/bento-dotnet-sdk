using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Request model for content moderation using experimental/content_moderation endpoint.
/// This endpoint returns an opinionated moderation score based on the content provided.
/// Optimized for small amounts of text content to prevent links/XSS/etc inside it.
/// </summary>
/// <param name="Content">The content you wish to moderate</param>
public record ContentModerationRequest(
    [property: JsonPropertyName("content")] string Content
);