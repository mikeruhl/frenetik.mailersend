using Frenetik.MailerSend.Models.Templates;
using Frenetik.MailerSend.Models.Util;

namespace Frenetik.MailerSend.Services.Templates;

/// <summary>
/// Service for managing templates
/// </summary>
public interface ITemplatesService
{
    /// <summary>
    /// Gets a paginated list of templates
    /// </summary>
    Task<TemplatesList> GetTemplatesAsync(
        PaginationParameters? pagination = null,
        string? domainId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a paginated list of templates (synchronous)
    /// </summary>
    TemplatesList GetTemplates(PaginationParameters? pagination = null, string? domainId = null);

    /// <summary>
    /// Gets a single template by ID
    /// </summary>
    Task<Template> GetTemplateAsync(string templateId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a single template by ID (synchronous)
    /// </summary>
    Template GetTemplate(string templateId);

    /// <summary>
    /// Deletes a template
    /// </summary>
    Task<bool> DeleteTemplateAsync(string templateId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a template (synchronous)
    /// </summary>
    bool DeleteTemplate(string templateId);
}
