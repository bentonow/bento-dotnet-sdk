using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Request model for blacklist checking using experimental/blacklist.json endpoint.
/// Validates IP or domain name with industry email reputation services to check for delivery issues.
/// You can provide either a domain or an IP to be looked up (or both).
/// Only IPv4 addresses are currently supported.
/// </summary>
/// <param name="Domain">Domain to check without protocol (e.g., "google.com" not "https://google.com")</param>
/// <param name="IpAddress">IPv4 address to check (e.g., "1.1.1.1")</param>
public record BlacklistStatusRequest(
    [property: JsonPropertyName("domain")] string? Domain = null,
    [property: JsonPropertyName("ip_address")] string? IpAddress = null
);