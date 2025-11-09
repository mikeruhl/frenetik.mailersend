using System.Text.Json.Serialization;

namespace MailerSend.Models.Templates;

/// <summary>
/// Paginated list of templates
/// </summary>
public class TemplatesList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the templates data
    /// </summary>
    [JsonPropertyName("data")]
    public Template[] Templates { get; set; } = Array.Empty<Template>();
}
