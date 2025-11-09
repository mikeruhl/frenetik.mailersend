using System.Text.Json.Serialization;
using MailerSend.Models.Util;

namespace MailerSend.Models;

/// <summary>
/// Base class for paginated API responses
/// </summary>
public abstract class PaginatedResponse : MailerSendResponse
{
    /// <summary>
    /// Gets or sets pagination links
    /// </summary>
    [JsonPropertyName("links")]
    public ResponseLinks? Links { get; set; }

    /// <summary>
    /// Gets or sets pagination metadata
    /// </summary>
    [JsonPropertyName("meta")]
    public ResponseMeta? Meta { get; set; }

    /// <summary>
    /// Gets the current page number
    /// </summary>
    public int CurrentPage => Meta?.CurrentPage ?? 1;

    /// <summary>
    /// Gets the last page number
    /// </summary>
    public int LastPage => Meta?.LastPage ?? 1;

    /// <summary>
    /// Gets the number of items per page
    /// </summary>
    public int PerPage => Meta?.Limit ?? 25;
}
