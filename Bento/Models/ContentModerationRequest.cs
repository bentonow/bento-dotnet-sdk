namespace Bento.Models;

/// <summary>
/// Request model for content moderation.
/// </summary>
/// <param name="Content">The content to moderate for safety (optimized for small text content)</param>
public record ContentModerationRequest(string Content);