namespace Bento.Models;

/// <summary>
/// Represents a request to create a tag in Bento.
/// Contains the required name for the tag to be created.
/// </summary>
/// <param name="Name">The name of the tag to create. This field is required.</param>
public record TagRequest(string Name);