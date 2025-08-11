using System;
using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Implementation of field management service for Bento API.
/// Provides methods to retrieve and create custom fields.
/// </summary>
public class BentoFieldService : IBentoFieldService
{
    private readonly IBentoClient _client;

    /// <summary>
    /// Initializes a new instance of the BentoFieldService.
    /// </summary>
    /// <param name="client">The Bento client for API communication.</param>
    /// <exception cref="ArgumentNullException">Thrown when client is null.</exception>
    public BentoFieldService(IBentoClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    /// <summary>
    /// Gets all fields in your account.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to.</typeparam>
    /// <returns>A task containing the response with fields data.</returns>
    public Task<BentoResponse<T>> GetFieldsAsync<T>()
    {
        return _client.GetAsync<T>("fetch/fields");
    }

    /// <summary>
    /// Gets all fields in your account with strongly typed response.
    /// </summary>
    /// <returns>A task containing the fields response.</returns>
    public async Task<FieldsResponse> GetFieldsAsync()
    {
        var response = await _client.GetAsync<FieldsResponse>("fetch/fields");
        
        if (!response.Success || response.Data == null)
        {
            var errorMessage = string.IsNullOrEmpty(response.Error) ? "Failed to retrieve fields" : response.Error;
            throw new BentoException(errorMessage, response.StatusCode);
        }
        
        return response.Data;
    }

    /// <summary>
    /// Creates a new custom field in your account.
    /// Only a single field can be created at a time.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to.</typeparam>
    /// <param name="field">The field creation request containing the field key.</param>
    /// <returns>A task containing the response with created field data.</returns>
    /// <exception cref="ArgumentNullException">Thrown when field is null.</exception>
    public Task<BentoResponse<T>> CreateFieldAsync<T>(FieldRequest field)
    {
        if (field == null) throw new ArgumentNullException(nameof(field));
        
        field.Validate();
        
        var request = new { field = new { key = field.Key } };
        return _client.PostAsync<T>("fetch/fields", request);
    }

    /// <summary>
    /// Creates a new custom field in your account with strongly typed response.
    /// Only a single field can be created at a time.
    /// </summary>
    /// <param name="field">The field creation request containing the field key.</param>
    /// <returns>A task containing the field response.</returns>
    /// <exception cref="ArgumentNullException">Thrown when field is null.</exception>
    public async Task<FieldResponse> CreateFieldAsync(FieldRequest field)
    {
        if (field == null) throw new ArgumentNullException(nameof(field));
        
        field.Validate();
        
        var request = new { field = new { key = field.Key } };
        var response = await _client.PostAsync<FieldResponse>("fetch/fields", request);
        
        if (!response.Success || response.Data == null)
        {
            var errorMessage = string.IsNullOrEmpty(response.Error) ? "Failed to create field" : response.Error;
            throw new BentoException(errorMessage, response.StatusCode);
        }
        
        return response.Data;
    }
}