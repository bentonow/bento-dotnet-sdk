using System.Collections.Generic;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for managing events via Bento API.
/// Uses events endpoints (<see href="https://docs.bentonow.com/events_api" />).
/// Events are the connecting superpower of Bento that store micro-data and trigger automations.
/// Can create events for new or existing users, with detailed fields and nested details.
/// </summary>
public interface IBentoEventService
{
    /// <summary>
    /// Tracks a single event for a user.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="eventData">The event data to track</param>
    /// <returns>Generic response containing the event tracking result</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when eventData is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<BentoResponse<T>> TrackEventAsync<T>(EventRequest eventData);

    /// <summary>
    /// Tracks a single event for a user.
    /// </summary>
    /// <param name="eventData">The event data to track</param>
    /// <returns>Typed response containing the event tracking result</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when eventData is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<EventResponse> TrackEventAsync(EventRequest eventData);

    /// <summary>
    /// Tracks multiple events in a batch (up to 1000 events).
    /// This is the most efficient way to send multiple events.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to</typeparam>
    /// <param name="events">Collection of events to track (max 1000)</param>
    /// <returns>Generic response containing the batch event tracking result</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when events is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<BentoResponse<T>> TrackEventsAsync<T>(IEnumerable<EventRequest> events);

    /// <summary>
    /// Tracks multiple events in a batch (up to 1000 events).
    /// This is the most efficient way to send multiple events.
    /// </summary>
    /// <param name="events">Collection of events to track (max 1000)</param>
    /// <returns>Typed response containing the batch event tracking result</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when events is null</exception>
    /// <exception cref="BentoException">Thrown when the API request fails</exception>
    Task<EventResponse> TrackEventsAsync(IEnumerable<EventRequest> events);
}