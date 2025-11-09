namespace Frenetik.MailerSend.Configuration;

/// <summary>
/// Configuration options for the MailerSend client
/// </summary>
public class MailerSendOptions
{
    /// <summary>
    /// Gets or sets the MailerSend API token
    /// </summary>
    public string? ApiToken { get; set; }

    /// <summary>
    /// Gets or sets the base URL for the MailerSend API
    /// </summary>
    public string BaseUrl { get; set; } = "https://api.mailersend.com/v1/";

    /// <summary>
    /// Gets or sets the timeout for HTTP requests in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 100;
}
