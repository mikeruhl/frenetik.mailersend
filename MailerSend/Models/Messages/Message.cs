using System.Text.Json.Serialization;
using MailerSend.Models.Domains;
using MailerSend.Models.Util;

namespace MailerSend.Models.Messages;

/// <summary>
/// Represents a complete message with all details
/// </summary>
public class Message
{
    /// <summary>
    /// Gets or sets the message ID
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

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
    /// Gets or sets the array of emails
    /// </summary>
    [JsonPropertyName("emails")]
    public ApiEmail[] Emails { get; set; } = Array.Empty<ApiEmail>();

    /// <summary>
    /// Gets or sets the domain
    /// </summary>
    [JsonPropertyName("domain")]
    public Domain? Domain { get; set; }
}
