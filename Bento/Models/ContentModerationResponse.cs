namespace Bento.Models;

/// <summary>
/// Response model for content moderation.
/// Returns safety assessment and cleaned content.
/// </summary>
public class ContentModerationResponse
{
    /// <summary>
    /// Whether the content is considered safe
    /// </summary>
    public bool Valid { get; set; }
    
    /// <summary>
    /// Issues found with the content (can be empty array)
    /// </summary>
    public string[]? Reasons { get; set; }
    
    /// <summary>
    /// Safe version of the original content
    /// </summary>
    public string? SafeOriginalContent { get; set; }
}
