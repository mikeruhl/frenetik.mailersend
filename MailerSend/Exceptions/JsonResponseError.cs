using System.Text.Json.Serialization;

namespace MailerSend.Exceptions;

/// <summary>
/// Internal class used for deserializing error responses from the API
/// </summary>
internal class JsonResponseError
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("errors")]
    public Dictionary<string, string[]> Errors { get; set; } = new();
}
