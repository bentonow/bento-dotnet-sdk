namespace Bento.Models;

/// <summary>
/// Request model for email validation using experimental/validation endpoint.
/// Validates an email's domain using MX records with additional context for better accuracy.
/// </summary>
/// <param name="EmailAddress">Email address to validate (required)</param>
/// <param name="FullName">Contact's full name (optional, heavy bias towards US Census Data)</param>
/// <param name="UserAgent">User agent string (optional)</param>
/// <param name="IpAddress">User's IP address (optional, returns false for all countries outside of Tier 1)</param>
public record EmailValidationRequest(
    string EmailAddress,
    string? FullName = null,
    string? UserAgent = null,
    string? IpAddress = null
);