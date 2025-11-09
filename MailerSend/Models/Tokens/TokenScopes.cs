namespace MailerSend.Models.Tokens;

/// <summary>
/// Available token scopes for API access
/// </summary>
public static class TokenScopes
{
    /// <summary>
    /// Full email permissions
    /// </summary>
    public const string EmailFull = "email_full";

    /// <summary>
    /// Read-only domains permissions
    /// </summary>
    public const string DomainsRead = "domains_read";

    /// <summary>
    /// Full domains permissions
    /// </summary>
    public const string DomainsFull = "domains_full";

    /// <summary>
    /// Read-only activity permissions
    /// </summary>
    public const string ActivityRead = "activity_read";

    /// <summary>
    /// Full activity permissions
    /// </summary>
    public const string ActivityFull = "activity_full";

    /// <summary>
    /// Read-only analytics permissions
    /// </summary>
    public const string AnalyticsRead = "analytics_read";

    /// <summary>
    /// Full analytics permissions
    /// </summary>
    public const string AnalyticsFull = "analytics_full";

    /// <summary>
    /// Full tokens permissions
    /// </summary>
    public const string TokensFull = "tokens_full";

    /// <summary>
    /// Full webhooks permissions
    /// </summary>
    public const string WebhooksFull = "webhooks_full";

    /// <summary>
    /// Full templates permissions
    /// </summary>
    public const string TemplatesFull = "templates_full";

    /// <summary>
    /// All valid scopes
    /// </summary>
    public static readonly string[] AllScopes = new[]
    {
        EmailFull,
        DomainsRead,
        DomainsFull,
        ActivityRead,
        ActivityFull,
        AnalyticsRead,
        AnalyticsFull,
        TokensFull,
        WebhooksFull,
        TemplatesFull
    };
}
