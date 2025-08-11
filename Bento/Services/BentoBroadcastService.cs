using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Implementation of broadcast service for managing broadcast campaigns via Bento API.
/// Provides functionality to create and retrieve broadcast campaigns.
/// </summary>
public class BentoBroadcastService : IBentoBroadcastService
{
    private readonly IBentoClient _client;

    /// <summary>
    /// Initializes a new instance of the BentoBroadcastService class.
    /// </summary>
    /// <param name="client">The Bento client for API communication.</param>
    /// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
    public BentoBroadcastService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> GetBroadcastsAsync<T>(int? page = null, string? siteUuid = null)
    {
        var endpoint = "fetch/broadcasts";
        var queryParams = new List<string>();
        
        if (page.HasValue)
        {
            queryParams.Add($"page={page.Value}");
        }
        
        if (!string.IsNullOrWhiteSpace(siteUuid))
        {
            queryParams.Add($"site_uuid={Uri.EscapeDataString(siteUuid)}");
        }
        
        if (queryParams.Any())
        {
            endpoint += "?" + string.Join("&", queryParams);
        }
        
        return _client.GetAsync<T>(endpoint);
    }

    /// <inheritdoc />
    public async Task<BroadcastListResponse> GetBroadcastsAsync(int? page = null, string? siteUuid = null)
    {
        var response = await GetBroadcastsAsync<BroadcastListResponse>(page, siteUuid);
        
        if (!response.Success || response.Data == null)
        {
            throw new BentoException("Failed to retrieve broadcasts: " + (response.Error ?? "Unknown error"), response.StatusCode);
        }
        
        return response.Data;
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> CreateBroadcastAsync<T>(BroadcastRequest broadcast)
    {
        if (broadcast == null) throw new ArgumentNullException(nameof(broadcast));
        
        return CreateBatchBroadcastsAsync<T>(new[] { broadcast });
    }

    /// <inheritdoc />
    public async Task<BatchBroadcastResponse> CreateBroadcastAsync(BroadcastRequest broadcast)
    {
        if (broadcast == null) throw new ArgumentNullException(nameof(broadcast));
        
        var response = await CreateBatchBroadcastsAsync<BatchBroadcastResponse>(new[] { broadcast });
        
        if (!response.Success || response.Data == null)
        {
            throw new BentoException("Failed to create broadcast: " + (response.Error ?? "Unknown error"), response.StatusCode);
        }
        
        return response.Data;
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> CreateBatchBroadcastsAsync<T>(IEnumerable<BroadcastRequest> broadcasts)
    {
        if (broadcasts == null) throw new ArgumentNullException(nameof(broadcasts));
        
        var broadcastsList = broadcasts.ToList();
        if (!broadcastsList.Any())
            throw new ArgumentException("Broadcasts collection cannot be empty", nameof(broadcasts));

        var request = new
        {
            broadcasts = broadcastsList.Select(b => new
            {
                name = b.Name,
                subject = b.Subject,
                content = b.Content,
                type = b.Type,
                from = new
                {
                    email = b.From.EmailAddress,
                    name = b.From.Name
                },
                inclusive_tags = b.InclusiveTags,
                exclusive_tags = b.ExclusiveTags,
                segment_id = b.SegmentId,
                batch_size_per_hour = b.BatchSizePerHour,
                send_at = b.SendAt?.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                approved = b.Approved
            })
        };

        return _client.PostAsync<T>("batch/broadcasts", request);
    }

    /// <inheritdoc />
    public async Task<BatchBroadcastResponse> CreateBatchBroadcastsAsync(IEnumerable<BroadcastRequest> broadcasts)
    {
        if (broadcasts == null) throw new ArgumentNullException(nameof(broadcasts));
        
        var response = await CreateBatchBroadcastsAsync<BatchBroadcastResponse>(broadcasts);
        
        if (!response.Success || response.Data == null)
        {
            throw new BentoException("Failed to create batch broadcasts: " + (response.Error ?? "Unknown error"), response.StatusCode);
        }
        
        return response.Data;
    }
}