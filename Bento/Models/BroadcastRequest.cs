using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Request model for creating broadcast campaigns.
/// Used with batch/broadcasts endpoint (<see href="https://docs.bentonow.com/broadcasts" />).
/// </summary>
/// <param name="Name">Name of the broadcast such as a campaign name.</param>
/// <param name="Subject">Subject line of the broadcast email.</param>
/// <param name="Content">Broadcast body content, supports liquid tags.</param>
/// <param name="Type">The type of email. 'plain' for Bento's css or 'raw' to use your own.</param>
/// <param name="From">Sender information, must be an author in your account.</param>
/// <param name="InclusiveTags">A comma separated list of tags to send to.</param>
/// <param name="ExclusiveTags">A comma separated list of tags you do not want the email to go to.</param>
/// <param name="SegmentId">The segment ID for the campaign.</param>
/// <param name="BatchSizePerHour">The amount of emails to send per hour to ensure the highest delivery.</param>
/// <param name="SendAt">The date and time to send the broadcast at. Required when Approved is true.</param>
/// <param name="Approved">Whether the broadcast has been approved by Bento and will be sent at the scheduled time.</param>
public record BroadcastRequest(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("subject")] string Subject,
    [property: JsonPropertyName("content")] string Content,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("from")] ContactInfo From,
    [property: JsonPropertyName("inclusive_tags")] string? InclusiveTags = null,
    [property: JsonPropertyName("exclusive_tags")] string? ExclusiveTags = null,
    [property: JsonPropertyName("segment_id")] string? SegmentId = null,
    [property: JsonPropertyName("batch_size_per_hour")] int? BatchSizePerHour = null,
    [property: JsonPropertyName("send_at")] DateTime? SendAt = null,
    [property: JsonPropertyName("approved")] bool? Approved = null
);

/// <summary>
/// Wrapper for Create Broadcasts API request.
/// According to Bento API documentation, broadcasts creation requires "broadcasts" array wrapper.
/// </summary>
public record CreateBroadcastsRequest(
    [property: JsonPropertyName("broadcasts")] IEnumerable<BroadcastRequest> Broadcasts
);

/// <summary>
/// Contact information for the sender of the broadcast.
/// </summary>
/// <param name="EmailAddress">Email address of the sender.</param>
/// <param name="Name">Display name of the sender.</param>
public record ContactInfo(
    [property: JsonPropertyName("email")] string EmailAddress,
    [property: JsonPropertyName("name")] string? Name = null
);