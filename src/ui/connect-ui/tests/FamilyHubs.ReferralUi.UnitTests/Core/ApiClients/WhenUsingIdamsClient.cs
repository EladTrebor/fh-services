using System.Net;
using FamilyHubs.Referral.Core.ApiClients;
using FluentAssertions;
using System.Text.Json;
using NSubstitute;

namespace FamilyHubs.ReferralUi.UnitTests.Core.ApiClients;

public class WhenUsingIdamsClient
{
    private readonly IHttpClientFactory _mockClientFactory;
    public WhenUsingIdamsClient()
    {
        _mockClientFactory = Substitute.For<IHttpClientFactory>();
    }
    
    [Fact]
    public async Task ThenGetAccountList()
    {
        //Arrange
        var expectedListAccounts = ClientHelper.GetAccountList();
        var jsonString = JsonSerializer.Serialize(expectedListAccounts);

        var httpClient = ClientHelper.GetMockClient(jsonString, HttpStatusCode.Accepted);

        _mockClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var idamClientService = new IdamsClient(_mockClientFactory);

        //Act
        var result = await idamClientService.GetVcsProfessionalsEmailsAsync(1);

        //Assert
        result.Should().BeEquivalentTo(expectedListAccounts.Select(x => x.Email));
    }

    [Fact]
    public async Task ThenThrowsIdamsClientException_OnBadRequest()
    {
        // Arrange
        var httpClient = ClientHelper.GetMockClient("", HttpStatusCode.BadRequest);

        _mockClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        var idamClientService = new IdamsClient(_mockClientFactory);

        // Act and Assert
        await Assert.ThrowsAsync<IdamsClientException>(() => idamClientService.GetVcsProfessionalsEmailsAsync(1, CancellationToken.None));
    }
}
