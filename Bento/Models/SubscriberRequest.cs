using System.Collections.Generic;
using System.Text.Json.Serialization;
using Bento.Extensions;

namespace Bento.Models;

/// <summary>
/// Request model for creating or importing subscribers via Bento API.
/// Used with Create Subscriber and Import Subscribers endpoints.
/// See <see href="https://docs.bentonow.com/subscribers" /> for details.
/// </summary>
/// <param name="Email">Email address for the subscriber (required)</param>
/// <param name="FirstName">First name for the subscriber (optional)</param>
/// <param name="LastName">Last name for the subscriber (optional)</param>
/// <param name="Tags">Collection of tags to add - if a tag doesn't exist it is created (optional). Will be serialized as comma-separated string</param>
/// <param name="RemoveTags">Collection of tags to remove from the subscriber (optional). Will be serialized as comma-separated string</param>
/// <param name="Fields">Key/value pairs for storing custom fields for the subscriber (optional)</param>
public record SubscriberRequest(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("first_name")] string? FirstName = null,
    [property: JsonPropertyName("last_name")] string? LastName = null,
    [property: JsonPropertyName("tags"), JsonConverter(typeof(StringArrayToCommaSeparatedConverter))] IEnumerable<string>? Tags = null,
    [property: JsonPropertyName("remove_tags"), JsonConverter(typeof(StringArrayToCommaSeparatedConverter))] IEnumerable<string>? RemoveTags = null,
    [property: JsonPropertyName("fields")] Dictionary<string, object>? Fields = null
);