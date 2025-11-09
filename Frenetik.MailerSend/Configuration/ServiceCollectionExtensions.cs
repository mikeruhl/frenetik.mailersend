using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Frenetik.MailerSend.Configuration;

/// <summary>
/// Extension methods for configuring MailerSend services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds MailerSend services to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="configure">Action to configure MailerSend options</param>
    /// <returns>The HTTP client builder for additional configuration (e.g., Polly policies)</returns>
    public static IHttpClientBuilder AddMailerSend(this IServiceCollection services, Action<MailerSendOptions> configure)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        if (configure == null)
            throw new ArgumentNullException(nameof(configure));

        services.Configure(configure);

        return AddMailerSendCore(services);
    }

    /// <summary>
    /// Adds MailerSend services to the service collection using pre-configured IOptions&lt;MailerSendOptions&gt;
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The HTTP client builder for additional configuration (e.g., Polly policies)</returns>
    /// <remarks>
    /// This overload assumes that MailerSendOptions have already been configured via services.Configure&lt;MailerSendOptions&gt;()
    /// or through IConfiguration binding (e.g., from appsettings.json).
    /// </remarks>
    public static IHttpClientBuilder AddMailerSend(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        return AddMailerSendCore(services);
    }

    /// <summary>
    /// Adds MailerSend services to the service collection with an API token
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <param name="apiToken">The MailerSend API token</param>
    /// <returns>The HTTP client builder for additional configuration (e.g., Polly policies)</returns>
    public static IHttpClientBuilder AddMailerSend(this IServiceCollection services, string apiToken)
    {
        return services.AddMailerSend(options => options.ApiToken = apiToken);
    }

    /// <summary>
    /// Core implementation for adding MailerSend services
    /// </summary>
    private static IHttpClientBuilder AddMailerSendCore(IServiceCollection services)
    {
        // Register the IHttpClient with named client "MailerSend"
        var httpClientBuilder = services.AddHttpClient("MailerSend", (serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<MailerSendOptions>>().Value;

            if (string.IsNullOrEmpty(options.ApiToken))
            {
                var envToken = Environment.GetEnvironmentVariable("MAILERSEND_API_TOKEN");
                if (!string.IsNullOrEmpty(envToken))
                {
                    options.ApiToken = envToken;
                }
            }

            httpClient.BaseAddress = new Uri(options.BaseUrl);
            httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutSeconds);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.ApiToken}");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        // Register MailerSendClient as scoped
        services.AddScoped<MailerSendClient>(serviceProvider =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<MailerSendOptions>>();
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            return new MailerSendClient(httpClientFactory, options);
        });

        return httpClientBuilder;
    }
}
