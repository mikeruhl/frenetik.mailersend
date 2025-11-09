using System.Text.Json.Serialization;

namespace MailerSend.Models.Util;

/// <summary>
/// Pagination metadata from API responses
/// </summary>
public class ResponseMeta
{
    /// <summary>
    /// Gets or sets the current page number
    /// </summary>
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the starting record number
    /// </summary>
    [JsonPropertyName("from")]
    public int From { get; set; }

    /// <summary>
    /// Gets or sets the API path
    /// </summary>
    [JsonPropertyName("path")]
    public string? Path { get; set; }

    /// <summary>
    /// Gets or sets the number of results per page
    /// </summary>
    [JsonPropertyName("per_page")]
    public int Limit { get; set; }

    /// <summary>
    /// Gets or sets the ending record number
    /// </summary>
    [JsonPropertyName("to")]
    public int To { get; set; }

    /// <summary>
    /// Gets or sets the last page number
    /// </summary>
    [JsonPropertyName("last_page")]
    public int LastPage { get; set; }
}
