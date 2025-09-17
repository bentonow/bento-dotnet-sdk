using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for content moderation via Bento API.
/// Uses experimental/content_moderation endpoint (<see href="https://docs.bentonow.com/utility" />).
/// Provides opinionated content moderation optimized for small amounts of text content.
/// Designed to prevent links/XSS/etc inside content and returns safe version of content.
/// </summary>
public interface IBentoModerationService
{
    /// <summary>
    /// Moderate content for safety (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Content moderation request with content to check</param>
    /// <returns>Generic moderation response</returns>
    Task<BentoResponse<T>> ModerateContentAsync<T>(ContentModerationRequest request);
    
    /// <summary>
    /// Moderate content for safety
    /// Returns opinionated moderation score based on the content provided
    /// </summary>
    /// <param name="request">Content moderation request with content to check</param>
    /// <returns>Moderation results with valid flag, reasons array, and safe content</returns>
    Task<ContentModerationResponse> ModerateContentAsync(ContentModerationRequest request);
}