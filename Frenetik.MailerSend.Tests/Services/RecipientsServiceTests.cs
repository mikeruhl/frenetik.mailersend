using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models;
using Frenetik.MailerSend.Models.Recipients;
using Frenetik.MailerSend.Models.Util;
using Frenetik.MailerSend.Services.Recipients;
using Frenetik.MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace Frenetik.MailerSend.Tests.Services;

public class RecipientsServiceTests
{
    private RecipientsService CreateService(HttpMessageHandler handler)
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

        return new RecipientsService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetBlocklistAsync_ReturnsSuppressionsList()
    {
        var expectedResponse = new SuppressionsList
        {
            Suppressions = new[]
            {
                new Suppression { Id = "supp1", Recipient = "blocked@example.com" }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetBlocklistAsync();

        Assert.NotNull(result);
        Assert.Single(result.Suppressions);
        Assert.Equal("blocked@example.com", result.Suppressions[0].Recipient);
    }

    [Fact]
    public async Task AddToBlocklistAsync_ReturnsTrue_WhenSuccessful()
    {
        var recipients = new[] { "user@example.com" };
        var response = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.AddToBlocklistAsync(recipients);

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteFromBlocklistAsync_ReturnsTrue_WhenSuccessful()
    {
        var ids = new[] { "id1", "id2" };
        var response = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteFromBlocklistAsync(ids);

        Assert.True(result);
    }

    [Fact]
    public async Task GetHardBouncesAsync_ReturnsSuppressionsList()
    {
        var expectedResponse = new SuppressionsList
        {
            Suppressions = new[]
            {
                new Suppression { Id = "supp1", Recipient = "bounced@example.com" }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetHardBouncesAsync();

        Assert.NotNull(result);
        Assert.Single(result.Suppressions);
    }

    [Fact]
    public async Task GetSpamComplaintsAsync_ReturnsSuppressionsList()
    {
        var expectedResponse = new SuppressionsList
        {
            Suppressions = new[]
            {
                new Suppression { Id = "supp1", Recipient = "spam@example.com" }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetSpamComplaintsAsync();

        Assert.NotNull(result);
        Assert.Single(result.Suppressions);
    }

    [Fact]
    public async Task GetUnsubscribesAsync_ReturnsSuppressionsList()
    {
        var expectedResponse = new SuppressionsList
        {
            Suppressions = new[]
            {
                new Suppression { Id = "supp1", Recipient = "unsubscribed@example.com" }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetUnsubscribesAsync();

        Assert.NotNull(result);
        Assert.Single(result.Suppressions);
    }

    [Fact]
    public async Task GetBlocklistAsync_WithDomainId_IncludesParameter()
    {
        var expectedResponse = new SuppressionsList
        {
            Suppressions = Array.Empty<Suppression>(),
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetBlocklistAsync(domainId: "domain1");

        Assert.NotNull(result);
    }

    [Fact]
    public void GetBlocklist_SyncMethod_Works()
    {
        var expectedResponse = new SuppressionsList
        {
            Suppressions = new[] { new Suppression { Id = "supp1" } },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetBlocklist();

        Assert.NotNull(result);
        Assert.Single(result.Suppressions);
    }
}
