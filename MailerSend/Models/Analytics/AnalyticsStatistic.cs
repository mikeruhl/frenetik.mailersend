using System.Text.Json.Serialization;

namespace MailerSend.Models.Analytics;

/// <summary>
/// Analytics statistic entry
/// </summary>
public class AnalyticsStatistic
{
    /// <summary>
    /// Gets or sets the name of the statistic
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the count
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}
