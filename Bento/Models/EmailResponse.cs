using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Represents the response from the Bento Emails API when sending emails.
/// Based on the Emails API documentation (<see href="https://docs.bentonow.com/emails_api" />).
/// </summary>
public record EmailResponse(
    /// <summary>
    /// The count of emails queued for delivery.
    /// Indicates how many emails were successfully accepted for processing.
    /// </summary>
    [property: JsonPropertyName("results")] int Results
);
