using System;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for gender prediction via Bento API.
/// Uses experimental/gender endpoint (<see href="https://docs.bentonow.com/utility#the-gender-guess-model" />).
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
    /// <param name="request">Gender prediction request</param>
    /// <returns>Gender prediction response</returns>
    public Task<BentoResponse<T>> PredictGenderAsync<T>(GenderRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        
        return _client.PostAsync<T>("experimental/gender", new { name = request.FullName });
    }

    /// <summary>
    /// Predict gender based on name
    /// </summary>
    /// <param name="request">Gender prediction request</param>
    /// <returns>Gender prediction with confidence</returns>
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