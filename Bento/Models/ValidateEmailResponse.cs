namespace Bento.Models;

/// <summary>
/// Response model for email validation.
/// Basic validation using experimental/validation endpoint.
/// </summary>
public class ValidateEmailResponse
{
    /// <summary>
    /// Whether the email address is considered valid
    /// </summary>
    public bool Valid { get; set; }
}