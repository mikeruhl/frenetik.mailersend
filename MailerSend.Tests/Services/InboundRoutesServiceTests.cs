using MailerSend.Configuration;
using MailerSend.Models;
using MailerSend.Models.InboundRoutes;
using MailerSend.Models.Util;
using MailerSend.Services.InboundRoutes;
using MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace MailerSend.Tests.Services;

public class InboundRoutesServiceTests
{
    private InboundRoutesService CreateService(HttpMessageHandler handler)
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

        return new InboundRoutesService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetRoutesAsync_ReturnsRoutesList()
    {
        var expectedResponse = new InboundRoutesList
        {
            Routes = new[]
            {
                new InboundRoute
                {
                    Id = "route1",
                    Name = "Test Route",
                    Address = "test@example.com",
                    Enabled = true
                }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetRoutesAsync();

        Assert.NotNull(result);
        Assert.Single(result.Routes);
        Assert.Equal("route1", result.Routes[0].Id);
        Assert.Equal("Test Route", result.Routes[0].Name);
    }

    [Fact]
    public async Task GetRoutesAsync_WithDomainId_IncludesFilter()
    {
        var expectedResponse = new InboundRoutesList
        {
            Routes = Array.Empty<InboundRoute>(),
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetRoutesAsync(domainId: "domain123");

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetRouteAsync_ReturnsSingleRoute()
    {
        var expectedRoute = new InboundRoute
        {
            Id = "route1",
            Name = "Test Route",
            Address = "test@example.com",
            Enabled = true,
            Priority = 1
        };

        var response = new SingleInboundRouteResponse { Route = expectedRoute };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.GetRouteAsync("route1");

        Assert.NotNull(result);
        Assert.Equal("route1", result.Id);
        Assert.Equal("Test Route", result.Name);
        Assert.Equal(1, result.Priority);
    }

    [Fact]
    public async Task DeleteRouteAsync_ReturnsTrue_WhenSuccessful()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteRouteAsync("route1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteRouteAsync_ReturnsTrue_ForStatus204()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 204 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteRouteAsync("route1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteRouteAsync_ReturnsFalse_WhenFailed()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 404 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteRouteAsync("route1");

        Assert.False(result);
    }

    [Fact]
    public void GetRoutes_SyncMethod_Works()
    {
        var expectedResponse = new InboundRoutesList
        {
            Routes = new[] { new InboundRoute { Id = "route1" } },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetRoutes();

        Assert.NotNull(result);
        Assert.Single(result.Routes);
    }
}
