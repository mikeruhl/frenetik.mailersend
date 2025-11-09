using System.Text.Json;
using System.Text.Json.Serialization;

namespace MailerSend.Models.Analytics;

/// <summary>
/// Analytics data for a specific date
/// </summary>
public class AnalyticsByDate
{
    /// <summary>
    /// Gets or sets the date for this statistic
    /// </summary>
    [JsonPropertyName("date")]
    [JsonConverter(typeof(UnixTimestampConverter))]
    public DateTime? StatDate { get; set; }

    /// <summary>
    /// Gets or sets the number of processed emails
    /// </summary>
    [JsonPropertyName("processed")]
    public int Processed { get; set; }

    /// <summary>
    /// Gets or sets the number of queued emails
    /// </summary>
    [JsonPropertyName("queued")]
    public int Queued { get; set; }

    /// <summary>
    /// Gets or sets the number of sent emails
    /// </summary>
    [JsonPropertyName("sent")]
    public int Sent { get; set; }

    /// <summary>
    /// Gets or sets the number of delivered emails
    /// </summary>
    [JsonPropertyName("delivered")]
    public int Delivered { get; set; }

    /// <summary>
    /// Gets or sets the number of soft bounced emails
    /// </summary>
    [JsonPropertyName("soft_bounced")]
    public int SoftBounced { get; set; }

    /// <summary>
    /// Gets or sets the number of hard bounced emails
    /// </summary>
    [JsonPropertyName("hard_bounced")]
    public int HardBounced { get; set; }

    /// <summary>
    /// Gets or sets the number of emails marked as junk
    /// </summary>
    [JsonPropertyName("junk")]
    public int Junk { get; set; }

    /// <summary>
    /// Gets or sets the number of opened emails
    /// </summary>
    [JsonPropertyName("opened")]
    public int Opened { get; set; }

    /// <summary>
    /// Gets or sets the number of clicked emails
    /// </summary>
    [JsonPropertyName("clicked")]
    public int Clicked { get; set; }

    /// <summary>
    /// Gets or sets the number of unsubscribed emails
    /// </summary>
    [JsonPropertyName("unsubscribed")]
    public int Unsubscribed { get; set; }

    /// <summary>
    /// Gets or sets the number of spam complaints
    /// </summary>
    [JsonPropertyName("spam_complaints")]
    public int SpamComplaints { get; set; }
}

/// <summary>
/// JSON converter for Unix timestamps
/// </summary>
internal class UnixTimestampConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
            return null;

        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrEmpty(stringValue))
                return null;

            if (long.TryParse(stringValue, out var unixTimestamp))
            {
                return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
            }
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            var unixTimestamp = reader.GetInt64();
            return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
        }

        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            var unixTimestamp = new DateTimeOffset(value.Value).ToUnixTimeSeconds();
            writer.WriteStringValue(unixTimestamp.ToString());
        }
        else
        {
            writer.WriteNullValue();
        }
    }
}
