using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models;
using Frenetik.MailerSend.Models.Templates;
using Frenetik.MailerSend.Models.Util;
using Microsoft.Extensions.Options;

namespace Frenetik.MailerSend.Services.Templates;

/// <summary>
/// Service implementation for managing templates
/// </summary>
public class TemplatesService : ServiceBase, ITemplatesService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TemplatesService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public TemplatesService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets a paginated list of templates
    /// </summary>
    public async Task<TemplatesList> GetTemplatesAsync(
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

        var endpoint = $"templates?{BuildQueryString(queryParams)}";
        return await mailerSendHttpClient.GetRequestAsync<TemplatesList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of templates (synchronous)
    /// </summary>
    public TemplatesList GetTemplates(PaginationParameters? pagination = null, string? domainId = null)
    {
        return GetTemplatesAsync(pagination, domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single template by ID
    /// </summary>
    public async Task<Template> GetTemplateAsync(string templateId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"templates/{templateId}";
        var response = await mailerSendHttpClient.GetRequestAsync<SingleTemplateResponse>(endpoint, cancellationToken);
        return response.Template;
    }

    /// <summary>
    /// Gets a single template by ID (synchronous)
    /// </summary>
    public Template GetTemplate(string templateId)
    {
        return GetTemplateAsync(templateId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Deletes a template
    /// </summary>
    public async Task<bool> DeleteTemplateAsync(string templateId, CancellationToken cancellationToken = default)
    {
        try
        {
            var mailerSendHttpClient = CreateHttpClient();

            var endpoint = $"templates/{templateId}";
            var response = await mailerSendHttpClient.DeleteRequestAsync<MailerSendResponse>(endpoint, cancellationToken);
            return IsSuccessStatusCode(response.ResponseStatusCode);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes a template (synchronous)
    /// </summary>
    public bool DeleteTemplate(string templateId)
    {
        return DeleteTemplateAsync(templateId).GetAwaiter().GetResult();
    }
}
