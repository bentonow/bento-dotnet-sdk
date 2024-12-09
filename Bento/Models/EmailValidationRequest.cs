namespace Bento.Models;

public record EmailValidationRequest(
    string EmailAddress,
    string? FullName = null,
    string? UserAgent = null,
    string? IpAddress = null
);