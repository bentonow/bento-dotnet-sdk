namespace Bento.Models;

/// <summary>
/// Response model for content moderation using experimental/content_moderation endpoint.
/// Returns opinionated moderation score based on the content provided.
/// Optimized for small amounts of text content to prevent links/XSS/etc.
/// </summary>
public class ContentModerationResponse
{
    /// <summary>
    /// True/false flag if the supplied content is considered safe
    /// False indicates the content contains issues that were flagged
    /// </summary>
    public bool Valid { get; set; }
    
    /// <summary>
    /// Array of string values describing the issues found with the supplied content
    /// Contains specific reasons why the content was flagged (can be empty array if valid)
    /// </summary>
    public string[]? Reasons { get; set; }
    
    /// <summary>
    /// A safe version of the supplied content with harmful elements removed
    /// Contains cleaned content that's safe to use
    /// </summary>
    public string? SafeOriginalContent { get; set; }
}
