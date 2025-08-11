using System.Collections.Generic;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for sending transactional and promotional emails via Bento API.
/// Uses the Emails API endpoint (<see href="https://docs.bentonow.com/emails_api" />).
/// 
/// This endpoint is throttled to send at a rate of 60 emails per minute.
/// Most requests will be processed within 1-5 seconds.
/// 
/// Note: Bento does not support file attachments in emails. Use links to attachments instead.
/// Avoid using emojis or URL shorteners in transactional emails as they may be detected as spam.
/// </summary>
public interface IBentoEmailService
{
    /// <summary>
    /// Sends a single email via Bento API.
    /// Returns a generic response wrapper.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response into</typeparam>
    /// <param name="email">The email request containing recipient, sender, subject, and content</param>
    /// <returns>A task containing the API response</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when email is null</exception>
    /// <exception cref="BentoException">Thrown when API request fails</exception>
    Task<BentoResponse<T>> SendEmailAsync<T>(EmailRequest email);
    
    /// <summary>
    /// Sends a single email via Bento API.
    /// Returns a typed EmailResponse.
    /// </summary>
    /// <param name="email">The email request containing recipient, sender, subject, and content</param>
    /// <returns>A task containing the email response with the count of queued emails</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when email is null</exception>
    /// <exception cref="BentoException">Thrown when API request fails</exception>
    Task<EmailResponse> SendEmailAsync(EmailRequest email);

    /// <summary>
    /// Sends multiple emails in a batch via Bento API.
    /// Can send 1 to 60 emails per request.
    /// Returns a generic response wrapper.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response into</typeparam>
    /// <param name="emails">Collection of email requests (max 60 emails per batch)</param>
    /// <returns>A task containing the API response</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when emails collection is null</exception>
    /// <exception cref="BentoException">Thrown when API request fails</exception>
    Task<BentoResponse<T>> SendBatchEmailsAsync<T>(IEnumerable<EmailRequest> emails);
    
    /// <summary>
    /// Sends multiple emails in a batch via Bento API.
    /// Can send 1 to 60 emails per request.
    /// Returns a typed EmailResponse.
    /// </summary>
    /// <param name="emails">Collection of email requests (max 60 emails per batch)</param>
    /// <returns>A task containing the email response with the count of queued emails</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when emails collection is null</exception>
    /// <exception cref="BentoException">Thrown when API request fails</exception>
    Task<EmailResponse> SendBatchEmailsAsync(IEnumerable<EmailRequest> emails);
}