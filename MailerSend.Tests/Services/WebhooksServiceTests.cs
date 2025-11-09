using MailerSend.Configuration;
using MailerSend.Models;
using MailerSend.Models.Webhooks;
using MailerSend.Services.Webhooks;
using MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace MailerSend.Tests.Services;

public class WebhooksServiceTests
{
    private WebhooksService CreateService(HttpMessageHandler handler)
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

        return new WebhooksService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetWebhooksAsync_ReturnsWebhooksList()
    {
        var expectedResponse = new WebhooksList
        {
            Webhooks = new[]
            {
                new Webhook
                {
                    Id = "webhook1",
                    Name = "My Webhook",
                    Url = "https://example.com/webhook",
                    Enabled = true
                }
            }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetWebhooksAsync("domain1");

        Assert.NotNull(result);
        Assert.Single(result.Webhooks);
        Assert.Equal("webhook1", result.Webhooks[0].Id);
        Assert.Equal("My Webhook", result.Webhooks[0].Name);
    }

    [Fact]
    public async Task GetWebhookAsync_ReturnsSingleWebhook()
    {
        var expectedWebhook = new Webhook
        {
            Id = "webhook1",
            Name = "My Webhook",
            Url = "https://example.com/webhook",
            Enabled = true
        };

        var response = new SingleWebhookResponse { Webhook = expectedWebhook };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.GetWebhookAsync("webhook1");

        Assert.NotNull(result);
        Assert.Equal("webhook1", result.Id);
        Assert.Equal("My Webhook", result.Name);
    }

    [Fact]
    public async Task CreateWebhookAsync_ReturnsWebhook()
    {
        var events = new[] { "activity.sent", "activity.delivered" };
        var expectedWebhook = new Webhook
        {
            Id = "webhook1",
            Name = "New Webhook",
            Url = "https://example.com/webhook",
            Enabled = true
        };

        var response = new SingleWebhookResponse { Webhook = expectedWebhook };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.CreateWebhookAsync(
            "https://example.com/webhook",
            "New Webhook",
            events,
            "domain1",
            enabled: true);

        Assert.NotNull(result);
        Assert.Equal("webhook1", result.Id);
        Assert.Equal("New Webhook", result.Name);
    }

    [Fact]
    public async Task UpdateWebhookAsync_ReturnsUpdatedWebhook()
    {
        var expectedWebhook = new Webhook
        {
            Id = "webhook1",
            Name = "Updated Webhook",
            Url = "https://example.com/webhook",
            Enabled = false
        };

        var response = new SingleWebhookResponse { Webhook = expectedWebhook };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.UpdateWebhookAsync("webhook1", name: "Updated Webhook", enabled: false);

        Assert.NotNull(result);
        Assert.Equal("Updated Webhook", result.Name);
        Assert.False(result.Enabled);
    }

    [Fact]
    public async Task DeleteWebhookAsync_ReturnsTrue_WhenSuccessful()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteWebhookAsync("webhook1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteWebhookAsync_ReturnsTrue_ForStatus204()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 204 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteWebhookAsync("webhook1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteWebhookAsync_ReturnsFalse_WhenFailed()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 404 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteWebhookAsync("webhook1");

        Assert.False(result);
    }

    [Fact]
    public void GetWebhooks_SyncMethod_Works()
    {
        var expectedResponse = new WebhooksList
        {
            Webhooks = new[] { new Webhook { Id = "webhook1", Name = "Test Webhook" } }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetWebhooks("domain1");

        Assert.NotNull(result);
        Assert.Single(result.Webhooks);
    }

    [Fact]
    public void GetWebhook_SyncMethod_Works()
    {
        var expectedWebhook = new Webhook { Id = "webhook1", Name = "Test Webhook" };
        var response = new SingleWebhookResponse { Webhook = expectedWebhook };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = service.GetWebhook("webhook1");

        Assert.NotNull(result);
        Assert.Equal("webhook1", result.Id);
    }
}
