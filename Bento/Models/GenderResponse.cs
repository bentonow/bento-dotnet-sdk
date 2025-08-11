namespace Bento.Models;

/// <summary>
/// Response model for gender prediction.
/// Based on US Census Data, works best with US users.
/// </summary>
public class GenderResponse
{
    /// <summary>
    /// Predicted gender based on census data
    /// </summary>
    public string? Gender { get; set; }
    
    /// <summary>
    /// Confidence level of the prediction (always returned)
    /// </summary>
    public float Confidence { get; set; }
}
