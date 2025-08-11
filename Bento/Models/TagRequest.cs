using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Represents a request to create a tag in Bento.
/// Contains the required name for the tag to be created.
/// </summary>
/// <param name="Name">The name of the tag to create. This field is required.</param>
public record TagRequest(
    [property: JsonPropertyName("name")] string Name
);

/// <summary>
/// Wrapper for Create Tag API request.
/// According to Bento API documentation, tag creation requires "tag" wrapper.
/// </summary>
public record CreateTagRequest(
    [property: JsonPropertyName("tag")] TagRequest Tag
);