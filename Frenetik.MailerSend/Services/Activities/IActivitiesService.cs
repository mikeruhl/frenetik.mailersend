using Frenetik.MailerSend.Models.Activities;
using Frenetik.MailerSend.Models.Util;

namespace Frenetik.MailerSend.Services.Activities;

/// <summary>
/// Service for retrieving activity logs
/// </summary>
public interface IActivitiesService
{
    /// <summary>
    /// Gets activities for a domain
    /// </summary>
    /// <param name="domainId">The domain ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<ActivitiesList> GetActivitiesAsync(string domainId, CancellationToken cancellationToken);

    /// <summary>
    /// Gets activities for a domain (synchronous)
    /// </summary>
    /// <param name="domainId">The domain ID</param>
    ActivitiesList GetActivities(string domainId);

    /// <summary>
    /// Gets activities for a domain with filtering and pagination
    /// </summary>
    /// <param name="domainId">The domain ID</param>
    /// <param name="pagination">Pagination parameters (page and limit)</param>
    /// <param name="dateFrom">Filter activities from this date</param>
    /// <param name="dateTo">Filter activities to this date</param>
    /// <param name="events">Filter by event types (e.g., sent, delivered, opened, clicked)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<ActivitiesList> GetActivitiesAsync(
        string domainId,
        PaginationParameters? pagination = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string[]? events = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets activities for a domain with filtering and pagination (synchronous)
    /// </summary>
    /// <param name="domainId">The domain ID</param>
    /// <param name="pagination">Pagination parameters (page and limit)</param>
    /// <param name="dateFrom">Filter activities from this date</param>
    /// <param name="dateTo">Filter activities to this date</param>
    /// <param name="events">Filter by event types (e.g., sent, delivered, opened, clicked)</param>
    ActivitiesList GetActivities(
        string domainId,
        PaginationParameters? pagination = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string[]? events = null);
}
