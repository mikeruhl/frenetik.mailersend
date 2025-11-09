using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Analytics;

/// <summary>
/// Response containing analytics statistics
/// </summary>
public class AnalyticsList : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the date from
    /// </summary>
    [JsonPropertyName("date_from")]
    [JsonConverter(typeof(UnixTimestampConverter))]
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Gets or sets the date to
    /// </summary>
    [JsonPropertyName("date_to")]
    [JsonConverter(typeof(UnixTimestampConverter))]
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Gets or sets the statistics array
    /// </summary>
    [JsonPropertyName("stats")]
    public AnalyticsStatistic[] Statistics { get; set; } = Array.Empty<AnalyticsStatistic>();
}
