using System;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;
/// <summary>
/// Service for email validation via Bento API.
/// Uses experimental validation endpoints (<see href="https://docs.bentonow.com/utility#the-validate-email-model" />).
/// Available methods: basic email validation, Jesse's custom ruleset validation.
/// </summary>
public class BentoValidationService : IBentoValidationService
{
    private readonly IBentoClient _client;

    public BentoValidationService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    
    /// <summary>
    /// Validates an email address using experimental/validation endpoint
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Email validation request</param>
    /// <returns>Validation response</returns>
    public Task<BentoResponse<T>> ValidateEmailAsync<T>(EmailValidationRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        return _client.PostAsync<T>("experimental/validation", new
        {
            email = request.EmailAddress,
            name = request.FullName,
            user_agent = request.UserAgent,
            ip = request.IpAddress
        });
    }

    /// <summary>
    /// Validates an email address using experimental/validation endpoint
    /// </summary>
    /// <param name="request">Email validation request</param>
    /// <returns>Validation response</returns>
    /// <exception cref="BentoException">Thrown when validation fails</exception>
    public async Task<ValidateEmailResponse> ValidateEmailAsync(EmailValidationRequest request)
    {
        var response = await ValidateEmailAsync<ValidateEmailResponse>(request);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Email validation failed", response.StatusCode);
    }

    /// <summary>
    /// Validates an email address using Jesse's custom ruleset (experimental/jesses_ruleset)
    /// This is extremely strict - use with caution and monitor false positives
    /// </summary>
    /// <param name="request">Jesse's ruleset validation request</param>
    /// <returns>Validation result with reasons</returns>
    /// <exception cref="BentoException">Thrown when validation fails</exception>
    public async Task<JesseRulesetResponse> ValidateEmailWithJesseRulesetAsync(JesseRulesetRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        var response = await _client.PostAsync<JesseRulesetResponse>("experimental/jesses_ruleset", new
        {
            email = request.EmailAddress,
            block_free_providers = request.BlockFreeProviders,
            wiggleroom = request.Wiggleroom
        });
        
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Jesse's ruleset validation failed", response.StatusCode);
    }
}
