using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for managing tags via Bento API.
/// Uses tags endpoints (<see href="https://docs.bentonow.com/tags_api" />).
/// Tags are essential data labels that allow you to monitor subscriber journeys,
/// segment users, and create targeted marketing strategies.
/// </summary>
public class BentoTagService : IBentoTagService
{
    private readonly IBentoClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="BentoTagService"/> class.
    /// </summary>
    /// <param name="client">The Bento client instance</param>
    /// <exception cref="ArgumentNullException">Thrown when client is null</exception>
    public BentoTagService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Gets all tags from your Bento account.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response</returns>
    public Task<BentoResponse<T>> GetTagsAsync<T>()
    {
        return _client.GetAsync<T>("fetch/tags");
    }

    /// <summary>
    /// Gets all tags from your Bento account.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the list of tags</returns>
    public async Task<List<TagResponse>?> GetTagsAsync()
    {
        try
        {
            var response = await _client.GetAsync<dynamic>("fetch/tags");
            
            if (!response.Success || response.Data?.data == null)
                return null;

            var tags = new List<TagResponse>();
            foreach (var item in response!.Data!.data!)
            {
                tags.Add(ParseTagResponse(item));
            }

            return tags;
        }
        catch (Exception ex)
        {
            throw new BentoException("Failed to retrieve tags", ex);
        }
    }

    /// <summary>
    /// Creates a new tag in your Bento account.
    /// Only a single tag can be created at a time.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="request">The tag creation request containing the tag name</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    public Task<BentoResponse<T>> CreateTagAsync<T>(TagRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        var payload = new { tag = new { name = request.Name } };
        return _client.PostAsync<T>("fetch/tags", payload);
    }

    /// <summary>
    /// Creates a new tag in your Bento account.
    /// Only a single tag can be created at a time.
    /// </summary>
    /// <param name="request">The tag creation request containing the tag name</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created tag details</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null</exception>
    public async Task<TagResponse?> CreateTagAsync(TagRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        try
        {
            var payload = new { tag = new { name = request.Name } };
            var response = await _client.PostAsync<dynamic>("fetch/tags", payload);
            
            if (!response.Success || response.Data?.data == null)
                return null;

            // Check if data is an array or single object
            dynamic dataArray = response.Data.data;
            var tagData = dataArray?.FirstOrDefault();
            if (tagData == null)
                return null;

            return ParseTagResponse(tagData);
        }
        catch (Exception ex)
        {
            throw new BentoException("Failed to create tag", ex);
        }
    }

    /// <summary>
    /// Parses a dynamic tag object into a TagResponse model.
    /// </summary>
    /// <param name="tagData">The dynamic tag data from API response</param>
    /// <returns>A TagResponse object</returns>
    private static TagResponse ParseTagResponse(dynamic tagData)
    {
        var createdAtStr = tagData.attributes?.created_at?.ToString();
        var discardedAtStr = tagData.attributes?.discarded_at?.ToString();
        
        DateTime? createdAt = null;
        DateTime? discardedAt = null;
        
        if (!string.IsNullOrEmpty(createdAtStr))
        {
            if (DateTime.TryParse(createdAtStr, out DateTime parsedCreated))
                createdAt = parsedCreated;
        }
                
        if (!string.IsNullOrEmpty(discardedAtStr))
        {
            if (DateTime.TryParse(discardedAtStr, out DateTime parsedDiscarded))
                discardedAt = parsedDiscarded;
        }

        return new TagResponse
        {
            Id = tagData.id?.ToString(),
            Type = tagData.type?.ToString(),
            Attributes = new TagAttributes
            {
                Name = tagData.attributes?.name?.ToString(),
                CreatedAt = createdAt,
                DiscardedAt = discardedAt
            }
        };
    }
}