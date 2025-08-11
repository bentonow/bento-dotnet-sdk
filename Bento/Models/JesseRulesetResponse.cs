namespace Bento.Models;

/// <summary>
/// Response model for Jesse's ruleset email validation using experimental/jesses_ruleset endpoint.
/// This is a custom ruleset created by Jesse (Founder of Bento) used in their own application.
/// It is extremely strict - use with caution and monitor false positives.
/// </summary>
public class JesseRulesetResponse
{
    /// <summary>
    /// True/false flag if the email passes Jesse's strict validation ruleset
    /// False indicates the email failed one or more strict validation rules
    /// </summary>
    public bool Valid { get; set; }
    
    /// <summary>
    /// Array of string values describing the issues found with the email
    /// Contains specific reasons for validation failure (can be empty array if valid)
    /// </summary>
    public string[]? Reasons { get; set; }
}
