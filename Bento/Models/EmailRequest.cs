namespace Bento.Models;

public record EmailRequest(
    string From,
    string Subject,
    string HtmlBody,
    string To,
    string? Cc = null,
    string? Bcc = null,
    bool Transactional = true
);