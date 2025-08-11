using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Implementation of event tracking service for Bento API.
/// Handles single and batch event creation via the batch/events endpoint.
/// </summary>
public class BentoEventService : IBentoEventService
{
    private readonly IBentoClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="BentoEventService"/> class.
    /// </summary>
    /// <param name="client">The Bento HTTP client</param>
    /// <exception cref="ArgumentNullException">Thrown when client is null</exception>
    public BentoEventService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Tracks a single event for a user.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="eventData">The event data to track</param>
    /// <returns>Generic response containing the event tracking result</returns>
    /// <exception cref="ArgumentNullException">Thrown when eventData is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public Task<BentoResponse<T>> TrackEventAsync<T>(EventRequest eventData)
    {
        if (eventData == null) throw new ArgumentNullException(nameof(eventData));
        
        return TrackEventsAsync<T>(new[] { eventData });
    }

    /// <summary>
    /// Tracks a single event for a user.
    /// </summary>
    /// <param name="eventData">The event data to track</param>
    /// <returns>Typed response containing the event tracking result</returns>
    /// <exception cref="ArgumentNullException">Thrown when eventData is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public async Task<EventResponse> TrackEventAsync(EventRequest eventData)
    {
        if (eventData == null) throw new ArgumentNullException(nameof(eventData));
        
        var response = await TrackEventsAsync<EventResponse>(new[] { eventData });
        return response.Data ?? throw new BentoException("Response data is null");
    }

    /// <summary>
    /// Tracks multiple events in a batch (up to 1000 events).
    /// This is the most efficient way to send multiple events.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="events">Collection of events to track (max 1000)</param>
    /// <returns>Generic response containing the batch event tracking result</returns>
    /// <exception cref="ArgumentNullException">Thrown when events is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public Task<BentoResponse<T>> TrackEventsAsync<T>(IEnumerable<EventRequest> events)
    {
        if (events == null) throw new ArgumentNullException(nameof(events));

        var request = new
        {
            events = events.Select(e => new
            {
                type = e.Type,
                email = e.Email,
                fields = e.Fields?.Count > 0 ? e.Fields : null,
                details = e.Details?.Count > 0 ? e.Details : null
            })
        };

        return _client.PostAsync<T>("batch/events", request);
    }

    /// <summary>
    /// Tracks multiple events in a batch (up to 1000 events).
    /// This is the most efficient way to send multiple events.
    /// </summary>
    /// <param name="events">Collection of events to track (max 1000)</param>
    /// <returns>Typed response containing the batch event tracking result</returns>
    /// <exception cref="ArgumentNullException">Thrown when events is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    public async Task<EventResponse> TrackEventsAsync(IEnumerable<EventRequest> events)
    {
        if (events == null) throw new ArgumentNullException(nameof(events));
        
        var response = await TrackEventsAsync<EventResponse>(events);
        return response.Data ?? throw new BentoException("Response data is null");
    }
}