using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.EmailVerification;

/// <summary>
/// Email verification status
/// </summary>
public class VerificationStatus
{
    /// <summary>
    /// Gets or sets the status name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the count
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }
}
