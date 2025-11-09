using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Sms;

/// <summary>
/// Response containing a single phone number
/// </summary>
public class SinglePhoneNumberResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the phone number data
    /// </summary>
    [JsonPropertyName("data")]
    public PhoneNumber PhoneNumber { get; set; } = new PhoneNumber();
}
