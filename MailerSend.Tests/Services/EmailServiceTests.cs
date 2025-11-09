using MailerSend.Configuration;
using MailerSend.Models;
using MailerSend.Models.Email;
using MailerSend.Services.Email;
using MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace MailerSend.Tests.Services;

public class EmailServiceTests
{
    private EmailService CreateService(HttpMessageHandler handler)
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

        return new EmailService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public void CreateEmail_WithDefaultFrom_SetsFromAddress()
    {
        var handler = MockHttpMessageHandler.Create(new object());
        var emailService = CreateService(handler);

        var defaultFrom = new Recipient { Email = "default@example.com", Name = "Default Sender" };
        emailService.DefaultFrom = defaultFrom;

        var email = emailService.CreateEmail();

        Assert.NotNull(email);
        Assert.Equal(defaultFrom, email.From);
    }

    [Fact]
    public void CreateEmail_WithoutDefaultFrom_CreatesEmptyEmail()
    {
        var handler = MockHttpMessageHandler.Create(new object());
        var emailService = CreateService(handler);

        var email = emailService.CreateEmail();

        Assert.NotNull(email);
        Assert.Null(email.From);
    }

    [Fact]
    public async Task SendAsync_WithValidEmail_ReturnsResponse()
    {
        var email = new EmailMessage
        {
            From = new Recipient { Email = "sender@example.com", Name = "Sender" },
            Recipients = new List<Recipient> { new Recipient { Email = "recipient@example.com", Name = "Recipient" } },
            Subject = "Test Subject",
            Text = "Test Body"
        };

        var expectedResponse = new MailerSendResponse
        {
            ResponseStatusCode = 200,
            MessageId = "msg_123"
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var emailService = CreateService(handler);

        var result = await emailService.SendAsync(email);

        Assert.NotNull(result);
        Assert.Equal(200, result.ResponseStatusCode);
        Assert.Equal("msg_123", result.MessageId);
    }

    [Fact]
    public async Task SendAsync_WithNullEmail_ThrowsArgumentNullException()
    {
        var handler = MockHttpMessageHandler.Create(new object());
        var emailService = CreateService(handler);

        await Assert.ThrowsAsync<ArgumentNullException>(() => emailService.SendAsync(null!));
    }

    [Fact]
    public void Send_SyncMethod_Works()
    {
        var email = new EmailMessage
        {
            From = new Recipient { Email = "sender@example.com" },
            Recipients = new List<Recipient> { new Recipient { Email = "recipient@example.com" } },
            Subject = "Test",
            Text = "Body"
        };

        var expectedResponse = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var emailService = CreateService(handler);

        var result = emailService.Send(email);

        Assert.NotNull(result);
        Assert.Equal(200, result.ResponseStatusCode);
    }

    [Fact]
    public async Task BulkSendAsync_WithValidEmails_ReturnsBulkSendId()
    {
        var emails = new[]
        {
            new EmailMessage
            {
                From = new Recipient { Email = "sender@example.com" },
                Recipients = new List<Recipient> { new Recipient { Email = "recipient1@example.com" } },
                Subject = "Test 1",
                Text = "Body 1"
            },
            new EmailMessage
            {
                From = new Recipient { Email = "sender@example.com" },
                Recipients = new List<Recipient> { new Recipient { Email = "recipient2@example.com" } },
                Subject = "Test 2",
                Text = "Body 2"
            }
        };

        var expectedResponse = new SendBulkResponse
        {
            BulkSendId = "bulk_123",
            ResponseStatusCode = 200
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var emailService = CreateService(handler);

        var result = await emailService.BulkSendAsync(emails);

        Assert.Equal("bulk_123", result);
    }

    [Fact]
    public async Task BulkSendAsync_WithNullArray_ThrowsArgumentException()
    {
        var handler = MockHttpMessageHandler.Create(new object());
        var emailService = CreateService(handler);

        await Assert.ThrowsAsync<ArgumentException>(() => emailService.BulkSendAsync(null!));
    }

    [Fact]
    public async Task BulkSendAsync_WithEmptyArray_ThrowsArgumentException()
    {
        var handler = MockHttpMessageHandler.Create(new object());
        var emailService = CreateService(handler);

        await Assert.ThrowsAsync<ArgumentException>(() => emailService.BulkSendAsync(Array.Empty<EmailMessage>()));
    }

    [Fact]
    public void BulkSend_SyncMethod_Works()
    {
        var emails = new[]
        {
            new EmailMessage
            {
                From = new Recipient { Email = "sender@example.com" },
                Recipients = new List<Recipient> { new Recipient { Email = "recipient@example.com" } },
                Subject = "Test",
                Text = "Body"
            }
        };

        var expectedResponse = new SendBulkResponse
        {
            BulkSendId = "bulk_456",
            ResponseStatusCode = 200
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var emailService = CreateService(handler);

        var result = emailService.BulkSend(emails);

        Assert.Equal("bulk_456", result);
    }

    [Fact]
    public async Task GetBulkSendStatusAsync_WithNullId_ThrowsArgumentException()
    {
        var handler = MockHttpMessageHandler.Create(new object());
        var emailService = CreateService(handler);

        await Assert.ThrowsAsync<ArgumentException>(() => emailService.GetBulkSendStatusAsync(null!));
    }

    [Fact]
    public async Task GetBulkSendStatusAsync_WithEmptyId_ThrowsArgumentException()
    {
        var handler = MockHttpMessageHandler.Create(new object());
        var emailService = CreateService(handler);

        await Assert.ThrowsAsync<ArgumentException>(() => emailService.GetBulkSendStatusAsync(string.Empty));
    }
}
