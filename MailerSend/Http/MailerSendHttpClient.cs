using System.Net;
using System.Text;
using System.Text.Json;
using MailerSend.Configuration;
using MailerSend.Exceptions;
using MailerSend.Models;
using Microsoft.Extensions.Options;

namespace MailerSend.Http;

/// <summary>
/// Handles all HTTP requests to the MailerSend API
/// </summary>
public class MailerSendHttpClient : IMailerSendHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<MailerSendOptions> _options;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Initializes a new instance of the MailerSendHttpClient class
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for requests</param>
    /// <param name="options">The MailerSend configuration options</param>
    public MailerSendHttpClient(HttpClient httpClient, IOptions<MailerSendOptions> options)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _options = options ?? throw new ArgumentNullException(nameof(options));

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }

    /// <summary>
    /// Performs a GET request to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    public async Task<T> GetRequestAsync<T>(string endpoint, CancellationToken cancellationToken = default) where T : MailerSendResponse, new()
    {
        var response = await _httpClient.GetAsync(endpoint, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a GET request to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <returns>The deserialized response</returns>
    public T GetRequest<T>(string endpoint) where T : MailerSendResponse, new()
    {
        return GetRequestAsync<T>(endpoint).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Performs a POST request to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    public async Task<T> PostRequestAsync<T>(string endpoint, object requestBody, CancellationToken cancellationToken = default) where T : MailerSendResponse, new()
    {
        var json = JsonSerializer.Serialize(requestBody, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a POST request to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object</param>
    /// <returns>The deserialized response</returns>
    public T PostRequest<T>(string endpoint, object requestBody) where T : MailerSendResponse, new()
    {
        return PostRequestAsync<T>(endpoint, requestBody).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Performs a PUT request to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object (optional)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    public async Task<T> PutRequestAsync<T>(string endpoint, object? requestBody = null, CancellationToken cancellationToken = default) where T : MailerSendResponse, new()
    {
        var json = requestBody != null ? JsonSerializer.Serialize(requestBody, _jsonOptions) : string.Empty;
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(endpoint, content, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a PUT request to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object (optional)</param>
    /// <returns>The deserialized response</returns>
    public T PutRequest<T>(string endpoint, object? requestBody = null) where T : MailerSendResponse, new()
    {
        return PutRequestAsync<T>(endpoint, requestBody).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Performs a DELETE request to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    public async Task<T> DeleteRequestAsync<T>(string endpoint, CancellationToken cancellationToken = default) where T : MailerSendResponse, new()
    {
        var response = await _httpClient.DeleteAsync(endpoint, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a DELETE request to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <returns>The deserialized response</returns>
    public T DeleteRequest<T>(string endpoint) where T : MailerSendResponse, new()
    {
        return DeleteRequestAsync<T>(endpoint).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Performs a DELETE request with a body to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    public async Task<T> DeleteRequestAsync<T>(string endpoint, object requestBody, CancellationToken cancellationToken = default) where T : MailerSendResponse, new()
    {
        var json = JsonSerializer.Serialize(requestBody, _jsonOptions);
        var request = new HttpRequestMessage(HttpMethod.Delete, endpoint)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };
        var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        return await HandleResponseAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a DELETE request with a body to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object</param>
    /// <returns>The deserialized response</returns>
    public T DeleteRequest<T>(string endpoint, object requestBody) where T : MailerSendResponse, new()
    {
        return DeleteRequestAsync<T>(endpoint, requestBody).GetAwaiter().GetResult();
    }

    private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken) where T : MailerSendResponse, new()
    {
#if NETSTANDARD2_0
        var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
#else
        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
#endif

        var successCodes = new[] { HttpStatusCode.OK, HttpStatusCode.Created, HttpStatusCode.Accepted, HttpStatusCode.NoContent };

        if (!successCodes.Contains(response.StatusCode))
        {
            var exception = new MailerSendException("API request failed");

            try
            {
                var error = JsonSerializer.Deserialize<JsonResponseError>(responseBody, _jsonOptions);
                if (error != null)
                {
                    exception = new MailerSendException(error.Message);
                    exception.Errors = error.Errors;
                }
            }
            catch
            {
                exception = new MailerSendException("Error parsing API response");
            }

            exception.ResponseBody = responseBody;
            exception.Code = (int)response.StatusCode;

            throw exception;
        }

        T result;
        if (string.IsNullOrWhiteSpace(responseBody) || responseBody == "[]")
        {
            result = new T();
        }
        else
        {
            result = JsonSerializer.Deserialize<T>(responseBody, _jsonOptions) ?? new T();
        }

        result.ResponseStatusCode = (int)response.StatusCode;
        result.Headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);

        if (response.Headers.TryGetValues("x-message-id", out var messageIdValues))
        {
            result.MessageId = messageIdValues.FirstOrDefault();
        }

        if (response.Headers.TryGetValues("x-ratelimit-limit", out var rateLimitValues) &&
            int.TryParse(rateLimitValues.FirstOrDefault(), out var rateLimit))
        {
            result.RateLimit = rateLimit;
        }

        if (response.Headers.TryGetValues("x-ratelimit-remaining", out var rateLimitRemainingValues) &&
            int.TryParse(rateLimitRemainingValues.FirstOrDefault(), out var rateLimitRemaining))
        {
            result.RateLimitRemaining = rateLimitRemaining;
        }

        return result;
    }
}
