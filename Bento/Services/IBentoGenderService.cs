using Bento.Models;

namespace Bento.Services;

public interface IBentoGenderService
{
    Task<BentoResponse<T>> PredictGenderAsync<T>(GenderRequest request);
}