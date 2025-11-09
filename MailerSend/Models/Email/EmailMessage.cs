using System.Text.Json.Serialization;

namespace MailerSend.Models.Email;

/// <summary>
/// Represents an email message to be sent
/// </summary>
public class EmailMessage
{
    /// <summary>
    /// Gets or sets the recipients
    /// </summary>
    [JsonPropertyName("to")]
    public List<Recipient> Recipients { get; set; } = new();

    /// <summary>
    /// Gets or sets the sender
    /// </summary>
    [JsonPropertyName("from")]
    public Recipient? From { get; set; }

    /// <summary>
    /// Gets or sets the CC recipients
    /// </summary>
    [JsonPropertyName("cc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Recipient>? Cc { get; set; }

    /// <summary>
    /// Gets or sets the BCC recipients
    /// </summary>
    [JsonPropertyName("bcc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Recipient>? Bcc { get; set; }

    /// <summary>
    /// Gets or sets the reply-to address
    /// </summary>
    [JsonPropertyName("reply_to")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Recipient? ReplyTo { get; set; }

    /// <summary>
    /// Gets or sets the email subject
    /// </summary>
    [JsonPropertyName("subject")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets the plain text body
    /// </summary>
    [JsonPropertyName("text")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the HTML body
    /// </summary>
    [JsonPropertyName("html")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Html { get; set; }

    /// <summary>
    /// Gets or sets the template ID
    /// </summary>
    [JsonPropertyName("template_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the tags
    /// </summary>
    [JsonPropertyName("tags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Gets or sets the attachments
    /// </summary>
    [JsonPropertyName("attachments")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Attachment>? Attachments { get; set; }

    /// <summary>
    /// Gets or sets the personalization data
    /// </summary>
    [JsonPropertyName("personalization")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Personalization>? Personalization { get; set; }

    /// <summary>
    /// Gets or sets the scheduled send time (Unix timestamp)
    /// </summary>
    [JsonPropertyName("send_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? SendAt { get; set; }

    /// <summary>
    /// Gets or sets the In-Reply-To header
    /// </summary>
    [JsonPropertyName("in_reply_to")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InReplyTo { get; set; }

    /// <summary>
    /// Gets or sets the List-Unsubscribe header (RFC 8058)
    /// </summary>
    [JsonPropertyName("list_unsubscribe")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ListUnsubscribe { get; set; }

    [JsonIgnore]
    private Dictionary<string, object> _allRecipientsPersonalization = new();

    [JsonIgnore]
    private Dictionary<string, string> _allRecipientsSubstitutions = new();

    /// <summary>
    /// Adds a recipient to the email
    /// </summary>
    public EmailMessage AddRecipient(string email, string? name = null)
    {
        Recipients.Add(new Recipient(email, name));
        return this;
    }

    /// <summary>
    /// Adds a recipient to the email
    /// </summary>
    public EmailMessage AddRecipient(Recipient recipient)
    {
        Recipients.Add(recipient);
        return this;
    }

    /// <summary>
    /// Adds multiple recipients to the email
    /// </summary>
    public EmailMessage AddRecipients(params Recipient[] recipients)
    {
        Recipients.AddRange(recipients);
        return this;
    }

    /// <summary>
    /// Adds a CC recipient
    /// </summary>
    public EmailMessage AddCc(string email, string? name = null)
    {
        Cc ??= new List<Recipient>();
        Cc.Add(new Recipient(email, name));
        return this;
    }

    /// <summary>
    /// Adds a CC recipient
    /// </summary>
    public EmailMessage AddCc(Recipient recipient)
    {
        Cc ??= new List<Recipient>();
        Cc.Add(recipient);
        return this;
    }

    /// <summary>
    /// Adds a BCC recipient
    /// </summary>
    public EmailMessage AddBcc(string email, string? name = null)
    {
        Bcc ??= new List<Recipient>();
        Bcc.Add(new Recipient(email, name));
        return this;
    }

    /// <summary>
    /// Adds a BCC recipient
    /// </summary>
    public EmailMessage AddBcc(Recipient recipient)
    {
        Bcc ??= new List<Recipient>();
        Bcc.Add(recipient);
        return this;
    }

    /// <summary>
    /// Sets the reply-to address
    /// </summary>
    public EmailMessage SetReplyTo(string email, string? name = null)
    {
        ReplyTo = new Recipient(email, name);
        return this;
    }

    /// <summary>
    /// Sets the reply-to address
    /// </summary>
    public EmailMessage SetReplyTo(Recipient replyTo)
    {
        ReplyTo = replyTo;
        return this;
    }

    /// <summary>
    /// Sets the sender
    /// </summary>
    public EmailMessage SetFrom(string email, string? name = null)
    {
        From = new Recipient(email, name);
        return this;
    }

    /// <summary>
    /// Sets the sender
    /// </summary>
    public EmailMessage SetFrom(Recipient from)
    {
        From = from;
        return this;
    }

    /// <summary>
    /// Sets the subject
    /// </summary>
    public EmailMessage SetSubject(string subject)
    {
        Subject = subject;
        return this;
    }

    /// <summary>
    /// Sets the HTML body
    /// </summary>
    public EmailMessage SetHtml(string html)
    {
        Html = html;
        return this;
    }

    /// <summary>
    /// Sets the plain text body
    /// </summary>
    public EmailMessage SetText(string text)
    {
        Text = text;
        return this;
    }

    /// <summary>
    /// Sets the template ID
    /// </summary>
    public EmailMessage SetTemplateId(string templateId)
    {
        TemplateId = templateId;
        return this;
    }

    /// <summary>
    /// Adds personalization for a specific recipient
    /// </summary>
    public EmailMessage AddPersonalization(Recipient recipient, string name, object value)
    {
        Personalization ??= new List<Personalization>();

        var existing = Personalization.FirstOrDefault(p => p.Email == recipient.Email);
        if (existing != null)
        {
            existing.Data[name] = value;
        }
        else
        {
            var newPersonalization = new Personalization
            {
                Email = recipient.Email,
                Data = new Dictionary<string, object> { { name, value } }
            };
            Personalization.Add(newPersonalization);
        }
        return this;
    }

    /// <summary>
    /// Adds personalization for all recipients
    /// </summary>
    public EmailMessage AddPersonalization(string name, object value)
    {
        _allRecipientsPersonalization[name] = value;
        return this;
    }

    /// <summary>
    /// Adds a variable substitution for all recipients
    /// </summary>
    public EmailMessage AddVariable(string name, string value)
    {
        _allRecipientsSubstitutions[name] = value;
        return this;
    }

    /// <summary>
    /// Adds a tag to the email
    /// </summary>
    public EmailMessage AddTag(string tag)
    {
        Tags ??= new List<string>();
        Tags.Add(tag);
        return this;
    }

    /// <summary>
    /// Attaches a file from a file path
    /// </summary>
    public EmailMessage AttachFile(string filePath)
    {
        Attachments ??= new List<Attachment>();
        var attachment = new Attachment();
        attachment.AddAttachmentFromFile(filePath);
        Attachments.Add(attachment);
        return this;
    }

    /// <summary>
    /// Sets the scheduled send time
    /// </summary>
    public EmailMessage SetSendAt(DateTime sendAt)
    {
        SendAt = new DateTimeOffset(sendAt).ToUnixTimeSeconds();
        return this;
    }

    /// <summary>
    /// Sets the In-Reply-To header
    /// </summary>
    public EmailMessage SetInReplyTo(string inReplyTo)
    {
        InReplyTo = inReplyTo;
        return this;
    }

    /// <summary>
    /// Sets the List-Unsubscribe header
    /// </summary>
    public EmailMessage SetListUnsubscribe(string listUnsubscribe)
    {
        ListUnsubscribe = listUnsubscribe;
        return this;
    }

    /// <summary>
    /// Prepares personalization for all recipients before sending
    /// </summary>
    internal void PrepareForSending()
    {
        if (_allRecipientsPersonalization.Count > 0)
        {
            foreach (var recipient in Recipients)
            {
                foreach (var kvp in _allRecipientsPersonalization)
                {
                    AddPersonalization(recipient, kvp.Key, kvp.Value);
                }
            }
        }

        if (_allRecipientsSubstitutions.Count > 0)
        {
            foreach (var recipient in Recipients)
            {
                foreach (var kvp in _allRecipientsSubstitutions)
                {
                    AddPersonalization(recipient, kvp.Key, kvp.Value);
                }
            }
        }
    }
}
