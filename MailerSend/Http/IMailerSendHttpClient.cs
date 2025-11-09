using MailerSend.Models;

namespace MailerSend.Http;

/// <summary>
/// Interface for HTTP client operations with the MailerSend API
/// </summary>
public interface IMailerSendHttpClient
{
    /// <summary>
    /// Performs a GET request to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> GetRequestAsync<T>(string endpoint, CancellationToken cancellationToken = default) where T : MailerSendResponse, new();

    /// <summary>
    /// Performs a GET request to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <returns>The deserialized response</returns>
    T GetRequest<T>(string endpoint) where T : MailerSendResponse, new();

    /// <summary>
    /// Performs a POST request to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> PostRequestAsync<T>(string endpoint, object requestBody, CancellationToken cancellationToken = default) where T : MailerSendResponse, new();

    /// <summary>
    /// Performs a POST request to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object</param>
    /// <returns>The deserialized response</returns>
    T PostRequest<T>(string endpoint, object requestBody) where T : MailerSendResponse, new();

    /// <summary>
    /// Performs a PUT request to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object (optional)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> PutRequestAsync<T>(string endpoint, object? requestBody = null, CancellationToken cancellationToken = default) where T : MailerSendResponse, new();

    /// <summary>
    /// Performs a PUT request to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object (optional)</param>
    /// <returns>The deserialized response</returns>
    T PutRequest<T>(string endpoint, object? requestBody = null) where T : MailerSendResponse, new();

    /// <summary>
    /// Performs a DELETE request to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> DeleteRequestAsync<T>(string endpoint, CancellationToken cancellationToken = default) where T : MailerSendResponse, new();

    /// <summary>
    /// Performs a DELETE request to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <returns>The deserialized response</returns>
    T DeleteRequest<T>(string endpoint) where T : MailerSendResponse, new();

    /// <summary>
    /// Performs a DELETE request with a body to the MailerSend API
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The deserialized response</returns>
    Task<T> DeleteRequestAsync<T>(string endpoint, object requestBody, CancellationToken cancellationToken = default) where T : MailerSendResponse, new();

    /// <summary>
    /// Performs a DELETE request with a body to the MailerSend API (synchronous)
    /// </summary>
    /// <typeparam name="T">The response type</typeparam>
    /// <param name="endpoint">The API endpoint</param>
    /// <param name="requestBody">The request body object</param>
    /// <returns>The deserialized response</returns>
    T DeleteRequest<T>(string endpoint, object requestBody) where T : MailerSendResponse, new();
}
