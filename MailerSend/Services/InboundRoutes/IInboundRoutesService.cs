using MailerSend.Models.InboundRoutes;
using MailerSend.Models.Util;

namespace MailerSend.Services.InboundRoutes;

/// <summary>
/// Service for managing inbound routes
/// </summary>
public interface IInboundRoutesService
{
    /// <summary>
    /// Gets a paginated list of inbound routes
    /// </summary>
    Task<InboundRoutesList> GetRoutesAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of inbound routes (synchronous)
    /// </summary>
    InboundRoutesList GetRoutes(PaginationParameters? pagination = null, string? domainId = null);

    /// <summary>
    /// Gets a single inbound route by ID
    /// </summary>
    Task<InboundRoute> GetRouteAsync(string routeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single inbound route by ID (synchronous)
    /// </summary>
    InboundRoute GetRoute(string routeId);

    /// <summary>
    /// Deletes an inbound route
    /// </summary>
    Task<bool> DeleteRouteAsync(string routeId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an inbound route (synchronous)
    /// </summary>
    bool DeleteRoute(string routeId);
}
