namespace Bento.Models;

public record BlacklistStatusRequest(
    string? Domain = null,
    string? IpAddress = null
);