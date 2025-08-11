namespace Bento.Models;

/// <summary>
/// Request model for gender prediction.
/// </summary>
/// <param name="FullName">Full name or first name for gender prediction (works best with US names based on US Census Data)</param>
public record GenderRequest(string FullName);