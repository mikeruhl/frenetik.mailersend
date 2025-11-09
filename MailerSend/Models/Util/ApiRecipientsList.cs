using System.Text.Json.Serialization;

namespace MailerSend.Models.Util;

/// <summary>
/// Response containing a paginated list of API recipients
/// </summary>
public class ApiRecipientsList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the array of recipients
    /// </summary>
    [JsonPropertyName("data")]
    public ApiRecipient[] Recipients { get; set; } = Array.Empty<ApiRecipient>();
}
