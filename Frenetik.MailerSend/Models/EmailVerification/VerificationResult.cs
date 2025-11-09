using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.EmailVerification;

/// <summary>
/// Email verification result
/// </summary>
public class VerificationResult
{
    /// <summary>
    /// Gets or sets the email address
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the verification result
    /// </summary>
    [JsonPropertyName("result")]
    public string Result { get; set; } = string.Empty;
}
