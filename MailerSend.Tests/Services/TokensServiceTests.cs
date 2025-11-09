using MailerSend.Configuration;
using MailerSend.Models;
using MailerSend.Models.Tokens;
using MailerSend.Models.Util;
using MailerSend.Services.Tokens;
using MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace MailerSend.Tests.Services;

public class TokensServiceTests
{
    private TokensService CreateService(HttpMessageHandler handler)
    {
        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://api.mailersend.com/v1/")
        };
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer test_token");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        var mockFactory = new Mock<IHttpClientFactory>();
        mockFactory.Setup(x => x.CreateClient("MailerSend")).Returns(httpClient);

        var options = new MailerSendOptions
        {
            ApiToken = "test_token",
            BaseUrl = "https://api.mailersend.com/v1/"
        };

        return new TokensService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetTokensAsync_ReturnsTokensList()
    {
        var expectedResponse = new TokensList
        {
            Tokens = new[]
            {
                new Token
                {
                    Id = "token1",
                    Name = "API Token",
                    Status = "active"
                }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetTokensAsync();

        Assert.NotNull(result);
        Assert.Single(result.Tokens);
        Assert.Equal("token1", result.Tokens[0].Id);
    }

    [Fact]
    public async Task GetTokenAsync_ReturnsSingleToken()
    {
        var expectedToken = new Token
        {
            Id = "token1",
            Name = "API Token",
            Status = "active"
        };

        var response = new SingleTokenResponse { Token = expectedToken };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.GetTokenAsync("token1");

        Assert.NotNull(result);
        Assert.Equal("token1", result.Id);
        Assert.Equal("API Token", result.Name);
    }

    [Fact]
    public async Task CreateTokenAsync_ReturnsTokenAdd()
    {
        var scopes = new[] { "email_full", "domains_read" };
        var expectedToken = new TokenAdd
        {
            Id = "token1",
            Name = "New Token",
            AccessToken = "abc123"
        };

        var response = new TokenAddResponse { Token = expectedToken };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.CreateTokenAsync("New Token", "domain1", scopes);

        Assert.NotNull(result);
        Assert.Equal("token1", result.Id);
        Assert.Equal("New Token", result.Name);
    }

    [Fact]
    public async Task UpdateTokenStatusAsync_ReturnsUpdatedToken()
    {
        var expectedToken = new Token
        {
            Id = "token1",
            Name = "API Token",
            Status = "paused"
        };

        var response = new SingleTokenResponse { Token = expectedToken };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.UpdateTokenStatusAsync("token1", "paused");

        Assert.NotNull(result);
        Assert.Equal("paused", result.Status);
    }

    [Fact]
    public async Task UpdateTokenNameAsync_ReturnsUpdatedToken()
    {
        var expectedToken = new Token
        {
            Id = "token1",
            Name = "Updated Token Name",
            Status = "active"
        };

        var response = new SingleTokenResponse { Token = expectedToken };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.UpdateTokenNameAsync("token1", "Updated Token Name");

        Assert.NotNull(result);
        Assert.Equal("Updated Token Name", result.Name);
    }

    [Fact]
    public async Task DeleteTokenAsync_ReturnsTrue_WhenSuccessful()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteTokenAsync("token1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteTokenAsync_ReturnsTrue_ForStatus204()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 204 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteTokenAsync("token1");

        Assert.True(result);
    }

    [Fact]
    public void GetTokens_SyncMethod_Works()
    {
        var expectedResponse = new TokensList
        {
            Tokens = new[] { new Token { Id = "token1", Name = "Test Token" } },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetTokens();

        Assert.NotNull(result);
        Assert.Single(result.Tokens);
    }
}
