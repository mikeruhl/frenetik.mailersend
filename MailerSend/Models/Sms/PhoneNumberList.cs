using System.Text.Json.Serialization;

namespace MailerSend.Models.Sms;

/// <summary>
/// List of phone numbers
/// </summary>
public class PhoneNumberList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the phone numbers data
    /// </summary>
    [JsonPropertyName("data")]
    public PhoneNumber[] PhoneNumbers { get; set; } = Array.Empty<PhoneNumber>();
}
