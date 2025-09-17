using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Request model for creating events via Bento Events API.
/// Events store micro-data that can trigger automations and workflows.
/// </summary>
/// <param name="Type">The name of the event (required). Examples: "$completed_onboarding", "$purchase"</param>
/// <param name="Email">Email of the event user (required). Will create new user if not exists</param>
/// <param name="Fields">Key-value array of fields for the event (optional). Should not be nested</param>
/// <param name="Details">Key-value array of detailed information (optional). Can be nested for complex data</param>
public record EventRequest(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("fields")] Dictionary<string, object>? Fields = null,
    [property: JsonPropertyName("details")] Dictionary<string, object>? Details = null
);

/// <summary>
/// Wrapper for Create Events API request.
/// According to Bento API documentation, events creation requires "events" array wrapper.
/// </summary>
public record CreateEventsRequest(
    [property: JsonPropertyName("events")] IEnumerable<EventRequest> Events
);