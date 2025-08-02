namespace Bento;

public class BentoOptions
{
    public const string SectionName = "Bento";
    public string PublishableKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
    public string SiteUuid { get; set; } = string.Empty;
}