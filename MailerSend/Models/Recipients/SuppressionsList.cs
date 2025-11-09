using System.Text.Json.Serialization;

namespace MailerSend.Models.Recipients;

/// <summary>
/// Paginated list of suppressions
/// </summary>
public class SuppressionsList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the suppressions data
    /// </summary>
    [JsonPropertyName("data")]
    public Suppression[] Suppressions { get; set; } = Array.Empty<Suppression>();
}
