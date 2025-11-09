using System.Text.Json.Serialization;

namespace MailerSend.Models.Sms;

/// <summary>
/// SMS personalization data
/// </summary>
public class SmsPersonalization
{
    /// <summary>
    /// Gets or sets the phone number
    /// </summary>
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the personalization data
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
}
