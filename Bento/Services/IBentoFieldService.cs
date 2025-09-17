using System.Threading.Tasks;
using Bento.Models;

namespace Bento.Services;

/// <summary>
/// Service for managing custom fields via Bento API.
/// Uses fields endpoints (<see href="https://docs.bentonow.com/fields" />).
/// Fields are unique data points you collect about subscribers, forming the
/// backbone of personalized data in Bento.
/// </summary>
public interface IBentoFieldService
{
    /// <summary>
    /// Gets all fields in your account.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to.</typeparam>
    /// <returns>A task containing the response with fields data.</returns>
    Task<BentoResponse<T>> GetFieldsAsync<T>();
    
    /// <summary>
    /// Gets all fields in your account with strongly typed response.
    /// </summary>
    /// <returns>A task containing the fields response.</returns>
    Task<FieldsResponse> GetFieldsAsync();

    /// <summary>
    /// Creates a new custom field in your account.
    /// Only a single field can be created at a time.
    /// </summary>
    /// <typeparam name="T">The type to deserialize the response to.</typeparam>
    /// <param name="field">The field creation request containing the field key.</param>
    /// <returns>A task containing the response with created field data.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when field is null.</exception>
    Task<BentoResponse<T>> CreateFieldAsync<T>(FieldRequest field);
    
    /// <summary>
    /// Creates a new custom field in your account with strongly typed response.
    /// Only a single field can be created at a time.
    /// </summary>
    /// <param name="field">The field creation request containing the field key.</param>
    /// <returns>A task containing the field response.</returns>
    /// <exception cref="System.ArgumentNullException">Thrown when field is null.</exception>
    Task<FieldResponse> CreateFieldAsync(FieldRequest field);
}