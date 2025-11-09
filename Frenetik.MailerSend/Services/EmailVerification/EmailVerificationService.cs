using System.Text.Json;
using System.Text.Json.Serialization;
using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models.EmailVerification;
using Frenetik.MailerSend.Models.Util;
using Microsoft.Extensions.Options;

namespace Frenetik.MailerSend.Services.EmailVerification;

/// <summary>
/// Service implementation for managing email verification
/// </summary>
public class EmailVerificationService : ServiceBase, IEmailVerificationService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailVerificationService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public EmailVerificationService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets a paginated list of email verification lists
    /// </summary>
    public async Task<VerificationListsList> GetVerificationListsAsync(
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

        var endpoint = $"email-verification?{BuildQueryString(queryParams)}";
        return await mailerSendHttpClient.GetRequestAsync<VerificationListsList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of email verification lists (synchronous)
    /// </summary>
    public VerificationListsList GetVerificationLists(PaginationParameters? pagination = null)
    {
        return GetVerificationListsAsync(pagination).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single email verification list by ID
    /// </summary>
    public async Task<VerificationList> GetVerificationListAsync(string listId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"email-verification/{listId}";
        var response = await mailerSendHttpClient.GetRequestAsync<SingleVerificationListResponse>(endpoint, cancellationToken);
        return response.List;
    }

    /// <summary>
    /// Gets a single email verification list by ID (synchronous)
    /// </summary>
    public VerificationList GetVerificationList(string listId)
    {
        return GetVerificationListAsync(listId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Creates a new email verification list
    /// </summary>
    public async Task<VerificationList> CreateVerificationListAsync(string name, string[] emails, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var request = new VerificationListCreateRequest
        {
            Name = name,
            Emails = emails
        };

        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var endpoint = "email-verification";
        var response = await mailerSendHttpClient.PostRequestAsync<SingleVerificationListResponse>(
            endpoint,
            JsonSerializer.Serialize(request, options),
            cancellationToken);

        return response.List;
    }

    /// <summary>
    /// Creates a new email verification list (synchronous)
    /// </summary>
    public VerificationList CreateVerificationList(string name, string[] emails)
    {
        return CreateVerificationListAsync(name, emails).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Verifies an email verification list
    /// </summary>
    public async Task<VerificationList> VerifyListAsync(string listId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"email-verification/{listId}/verify";
        var response = await mailerSendHttpClient.GetRequestAsync<SingleVerificationListResponse>(endpoint, cancellationToken);
        return response.List;
    }

    /// <summary>
    /// Verifies an email verification list (synchronous)
    /// </summary>
    public VerificationList VerifyList(string listId)
    {
        return VerifyListAsync(listId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets verification results for a list
    /// </summary>
    public async Task<VerificationResultsList> GetVerificationResultsAsync(string listId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"email-verification/{listId}/results";
        return await mailerSendHttpClient.GetRequestAsync<VerificationResultsList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets verification results for a list (synchronous)
    /// </summary>
    public VerificationResultsList GetVerificationResults(string listId)
    {
        return GetVerificationResultsAsync(listId).GetAwaiter().GetResult();
    }
}
