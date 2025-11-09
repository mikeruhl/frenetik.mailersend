using System.Text.Json;
using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Email;

/// <summary>
/// Status of a bulk email send operation
/// </summary>
public class BulkSendStatus
{
    /// <summary>
    /// Gets or sets the bulk send ID
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the state of the bulk send
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the total recipients count
    /// </summary>
    [JsonPropertyName("total_recipients_count")]
    public int TotalRecipientsCount { get; set; }

    /// <summary>
    /// Gets or sets the suppressed recipients count
    /// </summary>
    [JsonPropertyName("suppressed_recipients_count")]
    public int SuppressedRecipientsCount { get; set; }

    /// <summary>
    /// Gets or sets the suppressed recipients data
    /// </summary>
    [JsonPropertyName("suppressed_recipients")]
    public JsonElement? SuppressedRecipients { get; set; }

    /// <summary>
    /// Gets or sets the validation errors count
    /// </summary>
    [JsonPropertyName("validation_errors_count")]
    public int ValidationErrorsCount { get; set; }

    /// <summary>
    /// Gets or sets the validation errors data
    /// </summary>
    [JsonPropertyName("validation_errors")]
    public JsonElement? ValidationErrors { get; set; }

    /// <summary>
    /// Gets or sets the message IDs
    /// </summary>
    [JsonPropertyName("messages_id")]
    public string[]? MessagesId { get; set; }

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
}
