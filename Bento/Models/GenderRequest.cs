using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Request model for gender prediction using experimental/gender endpoint.
/// Guess a subscriber's gender using their first and last name.
/// Best for US users as it's based on US Census Data.
/// </summary>
/// <param name="FullName">Full name of a subscriber or just the first name (works best with US names based on US Census Data)</param>
public record GenderRequest(
    [property: JsonPropertyName("name")] string FullName
);