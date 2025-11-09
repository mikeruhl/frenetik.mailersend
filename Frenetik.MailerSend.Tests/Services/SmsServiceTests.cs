using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models;
using Frenetik.MailerSend.Models.Sms;
using Frenetik.MailerSend.Models.Util;
using Frenetik.MailerSend.Services.Sms;
using Frenetik.MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace Frenetik.MailerSend.Tests.Services;

public class SmsServiceTests
{
    private SmsService CreateService(HttpMessageHandler handler)
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

        return new SmsService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetMessagesAsync_ReturnsMessagesList()
    {
        var expectedResponse = new SmsMessagesList
        {
            Messages = new[]
            {
                new SmsMessage
                {
                    Id = "msg1",
                    From = "+1234567890",
                    To = new[] { "+0987654321" },
                    Sms = new SmsInfo { Status = "sent" }
                }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var smsService = CreateService(handler);

        var result = await smsService.GetMessagesAsync();

        Assert.NotNull(result);
        Assert.Single(result.Messages);
        Assert.Equal("msg1", result.Messages[0].Id);
        Assert.Equal(1, result.CurrentPage);
    }

    [Fact]
    public async Task GetMessageAsync_ReturnsSingleMessage()
    {
        var expectedMessage = new SmsMessage
        {
            Id = "msg1",
            From = "+1234567890",
            To = new[] { "+0987654321" },
            Sms = new SmsInfo { Status = "sent" }
        };

        var response = new SingleSmsMessageResponse { Message = expectedMessage };

        var handler = MockHttpMessageHandler.Create(response);
        var smsService = CreateService(handler);

        var result = await smsService.GetMessageAsync("msg1");

        Assert.NotNull(result);
        Assert.Equal("msg1", result.Id);
        Assert.Equal("+1234567890", result.From);
    }

    [Fact]
    public async Task SendSmsAsync_ReturnsMessageId()
    {
        var response = new MailerSendResponse
        {
            Headers = new Dictionary<string, IEnumerable<string>>
            {
                { "x-sms-message-id", new List<string> { "msg123" } }
            }
        };

        var handler = MockHttpMessageHandler.Create(response);
        var smsService = CreateService(handler);

        var result = await smsService.SendSmsAsync("+1234567890", new[] { "+0987654321" }, "Test message");

        Assert.Equal("msg123", result);
    }

    [Fact]
    public async Task GetPhoneNumbersAsync_ReturnsPhoneNumberList()
    {
        var expectedResponse = new PhoneNumberList
        {
            PhoneNumbers = new[]
            {
                new PhoneNumber { Id = "phone1", TelephoneNumber = "+1234567890", Paused = false }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var smsService = CreateService(handler);

        var result = await smsService.GetPhoneNumbersAsync();

        Assert.NotNull(result);
        Assert.Single(result.PhoneNumbers);
        Assert.Equal("phone1", result.PhoneNumbers[0].Id);
    }

    [Fact]
    public async Task UpdatePhoneNumberAsync_ReturnsUpdatedPhoneNumber()
    {
        var expectedPhone = new PhoneNumber { Id = "phone1", TelephoneNumber = "+1234567890", Paused = true };
        var response = new SinglePhoneNumberResponse { PhoneNumber = expectedPhone };

        var handler = MockHttpMessageHandler.Create(response);
        var smsService = CreateService(handler);

        var result = await smsService.UpdatePhoneNumberAsync("phone1", true);

        Assert.NotNull(result);
        Assert.Equal("phone1", result.Id);
        Assert.True(result.Paused);
    }

    [Fact]
    public async Task DeletePhoneNumberAsync_ReturnsTrue_WhenSuccessful()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(response);
        var smsService = CreateService(handler);

        var result = await smsService.DeletePhoneNumberAsync("phone1");

        Assert.True(result);
    }

    [Fact]
    public void GetMessages_SyncMethod_Works()
    {
        var expectedResponse = new SmsMessagesList
        {
            Messages = new[] { new SmsMessage { Id = "msg1" } },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var smsService = CreateService(handler);

        var result = smsService.GetMessages();

        Assert.NotNull(result);
        Assert.Single(result.Messages);
    }
}
