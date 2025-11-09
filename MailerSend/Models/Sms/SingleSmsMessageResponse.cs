using System.Text.Json.Serialization;

namespace MailerSend.Models.Sms;

/// <summary>
/// Response containing a single SMS message
/// </summary>
public class SingleSmsMessageResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the SMS message data
    /// </summary>
    [JsonPropertyName("data")]
    public SmsMessage Message { get; set; } = new SmsMessage();
}
