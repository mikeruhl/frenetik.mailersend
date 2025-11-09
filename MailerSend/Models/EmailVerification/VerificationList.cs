using System.Text.Json.Serialization;

namespace MailerSend.Models.EmailVerification;

/// <summary>
/// Represents an email verification list
/// </summary>
public class VerificationList
{
    /// <summary>
    /// Gets or sets the list ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total number of emails
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>
    /// Gets or sets when verification started
    /// </summary>
    [JsonPropertyName("verification_started")]
    public DateTime? VerificationStarted { get; set; }

    /// <summary>
    /// Gets or sets when verification ended
    /// </summary>
    [JsonPropertyName("verification_ended")]
    public DateTime? VerificationEnded { get; set; }

    /// <summary>
    /// Gets or sets the creation date
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the source
    /// </summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the status
    /// </summary>
    [JsonPropertyName("status")]
    public VerificationStatus? Status { get; set; }

    /// <summary>
    /// Gets or sets the statistics
    /// </summary>
    [JsonPropertyName("statistics")]
    public VerificationStatistics? Statistics { get; set; }
}
