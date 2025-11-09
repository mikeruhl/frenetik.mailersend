using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Sms;

/// <summary>
/// List of SMS messages
/// </summary>
public class SmsMessagesList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the SMS messages data
    /// </summary>
    [JsonPropertyName("data")]
    public SmsMessage[] Messages { get; set; } = Array.Empty<SmsMessage>();
}
