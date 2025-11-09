using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MailerSend.Tests.Helpers;

/// <summary>
/// Mock HTTP message handler for testing HTTP requests
/// </summary>
public class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, HttpResponseMessage> _sendFunc;

    public MockHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> sendFunc)
    {
        _sendFunc = sendFunc ?? throw new ArgumentNullException(nameof(sendFunc));
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_sendFunc(request));
    }

    public static MockHttpMessageHandler Create<T>(T responseObject, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new MockHttpMessageHandler(request =>
        {
            var responseType = typeof(T);

            // Check if responseObject has a ResponseStatusCode property
            var statusCodeProperty = responseType.GetProperty("ResponseStatusCode");
            if (statusCodeProperty != null && statusCodeProperty.PropertyType == typeof(int))
            {
                var responseStatusCode = (int?)statusCodeProperty.GetValue(responseObject);
                if (responseStatusCode.HasValue && responseStatusCode.Value >= 100)
                {
                    statusCode = (HttpStatusCode)responseStatusCode.Value;
                }
            }

            // Serialize with options that respect JsonPropertyName attributes
            var jsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true
            };

            // Serialize object, respecting JsonPropertyName attributes on properties
            var json = JsonSerializer.Serialize(responseObject, responseType, jsonOptions);

            // Parse and rebuild, excluding base MailerSendResponse properties
            var jsonDoc = JsonDocument.Parse(json);
            var root = jsonDoc.RootElement;

            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream))
            {
                writer.WriteStartObject();
                foreach (var property in root.EnumerateObject())
                {
                    // Exclude MailerSendResponse base class metadata properties
                    // These are set by SDK after deserialization, not part of API response
                    if (property.Name is not ("ResponseStatusCode" or "MessageId" or "RateLimit" or "RateLimitRemaining" or "Headers"))
                    {
                        property.WriteTo(writer);
                    }
                }
                writer.WriteEndObject();
            }

            var finalJson = Encoding.UTF8.GetString(stream.ToArray());
            var httpResponse = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(finalJson, Encoding.UTF8, "application/json")
            };

            // Add MessageId as x-message-id header if present
            var messageIdProperty = responseType.GetProperty("MessageId");
            if (messageIdProperty != null && messageIdProperty.PropertyType == typeof(string))
            {
                var messageId = (string?)messageIdProperty.GetValue(responseObject);
                if (!string.IsNullOrEmpty(messageId))
                {
                    httpResponse.Headers.TryAddWithoutValidation("x-message-id", messageId);
                }
            }

            // Add RateLimit as x-ratelimit-limit header if present
            var rateLimitProperty = responseType.GetProperty("RateLimit");
            if (rateLimitProperty != null && rateLimitProperty.PropertyType == typeof(int))
            {
                var rateLimit = (int?)rateLimitProperty.GetValue(responseObject);
                if (rateLimit.HasValue && rateLimit.Value > 0)
                {
                    httpResponse.Headers.TryAddWithoutValidation("x-ratelimit-limit", rateLimit.Value.ToString());
                }
            }

            // Add RateLimitRemaining as x-ratelimit-remaining header if present
            var rateLimitRemainingProperty = responseType.GetProperty("RateLimitRemaining");
            if (rateLimitRemainingProperty != null && rateLimitRemainingProperty.PropertyType == typeof(int))
            {
                var rateLimitRemaining = (int?)rateLimitRemainingProperty.GetValue(responseObject);
                if (rateLimitRemaining.HasValue && rateLimitRemaining.Value > 0)
                {
                    httpResponse.Headers.TryAddWithoutValidation("x-ratelimit-remaining", rateLimitRemaining.Value.ToString());
                }
            }

            // Add headers from the response object to the HTTP response
            var headersProperty = responseType.GetProperty("Headers");
            if (headersProperty != null && headersProperty.PropertyType == typeof(Dictionary<string, IEnumerable<string>>))
            {
                var headers = (Dictionary<string, IEnumerable<string>>?)headersProperty.GetValue(responseObject);
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        httpResponse.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
            }

            return httpResponse;
        });
    }

    public static MockHttpMessageHandler CreateForEndpoint<T>(string endpoint, T responseObject, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new MockHttpMessageHandler(request =>
        {
            if (request.RequestUri?.PathAndQuery.Contains(endpoint) == true)
            {
                var json = JsonSerializer.Serialize(responseObject);
                return new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            }

            return new HttpResponseMessage(HttpStatusCode.NotFound);
        });
    }
}
