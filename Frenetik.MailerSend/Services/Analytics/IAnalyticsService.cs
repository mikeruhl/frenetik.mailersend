using Frenetik.MailerSend.Models.Analytics;

namespace Frenetik.MailerSend.Services.Analytics;

/// <summary>
/// Service for retrieving analytics data
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Gets analytics data grouped by date
    /// </summary>
    /// <param name="dateFrom">Start date for the analytics period</param>
    /// <param name="dateTo">End date for the analytics period</param>
    /// <param name="events">Event types to include in the analytics</param>
    /// <param name="domainId">Optional domain ID filter</param>
    /// <param name="tags">Optional tags filter</param>
    /// <param name="groupBy">Group by period (days, weeks, months, years). Default: days</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<AnalyticsByDateList> GetByDateAsync(
        DateTime dateFrom,
        DateTime dateTo,
        string[] events,
        string? domainId = null,
        string[]? tags = null,
        string groupBy = "days",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets analytics data grouped by date (synchronous)
    /// </summary>
    AnalyticsByDateList GetByDate(
        DateTime dateFrom,
        DateTime dateTo,
        string[] events,
        string? domainId = null,
        string[]? tags = null,
        string groupBy = "days");

    /// <summary>
    /// Gets email opens analytics grouped by country
    /// </summary>
    /// <param name="dateFrom">Optional start date</param>
    /// <param name="dateTo">Optional end date</param>
    /// <param name="domainId">Optional domain ID filter</param>
    /// <param name="tags">Optional tags filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<AnalyticsList> GetOpensByCountryAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets email opens analytics grouped by country (synchronous)
    /// </summary>
    AnalyticsList GetOpensByCountry(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null);

    /// <summary>
    /// Gets email opens analytics grouped by user agent name
    /// </summary>
    /// <param name="dateFrom">Optional start date</param>
    /// <param name="dateTo">Optional end date</param>
    /// <param name="domainId">Optional domain ID filter</param>
    /// <param name="tags">Optional tags filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<AnalyticsList> GetOpensByUserAgentAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets email opens analytics grouped by user agent name (synchronous)
    /// </summary>
    AnalyticsList GetOpensByUserAgent(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null);

    /// <summary>
    /// Gets email opens analytics grouped by user agent type (desktop, mobile, etc.)
    /// </summary>
    /// <param name="dateFrom">Optional start date</param>
    /// <param name="dateTo">Optional end date</param>
    /// <param name="domainId">Optional domain ID filter</param>
    /// <param name="tags">Optional tags filter</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<AnalyticsList> GetOpensByUserAgentTypeAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets email opens analytics grouped by user agent type (synchronous)
    /// </summary>
    AnalyticsList GetOpensByUserAgentType(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null);
}
