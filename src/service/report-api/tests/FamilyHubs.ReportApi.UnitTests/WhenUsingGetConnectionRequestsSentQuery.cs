using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;
using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.Report.Data.Entities;
using FamilyHubs.Report.Data.Repository;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ReportApi.UnitTests;

public class WhenUsingGetConnectionRequestsSentQuery
{
    private readonly IGetConnectionRequestsSentFactQuery _getConnectionRequestsSentFactQuery;

    public WhenUsingGetConnectionRequestsSentQuery()
    {
        List<DateDim> dateDimList = new()
        {
            new DateDim
            {
                DateKey = 0,
                Date = DateTime.Parse("2024-08-08")
            },
            new DateDim
            {
                DateKey = 1,
                Date = DateTime.Parse("2024-08-04")
            },
            new DateDim
            {
                DateKey = 2,
                Date = DateTime.Parse("2024-06-08")
            },
            new DateDim
            {
                DateKey = 3,
                Date = DateTime.Parse("2024-06-04")
            }
        };

        List<OrganisationDim> organisationDimList = new()
        {
            new OrganisationDim
            {
                OrganisationKey = 1,
                OrganisationId = 10
            },
            new OrganisationDim
            {
                OrganisationKey = 2,
                OrganisationId = 20
            }
        };

        List<ConnectionRequestsSentFact> connectionRequestsSentFactList = new()
        {
            new ConnectionRequestsSentFact
            {
                DateKey = 0,
                OrganisationKey = 1,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[0]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 0,
                OrganisationKey = 1,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[0]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 0,
                OrganisationKey = 2,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 0,
                OrganisationKey = 2,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 1,
                DateDim = dateDimList[1],
                OrganisationDim = organisationDimList[0]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 2,
                DateDim = dateDimList[1],
                OrganisationDim = organisationDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 2,
                DateDim = dateDimList[2]
            }
        };

        List<ConnectionRequestsFact> connectionRequestsFactList = new()
        {
            new ConnectionRequestsFact
            {
                DateKey = 2,
                OrganisationKey = 1,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[0],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 2,
                OrganisationKey = 1,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[0],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 2,
                OrganisationKey = 2,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 2,
                OrganisationKey = 2,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 3,
                OrganisationKey = 1,
                DateDim = dateDimList[3],
                OrganisationDim = organisationDimList[0],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 3,
                OrganisationKey = 2,
                DateDim = dateDimList[3],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 0,
                OrganisationKey = 2,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            }
        };

        IReportDbContext reportDbContextMock = Substitute.For<IReportDbContext>();

        reportDbContextMock.ConnectionRequestsSentFacts.Returns(connectionRequestsSentFactList.AsQueryable());

        reportDbContextMock.CountAsync(Arg.Any<IQueryable<ConnectionRequestsSentFact>>(), Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var queryable = callInfo.ArgAt<IQueryable<ConnectionRequestsSentFact>>(0);
                return Task.FromResult(queryable.Count());
            });

        reportDbContextMock.ConnectionRequestsFacts.Returns(connectionRequestsFactList.AsQueryable());

        reportDbContextMock.CountAsync(Arg.Any<IQueryable<ConnectionRequestsFact>>(), Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                var queryable = callInfo.ArgAt<IQueryable<ConnectionRequestsFact>>(0);
                return Task.FromResult(queryable.Count());
            });

        _getConnectionRequestsSentFactQuery = new GetConnectionRequestsSentFactQuery(reportDbContextMock);
    }

    [Theory]
    [InlineData("2024-08-08", 1, 4, 1)]
    [InlineData("2024-08-04", 1, 2, 0)]
    [InlineData("2024-06-08", 1, 1, 4)]
    [InlineData("2024-06-04", 1, 0, 2)]
    [InlineData("2024-08-08", 7, 6, 1)]
    [InlineData("2024-08-04", 7, 2, 0)]
    [InlineData("2024-06-08", 7, 1, 6)]
    [InlineData("2024-06-04", 7, 0, 2)]
    [InlineData("2024-12-31", 365, 7, 7)]
    public async Task Then_GetConnectionRequestsForAdmin_Should_Return_ExpectedResult(string dateStr, int days, int requestsMade, int requestsAccepted)
    {
        ConnectionRequests expected = new()
        {
            Made = requestsMade,
            Accepted = requestsAccepted,
        };

        DateTime dateTime = DateTime.Parse(dateStr);

        ConnectionRequestsRequest request = new(dateTime, days);

        ConnectionRequests result = await _getConnectionRequestsSentFactQuery.GetConnectionRequestsForAdmin(request);

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public async Task Then_GetTotalConnectionRequestsForAdmin_Should_Return_ExpectedResult()
    {
        ConnectionRequests expected = new()
        {
            Made = 7,
            Accepted = 7,
        };

        ConnectionRequests result = await _getConnectionRequestsSentFactQuery.GetTotalConnectionRequestsForAdmin();

        Assert.Equivalent(expected, result);
    }

    [Theory]
    [InlineData("2024-08-08", 1, 2, 0)]
    [InlineData("2024-08-04", 1, 1, 0)]
    [InlineData("2024-06-08", 1, 0, 2)]
    [InlineData("2024-06-04", 1, 0, 1)]
    [InlineData("2024-08-08", 7, 3, 0)]
    [InlineData("2024-08-04", 7, 1, 0)]
    [InlineData("2024-06-08", 7, 0, 3)]
    [InlineData("2024-06-04", 7, 0, 1)]
    [InlineData("2024-12-31", 365, 3, 3)]
    public async Task Then_GetConnectionRequestsForLa_Should_Return_ExpectedResult(string dateStr, int days, int requestsMade, int requestsAccepted)
    {
        ConnectionRequests expected = new()
        {
            Made = requestsMade,
            Accepted = requestsAccepted,
        };

        DateTime dateTime = DateTime.Parse(dateStr);

        LaConnectionRequestsRequest request = new(10, dateTime, days);

        ConnectionRequests result = await _getConnectionRequestsSentFactQuery.GetConnectionRequestsForLa(request);

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public async Task Then_GetTotalConnectionRequestsForLa_Should_Return_ExpectedResult()
    {
        ConnectionRequests expected = new()
        {
            Made = 3,
            Accepted = 3
        };

        LaConnectionRequestsTotalRequest request = new(10);

        ConnectionRequests result = await _getConnectionRequestsSentFactQuery.GetTotalConnectionRequestsForLa(request);

        Assert.Equivalent(expected, result);
    }
}
