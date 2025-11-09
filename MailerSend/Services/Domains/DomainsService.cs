using MailerSend.Configuration;
using MailerSend.Models;
using MailerSend.Models.Domains;
using MailerSend.Models.Util;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace MailerSend.Services.Domains;

/// <summary>
/// Service implementation for managing domains
/// </summary>
public class DomainsService : ServiceBase, IDomainsService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainsService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public DomainsService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets a paginated list of domains
    /// </summary>
    public async Task<DomainsList> GetDomainsAsync(
        PaginationParameters? pagination = null,
        bool? verified = null,
        CancellationToken cancellationToken = default)
    {
        pagination ??= new PaginationParameters();
        var queryParams = new List<string>
        {
            $"page={pagination.Page}",
            $"limit={pagination.Limit}"
        };

        if (verified.HasValue)
        {
            queryParams.Add($"verified={verified.Value.ToString().ToLower()}");
        }

        var endpoint = $"domains?{BuildQueryString(queryParams)}";

        var mailerSendHttpClient = CreateHttpClient();
        return await mailerSendHttpClient.GetRequestAsync<DomainsList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of domains (synchronous)
    /// </summary>
    public DomainsList GetDomains(PaginationParameters? pagination = null, bool? verified = null)
    {
        return GetDomainsAsync(pagination, verified).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single domain by ID
    /// </summary>
    public async Task<Domain> GetDomainAsync(string domainId, CancellationToken cancellationToken = default)
    {
        var endpoint = $"domains/{domainId}";

        var mailerSendHttpClient = CreateHttpClient();
        var response = await mailerSendHttpClient.GetRequestAsync<SingleDomainResponse>(endpoint, cancellationToken);
        return response.Domain;
    }

    /// <summary>
    /// Gets a single domain by ID (synchronous)
    /// </summary>
    public Domain GetDomain(string domainId)
    {
        return GetDomainAsync(domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Adds a new domain
    /// </summary>
    public async Task<Domain> AddDomainAsync(
        string name,
        string? returnPathSubdomain = null,
        string? customTrackingSubdomain = null,
        string? inboundRoutingSubdomain = null,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new
        {
            name,
            return_path_subdomain = returnPathSubdomain,
            custom_tracking_subdomain = customTrackingSubdomain,
            inbound_routing_subdomain = inboundRoutingSubdomain
        };

        var json = JsonSerializer.Serialize(requestBody, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });

        var mailerSendHttpClient = CreateHttpClient();
        var response = await mailerSendHttpClient.PostRequestAsync<SingleDomainResponse>("domains", json, cancellationToken);
        return response.Domain;
    }

    /// <summary>
    /// Adds a new domain (synchronous)
    /// </summary>
    public Domain AddDomain(
        string name,
        string? returnPathSubdomain = null,
        string? customTrackingSubdomain = null,
        string? inboundRoutingSubdomain = null)
    {
        return AddDomainAsync(name, returnPathSubdomain, customTrackingSubdomain, inboundRoutingSubdomain)
            .GetAwaiter().GetResult();
    }

    /// <summary>
    /// Updates domain settings
    /// </summary>
    public async Task<Domain> UpdateDomainSettingsAsync(
        string domainId,
        DomainSettings settings,
        CancellationToken cancellationToken = default)
    {
        var endpoint = $"domains/{domainId}/settings";

        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        });

        var mailerSendHttpClient = CreateHttpClient();
        var response = await mailerSendHttpClient.PutRequestAsync<SingleDomainResponse>(endpoint, json, cancellationToken);
        return response.Domain;
    }

    /// <summary>
    /// Updates domain settings (synchronous)
    /// </summary>
    public Domain UpdateDomainSettings(string domainId, DomainSettings settings)
    {
        return UpdateDomainSettingsAsync(domainId, settings).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Deletes a domain
    /// </summary>
    public async Task<bool> DeleteDomainAsync(string domainId, CancellationToken cancellationToken = default)
    {
        try
        {
            var endpoint = $"domains/{domainId}";

            var mailerSendHttpClient = CreateHttpClient();
            var response = await mailerSendHttpClient.DeleteRequestAsync<MailerSendResponse>(endpoint, cancellationToken);

            return IsSuccessStatusCode(response.ResponseStatusCode);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Deletes a domain (synchronous)
    /// </summary>
    public bool DeleteDomain(string domainId)
    {
        return DeleteDomainAsync(domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets recipients for a domain
    /// </summary>
    public async Task<ApiRecipientsList> GetDomainRecipientsAsync(
        string domainId,
        PaginationParameters? pagination = null,
        CancellationToken cancellationToken = default)
    {
        pagination ??= new PaginationParameters();
        var queryParams = new List<string>
        {
            $"page={pagination.Page}",
            $"limit={pagination.Limit}"
        };

        var endpoint = $"domains/{domainId}/recipients?{BuildQueryString(queryParams)}";

        var mailerSendHttpClient = CreateHttpClient();
        return await mailerSendHttpClient.GetRequestAsync<ApiRecipientsList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets recipients for a domain (synchronous)
    /// </summary>
    public ApiRecipientsList GetDomainRecipients(string domainId, PaginationParameters? pagination = null)
    {
        return GetDomainRecipientsAsync(domainId, pagination).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets DNS records for a domain
    /// </summary>
    public async Task<DomainDnsRecords> GetDomainDnsRecordsAsync(
        string domainId,
        CancellationToken cancellationToken = default)
    {
        var endpoint = $"domains/{domainId}/dns-records";

        var mailerSendHttpClient = CreateHttpClient();
        var response = await mailerSendHttpClient.GetRequestAsync<DomainDnsRecordsResponse>(endpoint, cancellationToken);
        return response.Records;
    }

    /// <summary>
    /// Gets DNS records for a domain (synchronous)
    /// </summary>
    public DomainDnsRecords GetDomainDnsRecords(string domainId)
    {
        return GetDomainDnsRecordsAsync(domainId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Verifies a domain
    /// </summary>
    public async Task<DomainVerificationStatus> VerifyDomainAsync(
        string domainId,
        CancellationToken cancellationToken = default)
    {
        var endpoint = $"domains/{domainId}/verify";

        var mailerSendHttpClient = CreateHttpClient();
        return await mailerSendHttpClient.GetRequestAsync<DomainVerificationStatus>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Verifies a domain (synchronous)
    /// </summary>
    public DomainVerificationStatus VerifyDomain(string domainId)
    {
        return VerifyDomainAsync(domainId).GetAwaiter().GetResult();
    }
}
