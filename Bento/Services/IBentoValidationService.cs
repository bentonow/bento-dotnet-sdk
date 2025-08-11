using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for email validation via Bento API.
/// Uses experimental validation endpoints (<see href="https://docs.bentonow.com/utility" />).
/// Provides basic email validation and Jesse's custom ruleset validation.
/// Jesse's ruleset is extremely strict - use with caution and monitor false positives.
/// </summary>
public interface IBentoValidationService
{
    /// <summary>
    /// Validates an email address using basic validation (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Email validation request with email, name, user_agent, and IP</param>
    /// <returns>Generic validation response</returns>
    Task<BentoResponse<T>> ValidateEmailAsync<T>(EmailValidationRequest request);
    
    /// <summary>
    /// Validates an email address using basic validation
    /// </summary>
    /// <param name="request">Email validation request with email, name, user_agent, and IP</param>
    /// <returns>Validation result with valid flag</returns>
    Task<ValidateEmailResponse> ValidateEmailAsync(EmailValidationRequest request);
    
    /// <summary>
    /// Validates an email address using Jesse's custom ruleset (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Jesse's ruleset validation request</param>
    /// <returns>Generic validation response</returns>
    Task<BentoResponse<T>> ValidateEmailWithJesseRulesetAsync<T>(JesseRulesetRequest request);
    
    /// <summary>
    /// Validates an email address using Jesse's custom ruleset
    /// This is extremely strict - use with caution and monitor false positives
    /// </summary>
    /// <param name="request">Jesse's ruleset validation request</param>
    /// <returns>Validation result with reasons array</returns>
    Task<JesseRulesetResponse> ValidateEmailWithJesseRulesetAsync(JesseRulesetRequest request);
}