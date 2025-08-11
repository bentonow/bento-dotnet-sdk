using System.Collections.Generic;

namespace Bento.Models;

/// <summary>
/// Request model for creating or importing subscribers via Bento API.
/// Used with Create Subscriber and Import Subscribers endpoints.
/// See <see href="https://docs.bentonow.com/subscribers" /> for details.
/// </summary>
/// <param name="Email">Email address for the subscriber (required)</param>
/// <param name="FirstName">First name for the subscriber (optional)</param>
/// <param name="LastName">Last name for the subscriber (optional)</param>
/// <param name="Tags">Collection of tags to add - if a tag doesn't exist it is created (optional)</param>
/// <param name="RemoveTags">Collection of tags to remove from the subscriber (optional)</param>
/// <param name="Fields">Key/value pairs for storing custom fields for the subscriber (optional)</param>
public record SubscriberRequest(
    string Email,
    string? FirstName = null,
    string? LastName = null,
    IEnumerable<string>? Tags = null,
    IEnumerable<string>? RemoveTags = null,
    Dictionary<string, object>? Fields = null
);