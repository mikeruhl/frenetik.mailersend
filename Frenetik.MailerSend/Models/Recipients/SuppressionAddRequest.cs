using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Recipients;

/// <summary>
/// Request to add recipients to a suppression list
/// </summary>
public class SuppressionAddRequest
{
    /// <summary>
    /// Gets or sets the domain ID (optional)
    /// </summary>
    [JsonPropertyName("domain_id")]
    public string? DomainId { get; set; }

    /// <summary>
    /// Gets or sets the recipient email addresses to add
    /// </summary>
    [JsonPropertyName("recipients")]
    public string[] Recipients { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets patterns (for blocklist only)
    /// </summary>
    [JsonPropertyName("patterns")]
    public string[]? Patterns { get; set; }
}
