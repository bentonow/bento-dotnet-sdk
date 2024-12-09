namespace Bento.Models;

public record BroadcastRequest(
    string Name,
    string Subject,
    string Content,
    string Type,
    ContactInfo From,
    string? InclusiveTags = null,
    string? ExclusiveTags = null,
    string? SegmentId = null,
    int? BatchSizePerHour = null
);

public record ContactInfo(
    string EmailAddress,
    string? Name = null
);