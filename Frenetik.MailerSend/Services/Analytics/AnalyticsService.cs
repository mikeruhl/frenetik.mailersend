using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Exceptions;
using Frenetik.MailerSend.Models.Analytics;
using Microsoft.Extensions.Options;

namespace Frenetik.MailerSend.Services.Analytics;

/// <summary>
/// Service implementation for retrieving analytics data
/// </summary>
public class AnalyticsService : ServiceBase, IAnalyticsService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AnalyticsService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public AnalyticsService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets analytics data grouped by date
    /// </summary>
    public async Task<AnalyticsByDateList> GetByDateAsync(
        DateTime dateFrom,
        DateTime dateTo,
        string[] events,
        string? domainId = null,
        string[]? tags = null,
        string groupBy = "days",
        CancellationToken cancellationToken = default)
    {
        if (events == null || events.Length == 0)
            throw new MailerSendException("No events passed");

        var queryParams = new List<string>
        {
            $"group_by={groupBy}"
        };

        AddArrayParameter(queryParams, events, "event");
        AddOptionalParameters(queryParams, dateFrom, dateTo, domainId, tags);

        var endpoint = $"analytics/date?{BuildQueryString(queryParams)}";

        var mailerSendHttpClient = CreateHttpClient();
        return await mailerSendHttpClient.GetRequestAsync<AnalyticsByDateList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets analytics data grouped by date (synchronous)
    /// </summary>
    public AnalyticsByDateList GetByDate(
        DateTime dateFrom,
        DateTime dateTo,
        string[] events,
        string? domainId = null,
        string[]? tags = null,
        string groupBy = "days")
    {
        return GetByDateAsync(dateFrom, dateTo, events, domainId, tags, groupBy).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets email opens analytics grouped by country
    /// </summary>
    public Task<AnalyticsList> GetOpensByCountryAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null,
        CancellationToken cancellationToken = default)
    {
        return GetAnalyticsAsync("analytics/country", dateFrom, dateTo, domainId, tags, cancellationToken);
    }

    /// <summary>
    /// Gets email opens analytics grouped by country (synchronous)
    /// </summary>
    public AnalyticsList GetOpensByCountry(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null)
    {
        return GetOpensByCountryAsync(dateFrom, dateTo, domainId, tags).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets email opens analytics grouped by user agent name
    /// </summary>
    public Task<AnalyticsList> GetOpensByUserAgentAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null,
        CancellationToken cancellationToken = default)
    {
        return GetAnalyticsAsync("analytics/ua-name", dateFrom, dateTo, domainId, tags, cancellationToken);
    }

    /// <summary>
    /// Gets email opens analytics grouped by user agent name (synchronous)
    /// </summary>
    public AnalyticsList GetOpensByUserAgent(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null)
    {
        return GetOpensByUserAgentAsync(dateFrom, dateTo, domainId, tags).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets email opens analytics grouped by user agent type
    /// </summary>
    public Task<AnalyticsList> GetOpensByUserAgentTypeAsync(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null,
        CancellationToken cancellationToken = default)
    {
        return GetAnalyticsAsync("analytics/ua-type", dateFrom, dateTo, domainId, tags, cancellationToken);
    }

    /// <summary>
    /// Gets email opens analytics grouped by user agent type (synchronous)
    /// </summary>
    public AnalyticsList GetOpensByUserAgentType(
        DateTime? dateFrom = null,
        DateTime? dateTo = null,
        string? domainId = null,
        string[]? tags = null)
    {
        return GetOpensByUserAgentTypeAsync(dateFrom, dateTo, domainId, tags).GetAwaiter().GetResult();
    }

    private async Task<AnalyticsList> GetAnalyticsAsync(
        string endpoint,
        DateTime? dateFrom,
        DateTime? dateTo,
        string? domainId,
        string[]? tags,
        CancellationToken cancellationToken)
    {
        var queryParams = new List<string>();
        AddOptionalParameters(queryParams, dateFrom, dateTo, domainId, tags);

        if (queryParams.Count > 0)
        {
            endpoint = $"{endpoint}?{BuildQueryString(queryParams)}";
        }

        var mailerSendHttpClient = CreateHttpClient();
        return await mailerSendHttpClient.GetRequestAsync<AnalyticsList>(endpoint, cancellationToken);
    }

    private void AddOptionalParameters(
        List<string> queryParams,
        DateTime? dateFrom,
        DateTime? dateTo,
        string? domainId,
        string[]? tags)
    {
        if (domainId != null)
            queryParams.Add($"domain_id={domainId}");

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

        if (tags != null && tags.Length > 0)
        {
            AddArrayParameter(queryParams, tags, "tags");
        }
    }

    private void AddArrayParameter(List<string> queryParams, string[] values, string paramName)
    {
        foreach (var value in values)
        {
            queryParams.Add($"{paramName}[]={value}");
        }
    }
}
