namespace Bento.Models;

public record EventRequest(
    string Type,
    string Email,
    Dictionary<string, object>? Fields = null,
    Dictionary<string, object>? Details = null
);