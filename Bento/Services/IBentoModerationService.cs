using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for content moderation via Bento API.
/// Uses experimental/content_moderation endpoint (<see href="https://docs.bentonow.com/utility#the-moderate-content-model" />).
/// Returns opinionated moderation score optimized for small text content to prevent links/XSS/etc.
/// </summary>
public interface IBentoModerationService
{
    /// <summary>
    /// Moderate content for safety (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Content moderation request</param>
    /// <returns>Moderation response</returns>
    Task<BentoResponse<T>> ModerateContentAsync<T>(ContentModerationRequest request);
    
    /// <summary>
    /// Moderate content for safety
    /// </summary>
    /// <param name="request">Content moderation request</param>
    /// <returns>Moderation results</returns>
    Task<ContentModerationResponse> ModerateContentAsync(ContentModerationRequest request);
}