using System.Text.Json.Serialization;

namespace MailerSend.Models.Util;

/// <summary>
/// Recipient information returned from the API with timestamps
/// </summary>
public class ApiRecipient
{
    /// <summary>
    /// Gets or sets the recipient ID
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the email address
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation timestamp
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update timestamp
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the deletion timestamp
    /// </summary>
    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Converts this ApiRecipient to a Recipient
    /// </summary>
    public Email.Recipient ToRecipient()
    {
        return new Email.Recipient(Email);
    }
}
