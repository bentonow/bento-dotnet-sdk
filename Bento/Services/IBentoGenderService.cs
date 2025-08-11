using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for gender prediction via Bento API.
/// Uses experimental/gender endpoint (<see href="https://docs.bentonow.com/utility" />).
/// Guesses gender based on first/last name using US Census Data.
/// Works best with US users as it uses US Census Data to make predictions.
/// Always returns a confidence level with the prediction.
/// </summary>
public interface IBentoGenderService
{
    /// <summary>
    /// Predict gender based on name (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Gender prediction request with full name or last name</param>
    /// <returns>Generic gender prediction response</returns>
    Task<BentoResponse<T>> PredictGenderAsync<T>(GenderRequest request);
    
    /// <summary>
    /// Predict gender based on name
    /// Uses US Census Data - works best with US users
    /// </summary>
    /// <param name="request">Gender prediction request with full name or last name</param>
    /// <returns>Gender prediction with confidence level</returns>
    Task<GenderResponse> PredictGenderAsync(GenderRequest request);
}