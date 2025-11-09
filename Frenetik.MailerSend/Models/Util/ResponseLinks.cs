using System.Text.Json.Serialization;

namespace Frenetik.MailerSend.Models.Util;

/// <summary>
/// Pagination links from API responses
/// </summary>
public class ResponseLinks
{
    /// <summary>
    /// Gets or sets the URL to the first page
    /// </summary>
    [JsonPropertyName("first")]
    public string? First { get; set; }

    /// <summary>
    /// Gets or sets the URL to the last page
    /// </summary>
    [JsonPropertyName("last")]
    public string? Last { get; set; }

    /// <summary>
    /// Gets or sets the URL to the previous page
    /// </summary>
    [JsonPropertyName("prev")]
    public string? Prev { get; set; }

    /// <summary>
    /// Gets or sets the URL to the next page
    /// </summary>
    [JsonPropertyName("next")]
    public string? Next { get; set; }
}
