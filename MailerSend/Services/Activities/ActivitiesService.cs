using MailerSend.Configuration;
using MailerSend.Exceptions;
using MailerSend.Models.Activities;
using MailerSend.Models.Util;
using Microsoft.Extensions.Options;

namespace MailerSend.Services.Activities;

/// <summary>
/// Service implementation for retrieving activity logs
/// </summary>
public class ActivitiesService : ServiceBase, IActivitiesService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ActivitiesService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public ActivitiesService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets activities for a domain
    /// </summary>
    public Task<ActivitiesList> GetActivitiesAsync(string domainId, CancellationToken cancellationToken)
    {
        return GetActivitiesAsync(domainId, null, null, null, null, cancellationToken);
    }

    /// <summary>
    /// Gets activities for a domain (synchronous)
    /// </summary>
    public ActivitiesList GetActivities(string domainId)
    {
        return GetActivitiesAsync(domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets activities for a domain with filtering and pagination
    /// </summary>
    public async Task<ActivitiesList> GetActivitiesAsync(
        string domainId,
        PaginationParameters? pagination = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string[]? events = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(domainId))
            throw new ArgumentException("Domain ID must be provided", nameof(domainId));

        if (dateFrom.HasValue && dateTo.HasValue && dateTo.Value <= dateFrom.Value)
            throw new MailerSendException("From date cannot be after to date");

        pagination ??= new PaginationParameters();
        var endpoint = $"activity/{domainId}";
        var queryParams = new List<string>();

        if (pagination.Page > 0)
            queryParams.Add($"page={pagination.Page}");

        if (pagination.Limit > 0)
            queryParams.Add($"limit={pagination.Limit}");

        if (dateFrom.HasValue)
        {
            var unixTimestamp = new DateTimeOffset(dateFrom.Value).ToUnixTimeSeconds();
            queryParams.Add($"date_from={unixTimestamp}");
        }

        if (dateTo.HasValue)
        {
            var unixTimestamp = new DateTimeOffset(dateTo.Value).ToUnixTimeSeconds();
            queryParams.Add($"date_to={unixTimestamp}");
        }

        if (events != null && events.Length > 0)
        {
            foreach (var eventType in events)
            {
                queryParams.Add($"event[]={eventType}");
            }
        }

        if (queryParams.Count > 0)
        {
            endpoint = $"{endpoint}?{BuildQueryString(queryParams)}";
        }

        var mailerSendHttpClient = CreateHttpClient();
        return await mailerSendHttpClient.GetRequestAsync<ActivitiesList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets activities for a domain with filtering and pagination (synchronous)
    /// </summary>
    public ActivitiesList GetActivities(
        string domainId,
        PaginationParameters? pagination = null,
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string[]? events = null)
    {
        return GetActivitiesAsync(domainId, pagination, dateFrom, dateTo, events).GetAwaiter().GetResult();
    }
}
