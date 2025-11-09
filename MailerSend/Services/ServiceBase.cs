using MailerSend.Configuration;
using MailerSend.Http;
using Microsoft.Extensions.Options;

namespace MailerSend.Services;

/// <summary>
/// Base class for all MailerSend services providing common functionality
/// </summary>
public abstract class ServiceBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IOptions<MailerSendOptions> _options;

    /// <summary>
    /// Initializes a new instance of the service with required dependencies
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    /// <exception cref="ArgumentNullException">Thrown when httpClientFactory or options is null</exception>
    protected ServiceBase(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
    {
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Creates a new MailerSendHttpClient instance for making API requests
    /// </summary>
    /// <returns>A configured MailerSendHttpClient instance</returns>
    protected MailerSendHttpClient CreateHttpClient()
    {
        var httpClient = _httpClientFactory.CreateClient("MailerSend");
        return new MailerSendHttpClient(httpClient, _options);
    }

    /// <summary>
    /// Builds a query string from a list of query parameters
    /// </summary>
    /// <param name="queryParams">List of query parameters in key=value format</param>
    /// <returns>A concatenated query string</returns>
    protected static string BuildQueryString(List<string> queryParams)
    {
        return string.Join("&", queryParams);
    }

    /// <summary>
    /// Determines if the HTTP status code indicates a successful operation
    /// </summary>
    /// <param name="statusCode">HTTP status code to check</param>
    /// <returns>True if the status code indicates success; otherwise, false</returns>
    protected static bool IsSuccessStatusCode(int statusCode)
    {
        return statusCode is 200 or 201 or 202 or 204;
    }
}
