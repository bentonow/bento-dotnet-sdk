namespace Bento.Models;

/// <summary>
/// Response model for IP geolocation.
/// Note: Response may contain null values for some keys depending on data availability.
/// </summary>
public class GeolocationResponse
{
    /// <summary>
    /// Original IP request
    /// </summary>
    public string? Request { get; set; }
    
    /// <summary>
    /// Geolocated IP address
    /// </summary>
    public string? Ip { get; set; }
    
    /// <summary>
    /// Two letter country code
    /// </summary>
    public string? CountryCode2 { get; set; }
    
    /// <summary>
    /// Three letter country code
    /// </summary>
    public string? CountryCode3 { get; set; }
    
    /// <summary>
    /// Full country name
    /// </summary>
    public string? CountryName { get; set; }
    
    /// <summary>
    /// Continent code
    /// </summary>
    public string? ContinentCode { get; set; }
    
    /// <summary>
    /// Regional name
    /// </summary>
    public string? RegionName { get; set; }
    
    /// <summary>
    /// City name
    /// </summary>
    public string? CityName { get; set; }
    
    /// <summary>
    /// Regional postal code
    /// </summary>
    public string? PostalCode { get; set; }
    
    /// <summary>
    /// Latitude (can be null)
    /// </summary>
    public double? Latitude { get; set; }
    
    /// <summary>
    /// Longitude (can be null)
    /// </summary>
    public double? Longitude { get; set; }
    
    /// <summary>
    /// DMA code (can be null)
    /// </summary>
    public string? DmaCode { get; set; }
    
    /// <summary>
    /// Area code (can be null if not available)
    /// </summary>
    public string? AreaCode { get; set; }
    
    /// <summary>
    /// Regional time UTC timezone
    /// </summary>
    public string? Timezone { get; set; }
    
    /// <summary>
    /// Real region name
    /// </summary>
    public string? RealRegionName { get; set; }
}
