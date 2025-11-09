using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Exceptions;
using Frenetik.MailerSend.Models.Analytics;
using Frenetik.MailerSend.Services.Analytics;
using Frenetik.MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace Frenetik.MailerSend.Tests.Services;

public class AnalyticsServiceTests
{
    private AnalyticsService CreateService(HttpMessageHandler handler)
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

        return new AnalyticsService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetByDateAsync_WithEvents_ReturnsAnalytics()
    {
        var dateFrom = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTo = new DateTime(2024, 1, 31, 0, 0, 0, DateTimeKind.Utc);
        var events = new[] { "sent", "delivered" };

        var expectedResponse = new AnalyticsByDateList
        {
            Data = new[]
            {
                new AnalyticsByDate
                {
                    StatDate = new DateTime(2024, 1, 1),
                    Sent = 100,
                    Delivered = 95
                }
            }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetByDateAsync(dateFrom, dateTo, events);

        Assert.NotNull(result);
        Assert.Single(result.Data);
        Assert.Equal(100, result.Data[0].Sent);
    }

    [Fact]
    public async Task GetByDateAsync_WithNullEvents_ThrowsMailerSendException()
    {
        var dateFrom = new DateTime(2024, 1, 1);
        var dateTo = new DateTime(2024, 1, 31);

        var handler = MockHttpMessageHandler.Create(new object());
        var service = CreateService(handler);

        await Assert.ThrowsAsync<MailerSendException>(() =>
            service.GetByDateAsync(dateFrom, dateTo, null!));
    }

    [Fact]
    public async Task GetByDateAsync_WithEmptyEvents_ThrowsMailerSendException()
    {
        var dateFrom = new DateTime(2024, 1, 1);
        var dateTo = new DateTime(2024, 1, 31);

        var handler = MockHttpMessageHandler.Create(new object());
        var service = CreateService(handler);

        await Assert.ThrowsAsync<MailerSendException>(() =>
            service.GetByDateAsync(dateFrom, dateTo, Array.Empty<string>()));
    }

    [Fact]
    public async Task GetByDateAsync_WithDomainIdAndTags_IncludesAllParameters()
    {
        var dateFrom = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTo = new DateTime(2024, 1, 31, 0, 0, 0, DateTimeKind.Utc);
        var events = new[] { "sent" };
        var tags = new[] { "tag1", "tag2" };

        var expectedResponse = new AnalyticsByDateList { Data = Array.Empty<AnalyticsByDate>() };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetByDateAsync(dateFrom, dateTo, events, domainId: "domain1", tags: tags);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetOpensByCountryAsync_ReturnsAnalyticsList()
    {
        var expectedResponse = new AnalyticsList
        {
            Statistics = new[]
            {
                new AnalyticsStatistic { Name = "US", Count = 150 }
            }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetOpensByCountryAsync();

        Assert.NotNull(result);
        Assert.Single(result.Statistics);
        Assert.Equal("US", result.Statistics[0].Name);
    }

    [Fact]
    public async Task GetOpensByUserAgentAsync_ReturnsAnalyticsList()
    {
        var expectedResponse = new AnalyticsList
        {
            Statistics = new[]
            {
                new AnalyticsStatistic { Name = "Chrome", Count = 200 }
            }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetOpensByUserAgentAsync();

        Assert.NotNull(result);
        Assert.Single(result.Statistics);
        Assert.Equal("Chrome", result.Statistics[0].Name);
    }

    [Fact]
    public async Task GetOpensByUserAgentTypeAsync_ReturnsAnalyticsList()
    {
        var expectedResponse = new AnalyticsList
        {
            Statistics = new[]
            {
                new AnalyticsStatistic { Name = "Desktop", Count = 300 }
            }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetOpensByUserAgentTypeAsync();

        Assert.NotNull(result);
        Assert.Single(result.Statistics);
    }

    [Fact]
    public void GetByDate_SyncMethod_Works()
    {
        var dateFrom = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTo = new DateTime(2024, 1, 31, 0, 0, 0, DateTimeKind.Utc);
        var events = new[] { "sent" };

        var expectedResponse = new AnalyticsByDateList
        {
            Data = new[] { new AnalyticsByDate { StatDate = new DateTime(2024, 1, 1), Sent = 50 } }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetByDate(dateFrom, dateTo, events);

        Assert.NotNull(result);
        Assert.Single(result.Data);
    }
}
