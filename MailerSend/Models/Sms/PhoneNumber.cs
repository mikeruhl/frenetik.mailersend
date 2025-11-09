using System.Text.Json.Serialization;

namespace MailerSend.Models.Sms;

/// <summary>
/// Phone number
/// </summary>
public class PhoneNumber
{
    /// <summary>
    /// Gets or sets the phone number ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the telephone number
    /// </summary>
    [JsonPropertyName("telephone_number")]
    public string TelephoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the phone number is paused
    /// </summary>
    [JsonPropertyName("paused")]
    public bool Paused { get; set; }

    /// <summary>
    /// Gets or sets the creation date
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }
}
