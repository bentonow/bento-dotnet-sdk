using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Response model for blacklist checking using experimental/blacklist.json endpoint.
/// Validates IP or domain name with industry email reputation services to check for delivery issues.
/// A true value for any of the providers indicates a problem with the domain/IP.
/// </summary>
public class BlacklistResponse
{
    /// <summary>
    /// The query that was checked (domain or IP address)
    /// </summary>
    [JsonPropertyName("query")]
    public string? Query { get; set; }

    /// <summary>
    /// Description of the blacklist check performed
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Blacklist check results from multiple providers
    /// Can be null if all checks return false (no issues found)
    /// </summary>
    [JsonPropertyName("results")]
    public BlacklistResults? Results { get; set; }
}

/// <summary>
/// Blacklist check results from multiple email reputation services.
/// A true value for any provider indicates the domain/IP is flagged by that service.
/// </summary>
public class BlacklistResults
{
    /// <summary>
    /// True/false flag if the domain age is flagged
    /// Indicates if the domain was recently registered
    /// </summary>
    [JsonPropertyName("just_registered")]
    public bool JustRegistered { get; set; }

    /// <summary>
    /// True if Spamhaus considers mail from this domain or IP to contain likely spam
    /// Spamhaus is a major anti-spam organization
    /// </summary>
    [JsonPropertyName("spamhaus")]
    public bool Spamhaus { get; set; }

    /// <summary>
    /// True if Nordspam considers mail from this domain or IP to likely contain spam
    /// Nordspam is an email reputation service
    /// </summary>
    [JsonPropertyName("nordspam")]
    public bool Nordspam { get; set; }

    /// <summary>
    /// True if Spfbl considers mail from this domain or IP to likely contain spam
    /// Spfbl is a spam filtering service
    /// </summary>
    [JsonPropertyName("spfbl")]
    public bool Spfbl { get; set; }

    /// <summary>
    /// True if Sorbs considers mail from this domain or IP to likely contain spam
    /// Sorbs is a spam and open relay blocking service
    /// </summary>
    [JsonPropertyName("sorbs")]
    public bool Sorbs { get; set; }

    /// <summary>
    /// True if Abusix considers mail from this domain or IP to likely contain spam
    /// Abusix is a network abuse intelligence provider
    /// </summary>
    [JsonPropertyName("abusix")]
    public bool Abusix { get; set; }
}
