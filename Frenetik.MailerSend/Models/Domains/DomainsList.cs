using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Domains;

/// <summary>
/// Response containing a paginated list of domains
/// </summary>
public class DomainsList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the array of domains
    /// </summary>
    [JsonPropertyName("data")]
    public Domain[] Domains { get; set; } = Array.Empty<Domain>();
}
