using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Sms;

/// <summary>
/// Request to send an SMS
/// </summary>
public class SmsSendRequest
{
    /// <summary>
    /// Gets or sets the sender phone number
    /// </summary>
    [JsonPropertyName("from")]
    public string From { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipient phone numbers
    /// </summary>
    [JsonPropertyName("to")]
    public List<string> To { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the SMS text
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the personalization data
    /// </summary>
    [JsonPropertyName("personalization")]
    public List<SmsPersonalization>? Personalization { get; set; }
}
