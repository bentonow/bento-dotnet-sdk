using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Response model for event tracking operations.
/// Represents the result of creating events via the Bento Events API.
/// </summary>
public class EventResponse
{
    /// <summary>
    /// Number of events that were successfully recorded.
    /// </summary>
    [JsonPropertyName("results")]
    public int Results { get; set; }

    /// <summary>
    /// Number of events that failed to be recorded.
    /// </summary>
    [JsonPropertyName("failed")]
    public int Failed { get; set; }
}
