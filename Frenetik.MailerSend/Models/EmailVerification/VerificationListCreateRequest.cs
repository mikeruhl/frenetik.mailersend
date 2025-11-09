using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.EmailVerification;

/// <summary>
/// Request to create an email verification list
/// </summary>
public class VerificationListCreateRequest
{
    /// <summary>
    /// Gets or sets the list name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the emails to verify
    /// </summary>
    [JsonPropertyName("emails")]
    public string[] Emails { get; set; } = Array.Empty<string>();
}
