using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Implementation of email service for sending transactional and promotional emails via Bento API.
/// Uses the Emails API endpoint (<see href="https://docs.bentonow.com/emails_api" />).
/// </summary>
public class BentoEmailService : IBentoEmailService
{
    private readonly IBentoClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="BentoEmailService"/> class.
    /// </summary>
    /// <param name="client">The Bento client for making API requests</param>
    /// <exception cref="ArgumentNullException">Thrown when client is null</exception>
    public BentoEmailService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> SendEmailAsync<T>(EmailRequest email)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        
        return SendBatchEmailsAsync<T>(new[] { email });
    }

    /// <inheritdoc />
    public async Task<EmailResponse> SendEmailAsync(EmailRequest email)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));
        
        var response = await SendBatchEmailsAsync<EmailResponse>(new[] { email });
        return response.Data ?? new EmailResponse(0);
    }

    /// <inheritdoc />
    public Task<BentoResponse<T>> SendBatchEmailsAsync<T>(IEnumerable<EmailRequest> emails)
    {
        if (emails == null) throw new ArgumentNullException(nameof(emails));

        var request = new CreateEmailsRequest(emails);
        return _client.PostAsync<T>("batch/emails", request);
    }

    /// <inheritdoc />
    public async Task<EmailResponse> SendBatchEmailsAsync(IEnumerable<EmailRequest> emails)
    {
        if (emails == null) throw new ArgumentNullException(nameof(emails));

        var response = await SendBatchEmailsAsync<EmailResponse>(emails);
        return response.Data ?? new EmailResponse(0);
    }
}