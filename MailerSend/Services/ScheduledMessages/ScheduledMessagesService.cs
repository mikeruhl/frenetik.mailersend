using MailerSend.Configuration;
using MailerSend.Models;
using MailerSend.Models.ScheduledMessages;
using MailerSend.Models.Util;
using Microsoft.Extensions.Options;

namespace MailerSend.Services.ScheduledMessages;

/// <summary>
/// Service implementation for managing scheduled messages
/// </summary>
public class ScheduledMessagesService : ServiceBase, IScheduledMessagesService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ScheduledMessagesService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public ScheduledMessagesService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets a paginated list of scheduled messages
    /// </summary>
    public async Task<ScheduledMessagesList> GetScheduledMessagesAsync(
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

        var endpoint = $"message-schedules?{BuildQueryString(queryParams)}";
        return await mailerSendHttpClient.GetRequestAsync<ScheduledMessagesList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of scheduled messages (synchronous)
    /// </summary>
    public ScheduledMessagesList GetScheduledMessages(PaginationParameters? pagination = null, string? domainId = null)
    {
        return GetScheduledMessagesAsync(pagination, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single scheduled message by ID
    /// </summary>
    public async Task<ScheduledMessage> GetScheduledMessageAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"message-schedules/{messageId}";
        var response = await mailerSendHttpClient.GetRequestAsync<SingleScheduledMessageResponse>(endpoint, cancellationToken);
        return response.Message;
    }

    /// <summary>
    /// Gets a single scheduled message by ID (synchronous)
    /// </summary>
    public ScheduledMessage GetScheduledMessage(string messageId)
    {
        return GetScheduledMessageAsync(messageId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Deletes a scheduled message
    /// </summary>
    public async Task<bool> DeleteScheduledMessageAsync(string messageId, CancellationToken cancellationToken = default)
    {
        try
        {
            var mailerSendHttpClient = CreateHttpClient();

            var endpoint = $"message-schedules/{messageId}";
            var response = await mailerSendHttpClient.DeleteRequestAsync<MailerSendResponse>(endpoint, cancellationToken);
            return IsSuccessStatusCode(response.ResponseStatusCode);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes a scheduled message (synchronous)
    /// </summary>
    public bool DeleteScheduledMessage(string messageId)
    {
        return DeleteScheduledMessageAsync(messageId).GetAwaiter().GetResult();
    }
}
