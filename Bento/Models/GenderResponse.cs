namespace Bento.Models;

/// <summary>
/// Response model for gender prediction using experimental/gender endpoint.
/// Guesses gender based on first and last name using US Census Data.
/// Works best for US users as it's based on US Census Data.
/// The response will always return a confidence level.
/// </summary>
public class GenderResponse
{
    /// <summary>
    /// The guessed gender based on US Census Data
    /// Possible values include "male", "female", or other gender classifications
    /// </summary>
    public string? Gender { get; set; }
    
    /// <summary>
    /// Confidence level of the guess (float value between 0.0 and 1.0)
    /// Always returned - higher values indicate more confident predictions
    /// </summary>
    public float Confidence { get; set; }
}
