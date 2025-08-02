using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public class BentoEmailService : IBentoEmailService
{
    private readonly IBentoClient _client;

    public BentoEmailService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> SendEmailAsync<T>(EmailRequest email)
    {
        return SendBatchEmailsAsync<T>(new[] { email });
    }

    public Task<BentoResponse<T>> SendBatchEmailsAsync<T>(IEnumerable<EmailRequest> emails)
    {
        var request = new
        {
            emails = emails.Select(e => new
            {
                from = e.From,
                subject = e.Subject,
                html_body = e.HtmlBody,
                to = e.To,
                cc = e.Cc,
                bcc = e.Bcc,
                transactional = e.Transactional
            })
        };

        return _client.PostAsync<T>("batch/emails", request);
    }
}