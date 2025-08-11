using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

public interface IBentoValidationService
{
    // Email validation methods only
    Task<BentoResponse<T>> ValidateEmailAsync<T>(EmailValidationRequest request);
    Task<ValidateEmailResponse> ValidateEmailAsync(EmailValidationRequest request);
    Task<JesseRulesetResponse> ValidateEmailWithJesseRulesetAsync(JesseRulesetRequest request);
}