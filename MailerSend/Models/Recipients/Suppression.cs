using System.Text.Json.Serialization;

namespace MailerSend.Models.Recipients;

/// <summary>
/// Represents a suppressed recipient
/// </summary>
public class Suppression
{
    /// <summary>
    /// Gets or sets the suppression ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the recipient email address
    /// </summary>
    [JsonPropertyName("recipient")]
    public string Recipient { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation date
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the reason for suppression
    /// </summary>
    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}
