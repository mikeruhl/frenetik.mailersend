using System.Text.Json;
using System.Text.Json.Serialization;
using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models.Sms;
using Frenetik.MailerSend.Models.Util;
using Microsoft.Extensions.Options;

namespace Frenetik.MailerSend.Services.Sms;

/// <summary>
/// Service implementation for managing SMS messages and phone numbers
/// </summary>
public class SmsService : ServiceBase, ISmsService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SmsService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public SmsService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets a paginated list of SMS messages
    /// </summary>
    public async Task<SmsMessagesList> GetMessagesAsync(
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default)
    {
        pagination ??= new PaginationParameters();
        var mailerSendHttpClient = CreateHttpClient();

        var queryParams = new List<string>
        {
            $"page={pagination.Page}",
            $"limit={pagination.Limit}"
        };

        var endpoint = $"sms-messages?{BuildQueryString(queryParams)}";
        return await mailerSendHttpClient.GetRequestAsync<SmsMessagesList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of SMS messages (synchronous)
    /// </summary>
    public SmsMessagesList GetMessages(PaginationParameters? pagination = null)
    {
        return GetMessagesAsync(pagination).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single SMS message by ID
    /// </summary>
    public async Task<SmsMessage> GetMessageAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"sms-messages/{messageId}";
        var response = await mailerSendHttpClient.GetRequestAsync<SingleSmsMessageResponse>(endpoint, cancellationToken);
        return response.Message;
    }

    /// <summary>
    /// Gets a single SMS message by ID (synchronous)
    /// </summary>
    public SmsMessage GetMessage(string messageId)
    {
        return GetMessageAsync(messageId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Sends an SMS message
    /// </summary>
    public async Task<string> SendSmsAsync(
        string from,
        string[] to,
        string text,
        List<SmsPersonalization>? personalization = null,
        CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var request = new SmsSendRequest
        {
            From = from,
            To = to.ToList(),
            Text = text,
            Personalization = personalization
        };

        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var endpoint = "sms";
        var response = await mailerSendHttpClient.PostRequestAsync<Models.MailerSendResponse>(
            endpoint,
            JsonSerializer.Serialize(request, options),
            cancellationToken);

        if (response.Headers != null && response.Headers.TryGetValue("x-sms-message-id", out var messageIds))
        {
            return messageIds.FirstOrDefault() ?? string.Empty;
        }

        return string.Empty;
    }

    /// <summary>
    /// Sends an SMS message (synchronous)
    /// </summary>
    public string SendSms(string from, string[] to, string text, List<SmsPersonalization>? personalization = null)
    {
        return SendSmsAsync(from, to, text, personalization).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a paginated list of phone numbers
    /// </summary>
    public async Task<PhoneNumberList> GetPhoneNumbersAsync(
        PaginationParameters? pagination = null,
        bool? paused = null,
        CancellationToken cancellationToken = default)
    {
        pagination ??= new PaginationParameters();
        var mailerSendHttpClient = CreateHttpClient();

        var queryParams = new List<string>
        {
            $"page={pagination.Page}",
            $"limit={pagination.Limit}"
        };

        if (paused.HasValue)
        {
            queryParams.Add($"paused={paused.Value.ToString().ToLower()}");
        }

        var endpoint = $"sms-numbers?{BuildQueryString(queryParams)}";
        return await mailerSendHttpClient.GetRequestAsync<PhoneNumberList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of phone numbers (synchronous)
    /// </summary>
    public PhoneNumberList GetPhoneNumbers(PaginationParameters? pagination = null, bool? paused = null)
    {
        return GetPhoneNumbersAsync(pagination, paused).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single phone number by ID
    /// </summary>
    public async Task<PhoneNumber> GetPhoneNumberAsync(string phoneNumberId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"sms-numbers/{phoneNumberId}";
        var response = await mailerSendHttpClient.GetRequestAsync<SinglePhoneNumberResponse>(endpoint, cancellationToken);
        return response.PhoneNumber;
    }

    /// <summary>
    /// Gets a single phone number by ID (synchronous)
    /// </summary>
    public PhoneNumber GetPhoneNumber(string phoneNumberId)
    {
        return GetPhoneNumberAsync(phoneNumberId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Updates a phone number's paused status
    /// </summary>
    public async Task<PhoneNumber> UpdatePhoneNumberAsync(string phoneNumberId, bool paused, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"/sms-numbers/{phoneNumberId}?paused={paused.ToString().ToLower()}";
        var response = await mailerSendHttpClient.PutRequestAsync<SinglePhoneNumberResponse>(endpoint, null, cancellationToken);
        return response.PhoneNumber;
    }

    /// <summary>
    /// Updates a phone number's paused status (synchronous)
    /// </summary>
    public PhoneNumber UpdatePhoneNumber(string phoneNumberId, bool paused)
    {
        return UpdatePhoneNumberAsync(phoneNumberId, paused).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Deletes a phone number
    /// </summary>
    public async Task<bool> DeletePhoneNumberAsync(string phoneNumberId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"sms-numbers/{phoneNumberId}";
        var response = await mailerSendHttpClient.DeleteRequestAsync<Models.MailerSendResponse>(endpoint, cancellationToken);

        return IsSuccessStatusCode(response.ResponseStatusCode);
    }

    /// <summary>
    /// Deletes a phone number (synchronous)
    /// </summary>
    public bool DeletePhoneNumber(string phoneNumberId)
    {
        return DeletePhoneNumberAsync(phoneNumberId).GetAwaiter().GetResult();
    }
}
