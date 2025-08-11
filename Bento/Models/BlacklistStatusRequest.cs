namespace Bento.Models;

/// <summary>
/// Request model for blacklist checking.
/// Provide either domain or IP address (or both) to check against blacklist providers.
/// </summary>
/// <param name="Domain">Domain to check (e.g., "google.com") without protocol</param>
/// <param name="IpAddress">IPv4 address to check (e.g., "1.1.1.1")</param>
public record BlacklistStatusRequest(
    string? Domain = null,
    string? IpAddress = null
);