namespace Bento.Models;

/// <summary>
/// Response model for Jesse's ruleset email validation.
/// Extremely strict validation - use with caution and monitor false positives.
/// </summary>
public class JesseRulesetResponse
{
    /// <summary>
    /// Whether the email passes Jesse's strict validation
    /// </summary>
    public bool Valid { get; set; }
    
    /// <summary>
    /// Reasons for validation failure (can be empty array)
    /// </summary>
    public string[]? Reasons { get; set; }
}
