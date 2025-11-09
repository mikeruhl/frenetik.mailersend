namespace MailerSend.Models;

/// <summary>
/// Base class for API responses from MailerSend
/// </summary>
public class MailerSendResponse
{
    /// <summary>
    /// Gets or sets the HTTP response status code
    /// </summary>
    public int ResponseStatusCode { get; set; }

    /// <summary>
    /// Gets or sets the message ID (present for email send responses)
    /// </summary>
    public string? MessageId { get; set; }

    /// <summary>
    /// Gets or sets the rate limit for the API
    /// </summary>
    public int RateLimit { get; set; }

    /// <summary>
    /// Gets or sets the remaining rate limit
    /// </summary>
    public int RateLimitRemaining { get; set; }

    /// <summary>
    /// Gets or sets the response headers
    /// </summary>
    public Dictionary<string, IEnumerable<string>> Headers { get; set; } = new();
}
