using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Response model for email validation using experimental/validation endpoint.
/// Validates email address using provided information to infer its validity.
/// Validates against spam patterns and uses MX record validation.
/// </summary>
public class ValidateEmailResponse
{
    /// <summary>
    /// True/false flag if the email address is considered valid
    /// Indicates whether the email passed all validation checks
    /// </summary>
    [JsonPropertyName("valid")]
    public bool Valid { get; set; }
}