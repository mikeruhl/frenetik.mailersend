using System.Text.Json.Serialization;

namespace MailerSend.Models.EmailVerification;

/// <summary>
/// Response containing a single email verification list
/// </summary>
public class SingleVerificationListResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the verification list data
    /// </summary>
    [JsonPropertyName("data")]
    public VerificationList List { get; set; } = new VerificationList();
}
