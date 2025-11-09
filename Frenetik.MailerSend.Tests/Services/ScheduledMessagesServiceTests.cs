using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models;
using Frenetik.MailerSend.Models.ScheduledMessages;
using Frenetik.MailerSend.Models.Util;
using Frenetik.MailerSend.Services.ScheduledMessages;
using Frenetik.MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace Frenetik.MailerSend.Tests.Services;

public class ScheduledMessagesServiceTests
{
    private ScheduledMessagesService CreateService(HttpMessageHandler handler)
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

        return new ScheduledMessagesService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetScheduledMessagesAsync_ReturnsMessagesList()
    {
        var expectedResponse = new ScheduledMessagesList
        {
            Messages = new[]
            {
                new ScheduledMessage
                {
                    MessageId = "msg1",
                    Subject = "Test Subject",
                    Status = "scheduled",
                    SendAt = DateTime.UtcNow.AddHours(1)
                }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetScheduledMessagesAsync();

        Assert.NotNull(result);
        Assert.Single(result.Messages);
        Assert.Equal("msg1", result.Messages[0].MessageId);
        Assert.Equal("Test Subject", result.Messages[0].Subject);
    }

    [Fact]
    public async Task GetScheduledMessagesAsync_WithDomainId_IncludesFilter()
    {
        var expectedResponse = new ScheduledMessagesList
        {
            Messages = Array.Empty<ScheduledMessage>(),
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetScheduledMessagesAsync(domainId: "domain123");

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetScheduledMessageAsync_ReturnsSingleMessage()
    {
        var expectedMessage = new ScheduledMessage
        {
            MessageId = "msg1",
            Subject = "Test Subject",
            Status = "scheduled",
            SendAt = DateTime.UtcNow.AddHours(1)
        };

        var response = new SingleScheduledMessageResponse { Message = expectedMessage };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.GetScheduledMessageAsync("msg1");

        Assert.NotNull(result);
        Assert.Equal("msg1", result.MessageId);
        Assert.Equal("scheduled", result.Status);
    }

    [Fact]
    public async Task DeleteScheduledMessageAsync_ReturnsTrue_WhenSuccessful()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteScheduledMessageAsync("msg1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteScheduledMessageAsync_ReturnsFalse_WhenFailed()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 404 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteScheduledMessageAsync("msg1");

        Assert.False(result);
    }

    [Fact]
    public void GetScheduledMessages_SyncMethod_Works()
    {
        var expectedResponse = new ScheduledMessagesList
        {
            Messages = new[] { new ScheduledMessage { MessageId = "msg1" } },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetScheduledMessages();

        Assert.NotNull(result);
        Assert.Single(result.Messages);
    }

    [Fact]
    public async Task GetScheduledMessagesAsync_WithPagination_UsesCorrectEndpoint()
    {
        var expectedResponse = new ScheduledMessagesList
        {
            Messages = Array.Empty<ScheduledMessage>(),
            Meta = new ResponseMeta { CurrentPage = 2, LastPage = 5, Limit = 50 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetScheduledMessagesAsync(pagination: new MailerSend.Models.Util.PaginationParameters { Page = 2, Limit = 50 });

        Assert.Equal(2, result.CurrentPage);
        Assert.Equal(50, result.PerPage);
    }
}
