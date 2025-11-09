using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Domains;

/// <summary>
/// Response containing a single domain
/// </summary>
public class SingleDomainResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets the domain
    /// </summary>
    [JsonPropertyName("data")]
    public Domain Domain { get; set; } = new Domain();
}
