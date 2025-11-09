using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.EmailVerification;

/// <summary>
/// List of email verification results
/// </summary>
public class VerificationResultsList : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the verification results data
    /// </summary>
    [JsonPropertyName("data")]
    public VerificationResult[] Results { get; set; } = Array.Empty<VerificationResult>();
}
