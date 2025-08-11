namespace Bento.Models;

/// <summary>
/// Request model for blacklist checking
/// </summary>
public class BlacklistRequest
{
    /// <summary>
    /// Domain to check (e.g., google.com)
    /// </summary>
    public string? Domain { get; set; }
    
    /// <summary>
    /// IP address to check (IPv4 only)
    /// </summary>
    public string? IpAddress { get; set; }
}
