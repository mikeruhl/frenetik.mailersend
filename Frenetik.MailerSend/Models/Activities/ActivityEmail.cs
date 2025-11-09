using System.Text.Json.Serialization;
using Frenetik.MailerSend.Models.Email;
using Frenetik.MailerSend.Models.Util;

namespace Frenetik.MailerSend.Models.Activities;

/// <summary>
/// Email information within an activity
/// </summary>
public class ActivityEmail
{
    /// <summary>
    /// Gets or sets the email ID
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the sender email address
    /// </summary>
    [JsonPropertyName("from")]
    public string? From { get; set; }

    /// <summary>
    /// Gets or sets the email subject
    /// </summary>
    [JsonPropertyName("subject")]
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets the plain text content
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the HTML content
    /// </summary>
    [JsonPropertyName("html")]
    public string? Html { get; set; }

    /// <summary>
    /// Gets or sets the email status
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the email tags
    /// </summary>
    [JsonPropertyName("tags")]
    public string[]? Tags { get; set; }

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
    /// Gets or sets the recipient information
    /// </summary>
    [JsonPropertyName("recipient")]
    public ApiRecipient? Recipient { get; set; }

    /// <summary>
    /// Converts this ActivityEmail to an EmailMessage
    /// </summary>
    public EmailMessage ToEmailMessage()
    {
        var email = new EmailMessage
        {
            Subject = Subject,
            Html = Html,
            Text = Text
        };

        if (!string.IsNullOrEmpty(From))
        {
            email.From = new Recipient(From!);
        }

        if (Tags != null)
        {
            email.Tags = new List<string>(Tags);
        }

        if (Recipient != null)
        {
            email.AddRecipient(Recipient.ToRecipient());
        }

        return email;
    }
}
