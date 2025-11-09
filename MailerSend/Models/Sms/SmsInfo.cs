using System.Text.Json.Serialization;

namespace MailerSend.Models.Sms;

/// <summary>
/// SMS information
/// </summary>
public class SmsInfo
{
    /// <summary>
    /// Gets or sets the SMS ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sender phone number
    /// </summary>
    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipient phone number
    /// </summary>
    [JsonPropertyName("to")]
    public string To { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the SMS text
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the SMS status
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the segment count
    /// </summary>
    [JsonPropertyName("segment_count")]
    public int SegmentCount { get; set; }

    /// <summary>
    /// Gets or sets the error type
    /// </summary>
    [JsonPropertyName("error_type")]
    public string? ErrorType { get; set; }

    /// <summary>
    /// Gets or sets the error description
    /// </summary>
    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }

    /// <summary>
    /// Gets or sets the creation date
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }
}
