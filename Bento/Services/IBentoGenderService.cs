using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for gender prediction via Bento API.
/// Uses experimental/gender endpoint (<see href="https://docs.bentonow.com/utility#the-gender-guess-model" />).
/// Guesses gender based on first/last name using US Census Data. Works best with US users.
/// </summary>
public interface IBentoGenderService
{
    /// <summary>
    /// Predict gender based on name (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Gender prediction request</param>
    /// <returns>Gender prediction response</returns>
    Task<BentoResponse<T>> PredictGenderAsync<T>(GenderRequest request);
    
    /// <summary>
    /// Predict gender based on name
    /// </summary>
    /// <param name="request">Gender prediction request</param>
    /// <returns>Gender prediction with confidence</returns>
    Task<GenderResponse> PredictGenderAsync(GenderRequest request);
}