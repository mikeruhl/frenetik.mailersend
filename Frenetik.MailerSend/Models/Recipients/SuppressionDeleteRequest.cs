using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Recipients;

/// <summary>
/// Request to delete recipients from a suppression list
/// </summary>
public class SuppressionDeleteRequest
{
    /// <summary>
    /// Gets or sets the domain ID (optional)
    /// </summary>
    [JsonPropertyName("domain_id")]
    public string? DomainId { get; set; }

    /// <summary>
    /// Gets or sets the suppression IDs to delete
    /// </summary>
    [JsonPropertyName("ids")]
    public string[] Ids { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Gets or sets whether to delete all suppressions
    /// </summary>
    [JsonPropertyName("all")]
    public bool? All { get; set; }
}
