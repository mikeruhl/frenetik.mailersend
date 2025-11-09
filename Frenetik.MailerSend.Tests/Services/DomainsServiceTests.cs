using Frenetik.MailerSend.Configuration;
using Frenetik.MailerSend.Models;
using Frenetik.MailerSend.Models.Domains;
using Frenetik.MailerSend.Models.Util;
using Frenetik.MailerSend.Services.Domains;
using Frenetik.MailerSend.Tests.Helpers;
using Microsoft.Extensions.Options;
using Moq;

namespace Frenetik.MailerSend.Tests.Services;

public class DomainsServiceTests
{
    private DomainsService CreateService(HttpMessageHandler handler)
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

        return new DomainsService(mockFactory.Object, Options.Create(options));
    }

    [Fact]
    public async Task GetDomainsAsync_ReturnsDomainsList()
    {
        var expectedResponse = new DomainsList
        {
            Domains = new[]
            {
                new Domain
                {
                    Id = "domain1",
                    Name = "example.com",
                    IsVerified = true,
                    Dkim = true,
                    Spf = true
                }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetDomainsAsync();

        Assert.NotNull(result);
        Assert.Single(result.Domains);
        Assert.Equal("domain1", result.Domains[0].Id);
        Assert.Equal("example.com", result.Domains[0].Name);
    }

    [Fact]
    public async Task GetDomainsAsync_WithVerifiedFilter_IncludesParameter()
    {
        var expectedResponse = new DomainsList
        {
            Domains = Array.Empty<Domain>(),
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetDomainsAsync(verified: true);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetDomainsAsync_WithPagination_UsesCorrectEndpoint()
    {
        var expectedResponse = new DomainsList
        {
            Domains = Array.Empty<Domain>(),
            Meta = new ResponseMeta { CurrentPage = 2, LastPage = 5, Limit = 50 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetDomainsAsync(pagination: new MailerSend.Models.Util.PaginationParameters { Page = 2, Limit = 50 });

        Assert.Equal(2, result.CurrentPage);
        Assert.Equal(50, result.PerPage);
    }

    [Fact]
    public async Task GetDomainAsync_ReturnsSingleDomain()
    {
        var expectedDomain = new Domain
        {
            Id = "domain1",
            Name = "example.com",
            IsVerified = true
        };

        var response = new SingleDomainResponse { Domain = expectedDomain };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.GetDomainAsync("domain1");

        Assert.NotNull(result);
        Assert.Equal("domain1", result.Id);
        Assert.Equal("example.com", result.Name);
    }

    [Fact]
    public async Task AddDomainAsync_WithNameOnly_ReturnsDomain()
    {
        var expectedDomain = new Domain
        {
            Id = "domain1",
            Name = "newdomain.com"
        };

        var response = new SingleDomainResponse { Domain = expectedDomain };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.AddDomainAsync("newdomain.com");

        Assert.NotNull(result);
        Assert.Equal("newdomain.com", result.Name);
    }

    [Fact]
    public async Task AddDomainAsync_WithAllParameters_ReturnsDomain()
    {
        var expectedDomain = new Domain
        {
            Id = "domain1",
            Name = "newdomain.com"
        };

        var response = new SingleDomainResponse { Domain = expectedDomain };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.AddDomainAsync(
            "newdomain.com",
            returnPathSubdomain: "bounces",
            customTrackingSubdomain: "track",
            inboundRoutingSubdomain: "inbound");

        Assert.NotNull(result);
        Assert.Equal("newdomain.com", result.Name);
    }

    [Fact]
    public async Task UpdateDomainSettingsAsync_ReturnsUpdatedDomain()
    {
        var settings = new DomainSettings
        {
            SendPaused = true,
            TrackClicks = true,
            TrackOpens = true
        };

        var expectedDomain = new Domain
        {
            Id = "domain1",
            Name = "example.com",
            DomainSettings = settings
        };

        var response = new SingleDomainResponse { Domain = expectedDomain };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.UpdateDomainSettingsAsync("domain1", settings);

        Assert.NotNull(result);
        Assert.Equal("domain1", result.Id);
        Assert.NotNull(result.DomainSettings);
    }

    [Fact]
    public async Task DeleteDomainAsync_ReturnsTrue_WhenSuccessful()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 200 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteDomainAsync("domain1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteDomainAsync_ReturnsTrue_ForStatus204()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 204 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteDomainAsync("domain1");

        Assert.True(result);
    }

    [Fact]
    public async Task DeleteDomainAsync_ReturnsFalse_WhenFailed()
    {
        var response = new MailerSendResponse { ResponseStatusCode = 404 };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.DeleteDomainAsync("domain1");

        Assert.False(result);
    }

    [Fact]
    public async Task GetDomainRecipientsAsync_ReturnsRecipientsList()
    {
        var expectedResponse = new ApiRecipientsList
        {
            Recipients = new[]
            {
                new ApiRecipient
                {
                    Id = "recipient1",
                    Email = "user@example.com",
                    CreatedAt = DateTime.UtcNow
                }
            },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = await service.GetDomainRecipientsAsync("domain1");

        Assert.NotNull(result);
        Assert.Single(result.Recipients);
        Assert.Equal("recipient1", result.Recipients[0].Id);
    }

    [Fact]
    public async Task GetDomainDnsRecordsAsync_ReturnsDnsRecords()
    {
        var expectedRecords = new DomainDnsRecords
        {
            Spf = new DomainDnsAttribute { Type = "TXT", Hostname = "example.com", Value = "v=spf1..." },
            Dkim = new DomainDnsAttribute { Type = "TXT", Hostname = "_domainkey.example.com", Value = "k=rsa..." }
        };

        var response = new DomainDnsRecordsResponse { Records = expectedRecords };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = await service.GetDomainDnsRecordsAsync("domain1");

        Assert.NotNull(result);
        Assert.NotNull(result.Spf);
        Assert.NotNull(result.Dkim);
    }

    [Fact]
    public async Task VerifyDomainAsync_ReturnsVerificationStatus()
    {
        var expectedStatus = new DomainVerificationStatus
        {
            Message = "Domain verified successfully",
            ResponseStatusCode = 200
        };

        var handler = MockHttpMessageHandler.Create(expectedStatus);
        var service = CreateService(handler);

        var result = await service.VerifyDomainAsync("domain1");

        Assert.NotNull(result);
        Assert.Equal("Domain verified successfully", result.Message);
    }

    [Fact]
    public void GetDomains_SyncMethod_Works()
    {
        var expectedResponse = new DomainsList
        {
            Domains = new[] { new Domain { Id = "domain1", Name = "example.com" } },
            Meta = new ResponseMeta { CurrentPage = 1, LastPage = 1, Limit = 25 }
        };

        var handler = MockHttpMessageHandler.Create(expectedResponse);
        var service = CreateService(handler);

        var result = service.GetDomains();

        Assert.NotNull(result);
        Assert.Single(result.Domains);
    }

    [Fact]
    public void GetDomain_SyncMethod_Works()
    {
        var expectedDomain = new Domain { Id = "domain1", Name = "example.com" };
        var response = new SingleDomainResponse { Domain = expectedDomain };

        var handler = MockHttpMessageHandler.Create(response);
        var service = CreateService(handler);

        var result = service.GetDomain("domain1");

        Assert.NotNull(result);
        Assert.Equal("domain1", result.Id);
    }
}
