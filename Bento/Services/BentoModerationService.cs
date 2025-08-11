using System;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for content moderation via Bento API.
/// Uses experimental/content_moderation endpoint (<see href="https://docs.bentonow.com/utility" />).
/// Provides opinionated content moderation optimized for small amounts of text content.
/// Designed to prevent links/XSS/etc inside content and returns safe version of content.
/// </summary>
public class BentoModerationService : IBentoModerationService
{
    private readonly IBentoClient _client;

    public BentoModerationService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Moderate content for safety (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Content moderation request with content to check</param>
    /// <returns>Generic moderation response</returns>
    public Task<BentoResponse<T>> ModerateContentAsync<T>(ContentModerationRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        return _client.PostAsync<T>("experimental/content_moderation", new { content = request.Content });
    }

    /// <summary>
    /// Moderate content for safety
    /// Returns opinionated moderation score based on the content provided
    /// </summary>
    /// <param name="request">Content moderation request with content to check</param>
    /// <returns>Moderation results with valid flag, reasons array, and safe content</returns>
    /// <exception cref="BentoException">Thrown when content moderation fails</exception>
    public async Task<ContentModerationResponse> ModerateContentAsync(ContentModerationRequest request)
    {
        var response = await ModerateContentAsync<ContentModerationResponse>(request);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Content moderation failed", response.StatusCode);
    }
}