using System.Collections.Generic;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for managing subscribers via Bento API.
/// Provides functionality for creating, finding, searching, importing subscribers and running commands.
/// Uses subscribers endpoints (<see href="https://docs.bentonow.com/subscribers" />).
/// 
/// Main capabilities:
/// - Find individual subscribers by email or UUID
/// - Create new subscribers  
/// - Import multiple subscribers (1-1000 per request)
/// - Search subscribers with date-based filtering (Enterprise only)
/// - Run commands to modify subscriber data (add/remove tags, fields, subscribe/unsubscribe, etc.)
/// </summary>
public interface IBentoSubscriberService
{
    /// <summary>
    /// Find a subscriber by email address or UUID.
    /// Returns the subscriber details if found.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="email">Email address to search for</param>
    /// <param name="uuid">Optional UUID to search for instead of email</param>
    /// <returns>Generic response containing subscriber data</returns>
    Task<BentoResponse<T>> FindSubscriberAsync<T>(string email, string? uuid = null);
    
    /// <summary>
    /// Find a subscriber by email address or UUID.
    /// Returns the subscriber details if found.
    /// </summary>
    /// <param name="email">Email address to search for</param>
    /// <param name="uuid">Optional UUID to search for instead of email</param>
    /// <returns>Subscriber data if found</returns>
    /// <exception cref="BentoException">Thrown when request fails</exception>
    Task<SubscriberResponse> FindSubscriberAsync(string email, string? uuid = null);
    
    /// <summary>
    /// Find a subscriber using a structured request.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="request">Request containing email or UUID to search for</param>
    /// <returns>Generic response containing subscriber data</returns>
    Task<BentoResponse<T>> FindSubscriberAsync<T>(FindSubscriberRequest request);
    
    /// <summary>
    /// Find a subscriber using a structured request.
    /// </summary>
    /// <param name="request">Request containing email or UUID to search for</param>
    /// <returns>Subscriber data if found</returns>
    /// <exception cref="BentoException">Thrown when request fails</exception>
    Task<SubscriberResponse> FindSubscriberAsync(FindSubscriberRequest request);

    /// <summary>
    /// Create a new subscriber and queue them for indexing.
    /// Triggers flows and automations.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="subscriber">Subscriber data to create</param>
    /// <returns>Generic response containing created subscriber data</returns>
    Task<BentoResponse<T>> CreateSubscriberAsync<T>(SubscriberRequest subscriber);
    
    /// <summary>
    /// Create a new subscriber and queue them for indexing.
    /// Triggers flows and automations.
    /// </summary>
    /// <param name="subscriber">Subscriber data to create</param>
    /// <returns>Created subscriber data</returns>
    /// <exception cref="BentoException">Thrown when creation fails</exception>
    Task<SubscriberResponse> CreateSubscriberAsync(SubscriberRequest subscriber);

    /// <summary>
    /// Import multiple subscribers (1-1000 per request).
    /// Uses background import processes and does not trigger flows/automations.
    /// Updates may take up to 5 minutes to reflect.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="subscribers">Collection of subscribers to import</param>
    /// <returns>Generic response containing import result count</returns>
    Task<BentoResponse<T>> ImportSubscribersAsync<T>(IEnumerable<SubscriberRequest> subscribers);
    
    /// <summary>
    /// Import multiple subscribers (1-1000 per request).
    /// Uses background import processes and does not trigger flows/automations.
    /// Updates may take up to 5 minutes to reflect.
    /// </summary>
    /// <param name="subscribers">Collection of subscribers to import</param>
    /// <returns>Import result with count of processed subscribers</returns>
    /// <exception cref="BentoException">Thrown when import fails</exception>
    Task<ImportSubscribersResponse> ImportSubscribersAsync(IEnumerable<SubscriberRequest> subscribers);

    /// <summary>
    /// Search subscribers with date-based filtering and pagination.
    /// Returns the most recent 100 subscribers with filtering options.
    /// Note: This endpoint is limited to Bento Enterprise customers only.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="request">Search criteria including page number and date filters</param>
    /// <returns>Generic response containing search results</returns>
    Task<BentoResponse<T>> SearchSubscribersAsync<T>(SearchSubscribersRequest request);
    
    /// <summary>
    /// Search subscribers with date-based filtering and pagination.
    /// Returns the most recent 100 subscribers with filtering options.
    /// Note: This endpoint is limited to Bento Enterprise customers only.
    /// </summary>
    /// <param name="request">Search criteria including page number and date filters</param>
    /// <returns>Search results with subscriber data and pagination metadata</returns>
    /// <exception cref="BentoException">Thrown when search fails</exception>
    Task<SearchSubscribersResponse> SearchSubscribersAsync(SearchSubscribersRequest request);

    /// <summary>
    /// Execute a single command to modify subscriber data.
    /// Commands include: add_tag, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email.
    /// Note: This endpoint has delayed response and is not recommended for most use cases.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="command">Command to execute</param>
    /// <returns>Generic response containing updated subscriber data</returns>
    Task<BentoResponse<T>> RunCommandAsync<T>(CommandRequest command);
    
    /// <summary>
    /// Execute a single command to modify subscriber data.
    /// Commands include: add_tag, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email.
    /// Note: This endpoint has delayed response and is not recommended for most use cases.
    /// </summary>
    /// <param name="command">Command to execute</param>
    /// <returns>Updated subscriber data</returns>
    /// <exception cref="BentoException">Thrown when command execution fails</exception>
    Task<SubscriberResponse> RunCommandAsync(CommandRequest command);

    /// <summary>
    /// Execute multiple commands to modify subscriber data in batch.
    /// Commands include: add_tag, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email.
    /// Note: This endpoint has delayed response and is not recommended for most use cases.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="commands">Collection of commands to execute</param>
    /// <returns>Generic response containing updated subscriber data</returns>
    Task<BentoResponse<T>> RunCommandsAsync<T>(IEnumerable<CommandRequest> commands);
    
    /// <summary>
    /// Execute multiple commands to modify subscriber data in batch.
    /// Commands include: add_tag, remove_tag, add_field, remove_field, subscribe, unsubscribe, change_email.
    /// Note: This endpoint has delayed response and is not recommended for most use cases.
    /// </summary>
    /// <param name="commands">Collection of commands to execute</param>
    /// <returns>Updated subscriber data</returns>
    /// <exception cref="BentoException">Thrown when command execution fails</exception>
    Task<SubscriberResponse> RunCommandsAsync(IEnumerable<CommandRequest> commands);
}