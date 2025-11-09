using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Domains;

/// <summary>
/// Response containing domain verification status
/// </summary>
public class DomainVerificationStatus : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the verification message
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the verification status attributes
    /// </summary>
    [JsonPropertyName("data")]
    public DomainVerificationAttributes Status { get; set; } = new DomainVerificationAttributes();
}
