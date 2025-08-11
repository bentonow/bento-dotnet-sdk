using System.Collections.Generic;

namespace Bento.Models;

/// <summary>
/// Represents an email request for sending transactional or promotional emails via Bento API.
/// Based on the Emails API documentation (<see href="https://docs.bentonow.com/emails_api" />).
/// </summary>
public record EmailRequest(
    /// <summary>
    /// The email address of the recipient. This field is required.
    /// </summary>
    string To,
    
    /// <summary>
    /// The email address of the sender. Must be an author in your account. This field is required.
    /// </summary>
    string From,
    
    /// <summary>
    /// The subject of the email. This field is required.
    /// </summary>
    string Subject,
    
    /// <summary>
    /// The HTML body of the email. This field is required.
    /// </summary>
    string HtmlBody,
    
    /// <summary>
    /// True or false flag if the email is transactional or not. Defaults to false.
    /// When marked as true, the email will be sent even if the user has unsubscribed. USE WITH CAUTION!
    /// </summary>
    bool Transactional = false,
    
    /// <summary>
    /// Key-value pair custom data to be injected into the email using merge tags.
    /// Optional field for personalizing email content.
    /// </summary>
    Dictionary<string, object>? Personalizations = null
);