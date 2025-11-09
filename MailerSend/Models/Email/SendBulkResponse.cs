using System.Text.Json.Serialization;

namespace MailerSend.Models.Email;

/// <summary>
/// Response from bulk email send operation
/// </summary>
public class SendBulkResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the response message
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the bulk send ID
    /// </summary>
    [JsonPropertyName("bulk_email_id")]
    public string? BulkSendId { get; set; }
}
