using System.Text.Json.Serialization;

namespace MailerSend.Models.Activities;

/// <summary>
/// Response containing a list of activities with pagination
/// </summary>
public class ActivitiesList : PaginatedResponse
{
    /// <summary>
    /// Gets or sets the array of activities
    /// </summary>
    [JsonPropertyName("data")]
    public Activity[] Activities { get; set; } = Array.Empty<Activity>();
}
