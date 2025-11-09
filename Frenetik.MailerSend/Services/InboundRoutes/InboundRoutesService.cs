using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models.InboundRoutes;
using Frenetik.MailerSend.Models.Util;
using Microsoft.Extensions.Options;

namespace Frenetik.MailerSend.Services.InboundRoutes;

/// <summary>
/// Service implementation for managing inbound routes
/// </summary>
public class InboundRoutesService : ServiceBase, IInboundRoutesService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InboundRoutesService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public InboundRoutesService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets a paginated list of inbound routes
    /// </summary>
    public async Task<InboundRoutesList> GetRoutesAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default)
    {
        pagination ??= new PaginationParameters();
        var mailerSendHttpClient = CreateHttpClient();

        var queryParams = new List<string>
        {
            $"page={pagination.Page}",
            $"limit={pagination.Limit}"
        };

        if (!string.IsNullOrEmpty(domainId))
        {
            queryParams.Add($"domain_id={domainId}");
        }

        var endpoint = $"inbound?{BuildQueryString(queryParams)}";
        return await mailerSendHttpClient.GetRequestAsync<InboundRoutesList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of inbound routes (synchronous)
    /// </summary>
    public InboundRoutesList GetRoutes(PaginationParameters? pagination = null, string? domainId = null)
    {
        return GetRoutesAsync(pagination, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single inbound route by ID
    /// </summary>
    public async Task<InboundRoute> GetRouteAsync(string routeId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"inbound/{routeId}";
        var response = await mailerSendHttpClient.GetRequestAsync<SingleInboundRouteResponse>(endpoint, cancellationToken);
        return response.Route;
    }

    /// <summary>
    /// Gets a single inbound route by ID (synchronous)
    /// </summary>
    public InboundRoute GetRoute(string routeId)
    {
        return GetRouteAsync(routeId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Deletes an inbound route
    /// </summary>
    public async Task<bool> DeleteRouteAsync(string routeId, CancellationToken cancellationToken = default)
    {
        try
        {
            var mailerSendHttpClient = CreateHttpClient();

            var endpoint = $"inbound/{routeId}";
            var response = await mailerSendHttpClient.DeleteRequestAsync<Models.MailerSendResponse>(endpoint, cancellationToken);

            return IsSuccessStatusCode(response.ResponseStatusCode);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes an inbound route (synchronous)
    /// </summary>
    public bool DeleteRoute(string routeId)
    {
        return DeleteRouteAsync(routeId).GetAwaiter().GetResult();
    }
}
