using System.Text.Json.Serialization;

namespace MailerSend.Models.Sms;

/// <summary>
/// SMS message
/// </summary>
public class SmsMessage
{
    /// <summary>
    /// Gets or sets the message ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sender phone number
    /// </summary>
    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipient phone numbers
    /// </summary>
    [JsonPropertyName("to")]
    public string[] To { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets the SMS text
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the message is paused
    /// </summary>
    [JsonPropertyName("paused")]
    public bool Paused { get; set; }

    /// <summary>
    /// Gets or sets the creation date
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the SMS information
    /// </summary>
    [JsonPropertyName("sms")]
    public SmsInfo? Sms { get; set; }
}
