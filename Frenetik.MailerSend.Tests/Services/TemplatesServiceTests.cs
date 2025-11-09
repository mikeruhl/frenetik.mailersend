using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models;
using Frenetik.MailerSend.Models.Templates;
using Frenetik.MailerSend.Models.Util;
using Frenetik.MailerSend.Services.Templates;
using Frenetik.MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace Frenetik.MailerSend.Tests.Services;

public class TemplatesServiceTests
{
    private TemplatesService CreateService(HttpMessageHandler handler)
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

        return new TemplatesService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetTemplatesAsync_ReturnsTemplatesList()
    {
        var expectedResponse = new TemplatesList
        {
            Templates = new[]
            {
                new Template
                {
                    Id = "tmpl1",
                    Name = "Welcome Email",
                    Type = "email"
                }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetTemplatesAsync();

        Assert.NotNull(result);
        Assert.Single(result.Templates);
        Assert.Equal("tmpl1", result.Templates[0].Id);
        Assert.Equal("Welcome Email", result.Templates[0].Name);
    }

    [Fact]
    public async Task GetTemplatesAsync_WithDomainId_IncludesParameter()
    {
        var expectedResponse = new TemplatesList
        {
            Templates = Array.Empty<Template>(),
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetTemplatesAsync(domainId: "domain1");

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetTemplatesAsync_WithPagination_UsesCorrectEndpoint()
    {
        var expectedResponse = new TemplatesList
        {
            Templates = Array.Empty<Template>(),
            Meta = new ResponseMeta { CurrentPage = 2, LastPage = 5, Limit = 50 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetTemplatesAsync(pagination: new MailerSend.Models.Util.PaginationParameters { Page = 2, Limit = 50 });

        Assert.Equal(2, result.CurrentPage);
        Assert.Equal(50, result.PerPage);
    }

    [Fact]
    public async Task GetTemplateAsync_ReturnsSingleTemplate()
    {
        var expectedTemplate = new Template
        {
            Id = "tmpl1",
            Name = "Welcome Email",
            Type = "email"
        };

        var response = new SingleTemplateResponse { Template = expectedTemplate };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.GetTemplateAsync("tmpl1");

        Assert.NotNull(result);
        Assert.Equal("tmpl1", result.Id);
        Assert.Equal("Welcome Email", result.Name);
    }

    [Fact]
    public async Task DeleteTemplateAsync_ReturnsTrue_WhenSuccessful()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteTemplateAsync("tmpl1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteTemplateAsync_ReturnsTrue_ForStatus204()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 204 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteTemplateAsync("tmpl1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteTemplateAsync_ReturnsFalse_WhenFailed()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 404 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteTemplateAsync("tmpl1");

        Assert.False(result);
    }

    [Fact]
    public void GetTemplates_SyncMethod_Works()
    {
        var expectedResponse = new TemplatesList
        {
            Templates = new[] { new Template { Id = "tmpl1", Name = "Test Template" } },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetTemplates();

        Assert.NotNull(result);
        Assert.Single(result.Templates);
    }

    [Fact]
    public void GetTemplate_SyncMethod_Works()
    {
        var expectedTemplate = new Template { Id = "tmpl1", Name = "Test Template" };
        var response = new SingleTemplateResponse { Template = expectedTemplate };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = service.GetTemplate("tmpl1");

        Assert.NotNull(result);
        Assert.Equal("tmpl1", result.Id);
    }
}
