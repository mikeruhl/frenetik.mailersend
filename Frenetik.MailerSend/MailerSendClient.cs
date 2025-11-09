using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Services.Email;
using Frenetik.MailerSend.Services.Activities;
using Frenetik.MailerSend.Services.Analytics;
using Frenetik.MailerSend.Services.Domains;
using Frenetik.MailerSend.Services.Messages;
using Frenetik.MailerSend.Services.Tokens;
using Frenetik.MailerSend.Services.Recipients;
using Frenetik.MailerSend.Services.Webhooks;
using Frenetik.MailerSend.Services.Templates;
using Frenetik.MailerSend.Services.EmailVerification;
using Frenetik.MailerSend.Services.Sms;
using Frenetik.MailerSend.Services.InboundRoutes;
using Frenetik.MailerSend.Services.ScheduledMessages;
using Microsoft.Extensions.Options;

namespace Frenetik.MailerSend;

/// <summary>
/// Main client for interacting with the MailerSend API
/// </summary>
public class MailerSendClient
{
    /// <summary>
    /// Gets the Email service for sending and managing emails
    /// </summary>
    public IEmailService Email { get; }

    /// <summary>
    /// Gets the Activities service for retrieving activity logs
    /// </summary>
    public IActivitiesService Activities { get; }

    /// <summary>
    /// Gets the Analytics service for retrieving analytics data
    /// </summary>
    public IAnalyticsService Analytics { get; }

    /// <summary>
    /// Gets the Domains service for managing domains
    /// </summary>
    public IDomainsService Domains { get; }

    /// <summary>
    /// Gets the Messages service for retrieving message information
    /// </summary>
    public IMessagesService Messages { get; }

    /// <summary>
    /// Gets the Tokens service for managing API tokens
    /// </summary>
    public ITokensService Tokens { get; }

    /// <summary>
    /// Gets the Recipients service for managing recipients
    /// </summary>
    public IRecipientsService Recipients { get; }

    /// <summary>
    /// Gets the Webhooks service for managing webhooks
    /// </summary>
    public IWebhooksService Webhooks { get; }

    /// <summary>
    /// Gets the Templates service for managing templates
    /// </summary>
    public ITemplatesService Templates { get; }

    /// <summary>
    /// Gets the Email Verification service for managing email verification
    /// </summary>
    public IEmailVerificationService EmailVerification { get; }

    /// <summary>
    /// Gets the SMS service for sending and managing SMS messages
    /// </summary>
    public ISmsService Sms { get; }

    /// <summary>
    /// Gets the Inbound Routes service for managing inbound routes
    /// </summary>
    public IInboundRoutesService InboundRoutes { get; }

    /// <summary>
    /// Gets the Scheduled Messages service for managing scheduled messages
    /// </summary>
    public IScheduledMessagesService ScheduledMessages { get; }

    /// <summary>
    /// Initializes a new instance of the MailerSendClient class with IHttpClientFactory
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory for creating HTTP clients</param>
    /// <param name="options">The MailerSend configuration options</param>
    public MailerSendClient(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
    {
        if (httpClientFactory == null)
            throw new ArgumentNullException(nameof(httpClientFactory));

        if (options == null)
            throw new ArgumentNullException(nameof(options));

        var optionsValue = options.Value;
        if (string.IsNullOrEmpty(optionsValue.ApiToken))
        {
            var envToken = Environment.GetEnvironmentVariable("MAILERSEND_API_TOKEN");
            if (!string.IsNullOrEmpty(envToken))
            {
                optionsValue.ApiToken = envToken;
            }
            else
            {
                throw new ArgumentException("API token must be provided either in options or via MAILERSEND_API_TOKEN environment variable", nameof(options));
            }
        }

        Email = new EmailService(httpClientFactory, options);
        Activities = new ActivitiesService(httpClientFactory, options);
        Analytics = new AnalyticsService(httpClientFactory, options);
        Domains = new DomainsService(httpClientFactory, options);
        Messages = new MessagesService(httpClientFactory, options);
        Tokens = new TokensService(httpClientFactory, options);
        Recipients = new RecipientsService(httpClientFactory, options);
        Webhooks = new WebhooksService(httpClientFactory, options);
        Templates = new TemplatesService(httpClientFactory, options);
        EmailVerification = new EmailVerificationService(httpClientFactory, options);
        Sms = new SmsService(httpClientFactory, options);
        InboundRoutes = new InboundRoutesService(httpClientFactory, options);
        ScheduledMessages = new ScheduledMessagesService(httpClientFactory, options);
    }
}
