using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Exceptions;
using Frenetik.MailerSend.Models.Activities;
using Frenetik.MailerSend.Models.Util;
using Frenetik.MailerSend.Services.Activities;
using Frenetik.MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace Frenetik.MailerSend.Tests.Services;

public class ActivitiesServiceTests
{
    private ActivitiesService CreateService(HttpMessageHandler handler)
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

        return new ActivitiesService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetActivitiesAsync_WithDomainId_ReturnsActivitiesList()
    {
        var expectedResponse = new ActivitiesList
        {
            Activities = new[]
            {
                new Activity
                {
                    Id = "act1",
                    Type = "sent",
                    CreatedAt = DateTime.UtcNow
                }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetActivitiesAsync("domain1", default);

        Assert.NotNull(result);
        Assert.Single(result.Activities);
        Assert.Equal("act1", result.Activities[0].Id);
    }

    [Fact]
    public async Task GetActivitiesAsync_WithPagination_UsesCorrectEndpoint()
    {
        var expectedResponse = new ActivitiesList
        {
            Activities = Array.Empty<Activity>(),
            Meta = new ResponseMeta { CurrentPage = 2, LastPage = 5, Limit = 50 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetActivitiesAsync("domain1", pagination: new MailerSend.Models.Util.PaginationParameters { Page = 2, Limit = 50 });

        Assert.Equal(2, result.CurrentPage);
        Assert.Equal(50, result.PerPage);
    }

    [Fact]
    public async Task GetActivitiesAsync_WithDateRange_IncludesParameters()
    {
        var dateFrom = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var dateTo = new DateTime(2024, 1, 31, 23, 59, 59, DateTimeKind.Utc);

        var expectedResponse = new ActivitiesList
        {
            Activities = Array.Empty<Activity>(),
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetActivitiesAsync("domain1", dateFrom: dateFrom, dateTo: dateTo);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetActivitiesAsync_WithEvents_IncludesEventParameters()
    {
        var events = new[] { "sent", "delivered", "opened" };
        var expectedResponse = new ActivitiesList
        {
            Activities = Array.Empty<Activity>(),
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetActivitiesAsync("domain1", events: events);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetActivitiesAsync_WithNullDomainId_ThrowsArgumentException()
    {
        var handler = MockHttpMessageHandler.Create(new object());
        var service = CreateService(handler);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetActivitiesAsync(null!));
    }

    [Fact]
    public async Task GetActivitiesAsync_WithEmptyDomainId_ThrowsArgumentException()
    {
        var handler = MockHttpMessageHandler.Create(new object());
        var service = CreateService(handler);

        await Assert.ThrowsAsync<ArgumentException>(() => service.GetActivitiesAsync(string.Empty));
    }

    [Fact]
    public async Task GetActivitiesAsync_WithInvalidDateRange_ThrowsMailerSendException()
    {
        var dateFrom = new DateTime(2024, 1, 31, 0, 0, 0, DateTimeKind.Utc);
        var dateTo = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        var handler = MockHttpMessageHandler.Create(new object());
        var service = CreateService(handler);

        await Assert.ThrowsAsync<MailerSendException>(() =>
            service.GetActivitiesAsync("domain1", dateFrom: dateFrom, dateTo: dateTo));
    }

    [Fact]
    public void GetActivities_SyncMethod_Works()
    {
        var expectedResponse = new ActivitiesList
        {
            Activities = new[] { new Activity { Id = "act1", Type = "sent" } },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetActivities("domain1");

        Assert.NotNull(result);
        Assert.Single(result.Activities);
    }
}
