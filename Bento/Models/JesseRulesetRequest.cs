namespace Bento.Models;

/// <summary>
/// Request model for Jesse's ruleset email validation
/// </summary>
public class JesseRulesetRequest
{
    /// <summary>
    /// Email address to validate
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether to block free email providers
    /// </summary>
    public bool BlockFreeProviders { get; set; }
    
    /// <summary>
    /// Set to true to reduce extremely opinionated checks
    /// </summary>
    public bool Wiggleroom { get; set; }
}
