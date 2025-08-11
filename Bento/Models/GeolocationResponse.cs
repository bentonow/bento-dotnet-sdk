namespace Bento.Models;

/// <summary>
/// Response model for IP geolocation using experimental/geolocation endpoint.
/// Attempts to geolocate the provided IP address with detailed location information.
/// The response will contain null values for keys where data is not available.
/// Only works with IPv4 IP addresses.
/// </summary>
public class GeolocationResponse
{
    /// <summary>
    /// The provided IP address that was geolocated
    /// Original request parameter echoed back
    /// </summary>
    public string? Request { get; set; }
    
    /// <summary>
    /// The IP address that was geolocated
    /// May be the same as Request or processed version
    /// </summary>
    public string? Ip { get; set; }
    
    /// <summary>
    /// Two letter country code (ISO 3166-1 alpha-2)
    /// Example: "US", "CA", "GB"
    /// </summary>
    public string? CountryCode2 { get; set; }
    
    /// <summary>
    /// Three letter country code (ISO 3166-1 alpha-3)
    /// Example: "USA", "CAN", "GBR"
    /// </summary>
    public string? CountryCode3 { get; set; }
    
    /// <summary>
    /// Full name of the country
    /// Example: "United States", "Canada", "United Kingdom"
    /// </summary>
    public string? CountryName { get; set; }
    
    /// <summary>
    /// The continent code
    /// Example: "NA" (North America), "EU" (Europe)
    /// </summary>
    public string? ContinentCode { get; set; }
    
    /// <summary>
    /// The regional name (state/province)
    /// Example: "California", "Ontario", "England"
    /// </summary>
    public string? RegionName { get; set; }
    
    /// <summary>
    /// City name where the IP is located
    /// Example: "San Francisco", "Toronto", "London"
    /// </summary>
    public string? CityName { get; set; }
    
    /// <summary>
    /// Regional postal code (ZIP code)
    /// Example: "94102", "M5V 3A8", "SW1A 1AA"
    /// </summary>
    public string? PostalCode { get; set; }
    
    /// <summary>
    /// Latitude for the IP address (decimal degrees)
    /// Can be null if location data is not available
    /// </summary>
    public double? Latitude { get; set; }
    
    /// <summary>
    /// Longitude for the IP address (decimal degrees)
    /// Can be null if location data is not available
    /// </summary>
    public double? Longitude { get; set; }
    
    /// <summary>
    /// DMA code for the IP address (Designated Market Area)
    /// Used for TV/radio market identification, can be null
    /// </summary>
    public string? DmaCode { get; set; }
    
    /// <summary>
    /// Area code if available for the IP address
    /// Telephone area code, can be null if not available
    /// </summary>
    public string? AreaCode { get; set; }
    
    /// <summary>
    /// Regional time UTC time zone
    /// Example: "America/Los_Angeles", "Europe/London"
    /// </summary>
    public string? Timezone { get; set; }
    
    /// <summary>
    /// The real region name (more detailed than RegionName)
    /// Additional regional identifier, can be null
    /// </summary>
    public string? RealRegionName { get; set; }
}
