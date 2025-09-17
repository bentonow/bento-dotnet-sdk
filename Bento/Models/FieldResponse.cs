using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Represents a field response from the Bento Fields API.
/// Contains information about a custom field that can be used to store subscriber data.
/// </summary>
public class FieldResponse
{
    /// <summary>
    /// The unique identifier of the field.
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// The type of the resource (typically "visitors-fields").
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// The attributes containing the field data.
    /// </summary>
    [JsonPropertyName("attributes")]
    public FieldAttributes? Attributes { get; set; }
}

/// <summary>
/// Contains the field attributes returned by the Bento Fields API.
/// </summary>
public class FieldAttributes
{
    /// <summary>
    /// The display name of the field (e.g., "Last Name").
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The unique key identifier for the field (e.g., "last_name").
    /// This is used when storing field data for subscribers.
    /// </summary>
    [JsonPropertyName("key")]
    public string? Key { get; set; }

    /// <summary>
    /// Indicates if the field is whitelisted. Can be null if not specified.
    /// </summary>
    [JsonPropertyName("whitelisted")]
    public bool? Whitelisted { get; set; }
}

/// <summary>
/// Represents the response wrapper for fields API calls.
/// Contains an array of field data returned by the Bento API.
/// </summary>
public class FieldsResponse
{
    /// <summary>
    /// The array of fields returned by the API.
    /// </summary>
    [JsonPropertyName("data")]
    public FieldResponse[]? Data { get; set; }
}
