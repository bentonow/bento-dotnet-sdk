using System.Text.Json.Serialization;

namespace Bento.Models;

/// <summary>
/// Response model for report statistics data.
/// Contains report data with chart style, report type and name.
/// Used by the GET /v1/stats/report endpoint.
/// See <see href="https://docs.bentonow.com/stats#get-report-stats" />.
/// </summary>
public record ReportStatsResponse
{
    /// <summary>
    /// Contains the detailed report data including actual data points and metadata.
    /// </summary>
    [JsonPropertyName("report_data")]
    public ReportData? ReportData { get; init; }
}

/// <summary>
/// Contains the detailed report information including data points and metadata.
/// </summary>
public record ReportData
{
    /// <summary>
    /// The actual data points for the report.
    /// Structure depends on the specific report type.
    /// </summary>
    [JsonPropertyName("data")]
    public object? Data { get; init; }

    /// <summary>
    /// The chart style used for displaying the report.
    /// </summary>
    /// <example>count</example>
    [JsonPropertyName("chart_style")]
    public string? ChartStyle { get; init; }

    /// <summary>
    /// The type of the report (internal Bento report classification).
    /// </summary>
    /// <example>Reporting::Reports::VisitorCountReport</example>
    [JsonPropertyName("report_type")]
    public string? ReportType { get; init; }

    /// <summary>
    /// The human-readable name of the report.
    /// </summary>
    /// <example>New Subscribers</example>
    [JsonPropertyName("report_name")]
    public string? ReportName { get; init; }
}
