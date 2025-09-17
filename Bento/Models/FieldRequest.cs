using System;
using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Represents a request to create a field definition via the Bento Fields API.
/// Used with POST /v1/fetch/fields endpoint to create field templates.
/// </summary>
/// <param name="Key">The unique key identifier for the field to create (required).</param>
public record FieldRequest(
    [property: JsonPropertyName("key")] string Key
)
{
    /// <summary>
    /// Validates that the Key is not null or empty.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when Key is null, empty, or whitespace.</exception>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Key))
            throw new ArgumentException("Field key cannot be null, empty, or whitespace.", nameof(Key));
    }
}

/// <summary>
/// Wrapper for Create Field API request.
/// According to Bento API documentation, field creation requires "field" wrapper.
/// </summary>
public record CreateFieldRequest(
    [property: JsonPropertyName("field")] FieldRequest Field
);

/// <summary>
/// Represents a field data entry with both key and value.
/// Used when working with subscriber field data (e.g., in Commands API).
/// </summary>
/// <param name="Key">The field key identifier.</param>
/// <param name="Value">The field value.</param>
public record FieldData(
    [property: JsonPropertyName("key")] string Key, 
    [property: JsonPropertyName("value")] object Value
)
{
    /// <summary>
    /// Validates that the Key is not null or empty.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when Key is null, empty, or whitespace.</exception>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Key))
            throw new ArgumentException("Field key cannot be null, empty, or whitespace.", nameof(Key));
    }
}