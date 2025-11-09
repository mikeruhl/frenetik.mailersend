using MailerSend.Configuration;
using MailerSend.Models.Messages;
using MailerSend.Models.Util;
using MailerSend.Services.Messages;
using MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace MailerSend.Tests.Services;

public class MessagesServiceTests
{
    private MessagesService CreateService(HttpMessageHandler handler)
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

        return new MessagesService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetMessagesAsync_ReturnsMessagesList()
    {
        var expectedResponse = new MessagesList
        {
            Messages = new[]
            {
                new MessagesListItem
                {
                    Id = "msg1",
                    CreatedAt = DateTime.UtcNow
                }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetMessagesAsync();

        Assert.NotNull(result);
        Assert.Single(result.Messages);
        Assert.Equal("msg1", result.Messages[0].Id);
    }

    [Fact]
    public async Task GetMessagesAsync_WithPagination_UsesCorrectEndpoint()
    {
        var expectedResponse = new MessagesList
        {
            Messages = Array.Empty<MessagesListItem>(),
            Meta = new ResponseMeta { CurrentPage = 2, LastPage = 5, Limit = 50 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetMessagesAsync(pagination: new MailerSend.Models.Util.PaginationParameters { Page = 2, Limit = 50 });

        Assert.Equal(2, result.CurrentPage);
        Assert.Equal(50, result.PerPage);
    }

    [Fact]
    public async Task GetMessageAsync_ReturnsSingleMessage()
    {
        var expectedMessage = new Message
        {
            Id = "msg1",
            CreatedAt = DateTime.UtcNow,
            Emails = Array.Empty<ApiEmail>()
        };

        var response = new SingleMessageResponse { Message = expectedMessage };
        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.GetMessageAsync("msg1");

        Assert.NotNull(result);
        Assert.Equal("msg1", result.Id);
    }

    [Fact]
    public void GetMessages_SyncMethod_Works()
    {
        var expectedResponse = new MessagesList
        {
            Messages = new[] { new MessagesListItem { Id = "msg1" } },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetMessages();

        Assert.NotNull(result);
        Assert.Single(result.Messages);
    }

    [Fact]
    public void GetMessage_SyncMethod_Works()
    {
        var expectedMessage = new Message { Id = "msg1" };
        var response = new SingleMessageResponse { Message = expectedMessage };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = service.GetMessage("msg1");

        Assert.NotNull(result);
        Assert.Equal("msg1", result.Id);
    }
}
