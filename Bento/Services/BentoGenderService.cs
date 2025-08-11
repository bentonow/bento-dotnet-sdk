using System;
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
public class BentoGenderService : IBentoGenderService
{
    private readonly IBentoClient _client;

    public BentoGenderService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Predict gender based on name (generic response)
    /// </summary>
    /// <typeparam name="T">Response type</typeparam>
    /// <param name="request">Gender prediction request with full name or last name</param>
    /// <returns>Generic gender prediction response</returns>
    public Task<BentoResponse<T>> PredictGenderAsync<T>(GenderRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        return _client.PostAsync<T>("experimental/gender", new { name = request.FullName });
    }

    /// <summary>
    /// Predict gender based on name
    /// Uses US Census Data - works best with US users
    /// </summary>
    /// <param name="request">Gender prediction request with full name or last name</param>
    /// <returns>Gender prediction with confidence level</returns>
    /// <exception cref="BentoException">Thrown when gender prediction fails</exception>
    public async Task<GenderResponse> PredictGenderAsync(GenderRequest request)
    {
        var response = await PredictGenderAsync<GenderResponse>(request);
        if (response.Success && response.Data != null)
        {
            return response.Data;
        }
        throw new BentoException(response.Error ?? "Gender prediction failed", response.StatusCode);
    }
}