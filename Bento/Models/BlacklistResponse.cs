namespace Bento.Models;

/// <summary>
/// Response model for blacklist checking
/// </summary>
public class BlacklistResponse
{
    /// <summary>
    /// The query that was checked
    /// </summary>
    public string? Query { get; set; }
    
    /// <summary>
    /// Description of the check
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Blacklist check results (can be null if all checks return false)
    /// </summary>
    public BlacklistResults? Results { get; set; }
}

/// <summary>
/// Blacklist check results
/// </summary>
public class BlacklistResults
{
    /// <summary>
    /// Domain age is flagged
    /// </summary>
    public bool JustRegistered { get; set; }
    
    /// <summary>
    /// Spamhaus considers this spam
    /// </summary>
    public bool Spamhaus { get; set; }
    
    /// <summary>
    /// Nordspam considers this spam
    /// </summary>
    public bool Nordspam { get; set; }
    
    /// <summary>
    /// Spfbl considers this spam
    /// </summary>
    public bool Spfbl { get; set; }
    
    /// <summary>
    /// Sorbs considers this spam
    /// </summary>
    public bool Sorbs { get; set; }
    
    /// <summary>
    /// Abusix considers this spam
    /// </summary>
    public bool Abusix { get; set; }
}
