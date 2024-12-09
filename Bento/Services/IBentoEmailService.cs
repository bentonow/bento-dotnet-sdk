using Bento.Models;

namespace Bento.Services;

public interface IBentoEmailService
{
    Task<BentoResponse<T>> SendEmailAsync<T>(EmailRequest email);
    Task<BentoResponse<T>> SendBatchEmailsAsync<T>(IEnumerable<EmailRequest> emails);
}