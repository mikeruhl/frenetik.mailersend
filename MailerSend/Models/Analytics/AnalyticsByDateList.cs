using System.Text.Json.Serialization;

namespace MailerSend.Models.Analytics;

/// <summary>
/// Response containing analytics data grouped by date
/// </summary>
public class AnalyticsByDateList : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the array of analytics by date
    /// </summary>
    [JsonPropertyName("data")]
    public AnalyticsByDate[] Data { get; set; } = Array.Empty<AnalyticsByDate>();
}
