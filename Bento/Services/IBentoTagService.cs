using System.Collections.Generic;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for managing tags via Bento API.
/// Uses tags endpoints (<see href="https://docs.bentonow.com/tags_api" />).
/// Tags are essential data labels that allow you to monitor subscriber journeys,
/// segment users, and create targeted marketing strategies.
/// </summary>
public interface IBentoTagService
{
    /// <summary>
    /// Gets all tags from your Bento account.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response</returns>
    Task<BentoResponse<T>> GetTagsAsync<T>();

    /// <summary>
    /// Gets all tags from your Bento account.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of tags</returns>
    Task<List<TagResponse>?> GetTagsAsync();

    /// <summary>
    /// Creates a new tag in your Bento account.
    /// Only a single tag can be created at a time.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="request">The tag creation request containing the tag name</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response</returns>
    Task<BentoResponse<T>> CreateTagAsync<T>(TagRequest request);

    /// <summary>
    /// Creates a new tag in your Bento account.
    /// Only a single tag can be created at a time.
    /// </summary>
    /// <param name="request">The tag creation request containing the tag name</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created tag details</returns>
    Task<TagResponse?> CreateTagAsync(TagRequest request);
}