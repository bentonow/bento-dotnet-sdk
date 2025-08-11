using System;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for email validation via Bento API.
/// Uses experimental validation endpoints (<see href="https://docs.bentonow.com/utility" />).
/// Provides basic email validation and Jesse's custom ruleset validation.
/// Jesse's ruleset is extremely strict - use with caution and monitor false positives.
/// </summary>
public class BentoValidationService : IBentoValidationService
{
    private readonly IBentoClient _client;

    public BentoValidationService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    
    /// <summary>
    /// Validates an email address using basic validation (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Email validation request with email, name, user_agent, and IP</param>
    /// <returns>Generic validation response</returns>
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
    /// Validates an email address using basic validation
    /// </summary>
    /// <param name="request">Email validation request with email, name, user_agent, and IP</param>
    /// <returns>Validation result with valid flag</returns>
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
    /// Validates an email address using Jesse's custom ruleset (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Jesse's ruleset validation request</param>
    /// <returns>Generic validation response</returns>
    public Task<BentoResponse<T>> ValidateEmailWithJesseRulesetAsync<T>(JesseRulesetRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        return _client.PostAsync<T>("experimental/jesses_ruleset", new
        {
            email = request.EmailAddress,
            block_free_providers = request.BlockFreeProviders,
            wiggleroom = request.Wiggleroom
        });
    }

    /// <summary>
    /// Validates an email address using Jesse's custom ruleset
    /// This is extremely strict - use with caution and monitor false positives
    /// </summary>
    /// <param name="request">Jesse's ruleset validation request</param>
    /// <returns>Validation result with reasons array</returns>
    /// <exception cref="BentoException">Thrown when validation fails</exception>
    public async Task<JesseRulesetResponse> ValidateEmailWithJesseRulesetAsync(JesseRulesetRequest request)
    {
        var response = await ValidateEmailWithJesseRulesetAsync<JesseRulesetResponse>(request);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Jesse's ruleset validation failed", response.StatusCode);
    }
}
