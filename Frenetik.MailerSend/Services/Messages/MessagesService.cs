using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models.Messages;
using Frenetik.MailerSend.Models.Util;
using Microsoft.Extensions.Options;

namespace Frenetik.MailerSend.Services.Messages;

/// <summary>
/// Service implementation for retrieving messages
/// </summary>
public class MessagesService : ServiceBase, IMessagesService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MessagesService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public MessagesService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets a paginated list of messages
    /// </summary>
    public async Task<MessagesList> GetMessagesAsync(
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default)
    {
        pagination ??= new PaginationParameters();
        var queryParams = new List<string>
        {
            $"page={pagination.Page}",
            $"limit={pagination.Limit}"
        };

        var endpoint = $"messages?{BuildQueryString(queryParams)}";

        var mailerSendHttpClient = CreateHttpClient();
        return await mailerSendHttpClient.GetRequestAsync<MessagesList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of messages (synchronous)
    /// </summary>
    public MessagesList GetMessages(PaginationParameters? pagination = null)
    {
        return GetMessagesAsync(pagination).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single message by ID
    /// </summary>
    public async Task<Message> GetMessageAsync(string messageId, CancellationToken cancellationToken = default)
    {
        var endpoint = $"messages/{messageId}";

        var mailerSendHttpClient = CreateHttpClient();
        var response = await mailerSendHttpClient.GetRequestAsync<SingleMessageResponse>(endpoint, cancellationToken);
        return response.Message;
    }

    /// <summary>
    /// Gets a single message by ID (synchronous)
    /// </summary>
    public Message GetMessage(string messageId)
    {
        return GetMessageAsync(messageId).GetAwaiter().GetResult();
    }
}
