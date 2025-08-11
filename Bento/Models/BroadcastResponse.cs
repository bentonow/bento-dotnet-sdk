using System;

namespace Bento.Models;

/// <summary>
/// Response model for broadcast operations.
/// Contains broadcast details with stats and template information.
/// </summary>
public record BroadcastResponse
{
    /// <summary>
    /// Unique identifier of the broadcast.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Type of the broadcast.
    /// </summary>
    public string Type { get; init; } = string.Empty;

    /// <summary>
    /// Broadcast attributes including name, template, and statistics.
    /// </summary>
    public BroadcastAttributes Attributes { get; init; } = new();
}

/// <summary>
/// Broadcast attributes containing detailed information about the broadcast.
/// </summary>
public record BroadcastAttributes
{
    /// <summary>
    /// Name of the broadcast campaign.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Shareable URL for the broadcast.
    /// </summary>
    public string ShareUrl { get; init; } = string.Empty;

    /// <summary>
    /// Template information for the broadcast email.
    /// </summary>
    public BroadcastTemplate Template { get; init; } = new();

    /// <summary>
    /// Date and time when the broadcast was created.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Date and time when the final batch was sent. Can be null if not yet sent.
    /// </summary>
    public DateTime? SentFinalBatchAt { get; init; }

    /// <summary>
    /// Scheduled date and time for sending the broadcast. Can be null for immediate sending.
    /// </summary>
    public DateTime? SendAt { get; init; }

    /// <summary>
    /// Statistics for the broadcast including open rates.
    /// </summary>
    public BroadcastStats Stats { get; init; } = new();
}

/// <summary>
/// Template information for broadcast email.
/// </summary>
public record BroadcastTemplate
{
    /// <summary>
    /// Subject line of the email.
    /// </summary>
    public string Subject { get; init; } = string.Empty;

    /// <summary>
    /// Recipient information.
    /// </summary>
    public string To { get; init; } = string.Empty;

    /// <summary>
    /// HTML content of the email.
    /// </summary>
    public string Html { get; init; } = string.Empty;
}

/// <summary>
/// Statistics for broadcast performance.
/// </summary>
public record BroadcastStats
{
    /// <summary>
    /// Open rate percentage for the broadcast.
    /// </summary>
    public decimal OpenRate { get; init; }
}

/// <summary>
/// Response model for batch broadcast creation.
/// Contains the count of broadcasts queued for processing.
/// </summary>
public record BatchBroadcastResponse
{
    /// <summary>
    /// Number of broadcasts that were queued for processing.
    /// </summary>
    public int Results { get; init; }
}

/// <summary>
/// Container for broadcast list responses.
/// </summary>
public record BroadcastListResponse
{
    /// <summary>
    /// List of broadcasts returned by the API.
    /// </summary>
    public BroadcastResponse[] Data { get; init; } = Array.Empty<BroadcastResponse>();
}
