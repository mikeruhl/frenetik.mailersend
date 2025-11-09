using System.Text.Json.Serialization;

namespace MailerSend.Models.Templates;

/// <summary>
/// Response containing a single template
/// </summary>
public class SingleTemplateResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the template data
    /// </summary>
    [JsonPropertyName("data")]
    public Template Template { get; set; } = new Template();
}
