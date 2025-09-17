using System.Collections.Generic;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for managing broadcast campaigns via Bento API.
/// Uses broadcasts endpoints (<see href="https://docs.bentonow.com/broadcasts" />).
/// Allows creating and retrieving broadcast campaigns for large-scale communications
/// designed to engage a wide audience, targeting either all subscribers or specific segments.
/// </summary>
public interface IBentoBroadcastService
{
    /// <summary>
    /// Gets a list of broadcasts in your account with pagination support.
    /// </summary>
    /// <typeparam name="T">Type for generic response deserialization.</typeparam>
    /// <param name="page">Pagination number. Optional parameter for paginating through broadcasts.</param>
    /// <param name="siteUuid">Your site UUID. Optional parameter for filtering by site.</param>
    /// <returns>Generic response containing broadcasts data.</returns>
    Task<BentoResponse<T>> GetBroadcastsAsync<T>(int? page = null, string? siteUuid = null);

    /// <summary>
    /// Gets a list of broadcasts in your account with pagination support.
    /// </summary>
    /// <param name="page">Pagination number. Optional parameter for paginating through broadcasts.</param>
    /// <param name="siteUuid">Your site UUID. Optional parameter for filtering by site.</param>
    /// <returns>Typed response containing broadcasts list.</returns>
    Task<BroadcastListResponse> GetBroadcastsAsync(int? page = null, string? siteUuid = null);

    /// <summary>
    /// Creates a single broadcast campaign.
    /// </summary>
    /// <typeparam name="T">Type for generic response deserialization.</typeparam>
    /// <param name="broadcast">Broadcast request data containing campaign details.</param>
    /// <returns>Generic response containing creation result.</returns>
    Task<BentoResponse<T>> CreateBroadcastAsync<T>(BroadcastRequest broadcast);

    /// <summary>
    /// Creates a single broadcast campaign.
    /// </summary>
    /// <param name="broadcast">Broadcast request data containing campaign details.</param>
    /// <returns>Typed response containing the count of broadcasts queued for processing.</returns>
    Task<BatchBroadcastResponse> CreateBroadcastAsync(BroadcastRequest broadcast);

    /// <summary>
    /// Creates multiple broadcast campaigns in a single batch operation.
    /// </summary>
    /// <typeparam name="T">Type for generic response deserialization.</typeparam>
    /// <param name="broadcasts">Collection of broadcast requests to create.</param>
    /// <returns>Generic response containing batch creation result.</returns>
    Task<BentoResponse<T>> CreateBatchBroadcastsAsync<T>(IEnumerable<BroadcastRequest> broadcasts);

    /// <summary>
    /// Creates multiple broadcast campaigns in a single batch operation.
    /// </summary>
    /// <param name="broadcasts">Collection of broadcast requests to create.</param>
    /// <returns>Typed response containing the count of broadcasts queued for processing.</returns>
    Task<BatchBroadcastResponse> CreateBatchBroadcastsAsync(IEnumerable<BroadcastRequest> broadcasts);
}