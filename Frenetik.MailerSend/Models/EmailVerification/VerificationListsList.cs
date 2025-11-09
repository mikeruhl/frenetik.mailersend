using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.EmailVerification;

/// <summary>
/// Paginated list of email verification lists
/// </summary>
public class VerificationListsList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the verification lists data
    /// </summary>
    [JsonPropertyName("data")]
    public VerificationList[] Lists { get; set; } = Array.Empty<VerificationList>();
}
