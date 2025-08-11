namespace Bento.Models;

/// <summary>
/// Request model for Jesse's ruleset email validation using experimental/jesses_ruleset endpoint.
/// This is a custom ruleset created by Jesse (Founder of Bento) used in their own application.
/// It is extremely strict - use with caution and monitor failures to avoid false positives.
/// </summary>
public class JesseRulesetRequest
{
    /// <summary>
    /// Email address to validate (required)
    /// Validates email's domain using MX records
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether to block free email providers (optional)
    /// Set to true to reject emails from free providers like Gmail, Yahoo, etc.
    /// </summary>
    public bool BlockFreeProviders { get; set; }
    
    /// <summary>
    /// Set to true to reduce extremely opinionated checks (optional)
    /// Enables wiggleroom mode to make validation less strict
    /// </summary>
    public bool Wiggleroom { get; set; }
}
