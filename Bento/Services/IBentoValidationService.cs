using Bento.Models;

namespace Bento.Services;

public interface IBentoValidationService
{
    Task<BentoResponse<T>> ValidateEmailAsync<T>(EmailValidationRequest request);
}