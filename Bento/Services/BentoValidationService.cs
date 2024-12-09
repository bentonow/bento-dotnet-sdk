using Bento.Models;

namespace Bento.Services;

public class BentoValidationService : IBentoValidationService
{
    private readonly IBentoClient _client;

    public BentoValidationService(IBentoClient client)
    {
        _client = client;
    }

    public Task<BentoResponse<T>> ValidateEmailAsync<T>(EmailValidationRequest request)
    {
        return _client.PostAsync<T>("experimental/validation", new
        {
            email = request.EmailAddress,
            name = request.FullName,
            user_agent = request.UserAgent,
            ip = request.IpAddress
        });
    }
}