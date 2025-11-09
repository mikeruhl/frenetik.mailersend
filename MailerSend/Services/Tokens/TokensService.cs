using System.Text.Json;
using System.Text.Json.Serialization;
using MailerSend.Configuration;
using MailerSend.Models;
using MailerSend.Models.Tokens;
using MailerSend.Models.Util;
using Microsoft.Extensions.Options;

namespace MailerSend.Services.Tokens;

/// <summary>
/// Service implementation for managing API tokens
/// </summary>
public class TokensService : ServiceBase, ITokensService
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TokensService"/> class
    /// </summary>
    /// <param name="httpClientFactory">Factory for creating HTTP clients</param>
    /// <param name="options">MailerSend configuration options</param>
    public TokensService(IHttpClientFactory httpClientFactory, IOptions<MailerSendOptions> options)
        : base(httpClientFactory, options)
    {
    }

    /// <summary>
    /// Gets a paginated list of tokens
    /// </summary>
    public async Task<TokensList> GetTokensAsync(
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

        var endpoint = $"token?{BuildQueryString(queryParams)}";
        return await mailerSendHttpClient.GetRequestAsync<TokensList>(endpoint, cancellationToken);
    }

    /// <summary>
    /// Gets a paginated list of tokens (synchronous)
    /// </summary>
    public TokensList GetTokens(PaginationParameters? pagination = null)
    {
        return GetTokensAsync(pagination).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Gets a single token by ID
    /// </summary>
    public async Task<Token> GetTokenAsync(string tokenId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"token/{tokenId}";
        var response = await mailerSendHttpClient.GetRequestAsync<SingleTokenResponse>(endpoint, cancellationToken);
        return response.Token;
    }

    /// <summary>
    /// Gets a single token by ID (synchronous)
    /// </summary>
    public Token GetToken(string tokenId)
    {
        return GetTokenAsync(tokenId).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Creates a new API token
    /// </summary>
    public async Task<TokenAdd> CreateTokenAsync(
        string name,
        string domainId,
        string[] scopes,
        CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var request = new TokenCreateRequest
        {
            Name = name,
            DomainId = domainId,
            Scopes = scopes
        };

        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var endpoint = "token";
        var response = await mailerSendHttpClient.PostRequestAsync<TokenAddResponse>(
            endpoint,
            JsonSerializer.Serialize(request, options),
            cancellationToken);

        return response.Token;
    }

    /// <summary>
    /// Creates a new API token (synchronous)
    /// </summary>
    public TokenAdd CreateToken(string name, string domainId, string[] scopes)
    {
        return CreateTokenAsync(name, domainId, scopes).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Updates a token's status
    /// </summary>
    public async Task<Token> UpdateTokenStatusAsync(
        string tokenId,
        string status,
        CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var request = new TokenUpdateRequest
        {
            Status = status
        };

        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var endpoint = $"token/{tokenId}/settings";
        var response = await mailerSendHttpClient.PutRequestAsync<SingleTokenResponse>(
            endpoint,
            JsonSerializer.Serialize(request, options),
            cancellationToken);

        return response.Token;
    }

    /// <summary>
    /// Updates a token's status (synchronous)
    /// </summary>
    public Token UpdateTokenStatus(string tokenId, string status)
    {
        return UpdateTokenStatusAsync(tokenId, status).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Updates a token's name
    /// </summary>
    public async Task<Token> UpdateTokenNameAsync(
        string tokenId,
        string name,
        CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var request = new TokenUpdateRequest
        {
            Name = name
        };

        var options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        var endpoint = $"token/{tokenId}";
        var response = await mailerSendHttpClient.PutRequestAsync<SingleTokenResponse>(
            endpoint,
            JsonSerializer.Serialize(request, options),
            cancellationToken);

        return response.Token;
    }

    /// <summary>
    /// Updates a token's name (synchronous)
    /// </summary>
    public Token UpdateTokenName(string tokenId, string name)
    {
        return UpdateTokenNameAsync(tokenId, name).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Deletes a token
    /// </summary>
    public async Task<bool> DeleteTokenAsync(string tokenId, CancellationToken cancellationToken = default)
    {
        var mailerSendHttpClient = CreateHttpClient();

        var endpoint = $"token/{tokenId}";
        var response = await mailerSendHttpClient.DeleteRequestAsync<MailerSendResponse>(endpoint, cancellationToken);
        return IsSuccessStatusCode(response.ResponseStatusCode);
    }

    /// <summary>
    /// Deletes a token (synchronous)
    /// </summary>
    public bool DeleteToken(string tokenId)
    {
        return DeleteTokenAsync(tokenId).GetAwaiter().GetResult();
    }
}
