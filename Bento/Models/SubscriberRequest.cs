namespace Bento.Models;

public record SubscriberRequest(
    string Email,
    string? FirstName = null,
    string? LastName = null,
    IEnumerable<string>? Tags = null,
    IEnumerable<string>? RemoveTags = null,
    Dictionary<string, object>? Fields = null
);