using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;
using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.Report.Data.Entities;
using FamilyHubs.Report.Data.Repository;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ReportApi.UnitTests;

public class WhenUsingGetConnectionRequestsSentFactFourWeekBreakdownQuery
{
    private readonly IGetConnectionRequestsSentFactFourWeekBreakdownQuery
        _getConnectionRequestsSentFactFourWeekBreakdownQuery;

    public WhenUsingGetConnectionRequestsSentFactFourWeekBreakdownQuery()
    {
        List<DateDim> dateDimList = new()
        {
            new DateDim
            {
                DateKey = 1,
                Date = DateTime.Parse("2024-01-01") // Week 1
            },
            new DateDim
            {
                DateKey = 2,
                Date = DateTime.Parse("2024-01-08") // Week 2
            },
            new DateDim
            {
                DateKey = 3,
                Date = DateTime.Parse("2024-01-15") // Week 3
            },
            new DateDim
            {
                DateKey = 4,
                Date = DateTime.Parse("2024-01-22") // Week 4
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
                DateKey = 1,
                OrganisationKey = 1,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[0]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 1,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[0]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 2,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 1,
                OrganisationKey = 2,
                DateDim = dateDimList[0],
                OrganisationDim = organisationDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 2,
                DateDim = dateDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 2,
                DateDim = dateDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 2,
                DateDim = dateDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 3,
                OrganisationKey = 1,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[0]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 3,
                OrganisationKey = 2,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[1]
            },
            new ConnectionRequestsSentFact
            {
                DateKey = 4,
                DateDim = dateDimList[3]
            },
        };

        List<ConnectionRequestsFact> connectionRequestsFactList = new()
        {
            new ConnectionRequestsFact
            {
                DateKey = 4,
                OrganisationKey = 1,
                DateDim = dateDimList[3],
                OrganisationDim = organisationDimList[0],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 4,
                OrganisationKey = 1,
                DateDim = dateDimList[3],
                OrganisationDim = organisationDimList[0],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 4,
                OrganisationKey = 2,
                DateDim = dateDimList[3],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 4,
                OrganisationKey = 2,
                DateDim = dateDimList[3],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 3,
                OrganisationKey = 2,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 3,
                OrganisationKey = 2,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 3,
                OrganisationKey = 2,
                DateDim = dateDimList[2],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 2,
                OrganisationKey = 1,
                DateDim = dateDimList[1],
                OrganisationDim = organisationDimList[0],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 2,
                OrganisationKey = 2,
                DateDim = dateDimList[1],
                OrganisationDim = organisationDimList[1],
                ConnectionRequestStatusTypeKey = (short)ReferralStatus.Accepted
            },
            new ConnectionRequestsFact
            {
                DateKey = 1,
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

        IGetConnectionRequestsSentFactQuery getConnectionRequestsSentFactQueryMock
            = Substitute.For<GetConnectionRequestsSentFactQuery>(reportDbContextMock);

        _getConnectionRequestsSentFactFourWeekBreakdownQuery =
            new GetConnectionRequestsSentFactFourWeekBreakdownQuery(getConnectionRequestsSentFactQueryMock);
    }

    [Fact]
    public async Task Then_GetFourWeekBreakdownForAdmin_Should_Return_Results_IfRequestsMade()
    {
        ConnectionRequestsBreakdown expected = new()
        {
            Totals = new ConnectionRequests
            {
                Made = 10,
                Accepted = 10,
            },
            WeeklyReports = new ConnectionRequestsDated[]
            {
                new()
                {
                    Date = "1 January to 7 January",
                    Made = 4,
                    Accepted = 1,
                },
                new()
                {
                    Date = "8 January to 14 January",
                    Made = 3,
                    Accepted = 2,
                },
                new()
                {
                    Date = "15 January to 21 January",
                    Made = 2,
                    Accepted = 3,
                },
                new()
                {
                    Date = "22 January to 28 January",
                    Made = 1,
                    Accepted = 4,
                },
            }
        };

        ConnectionRequestsBreakdownRequest request = new(DateTime.Parse("2024-01-31"));

        ConnectionRequestsBreakdown result =
            await _getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForAdmin(request);

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public async Task Then_GetFourWeekBreakdownForAdmin_Should_Return_Zero_If_NoRequestsMade()
    {
        ConnectionRequestsBreakdown expected = new()
        {
            Totals = new ConnectionRequests
            {
                Made = 0,
                Accepted = 0,
            },
            WeeklyReports = new ConnectionRequestsDated[]
            {
                new()
                {
                    Date = "13 November to 19 November",
                    Made = 0,
                    Accepted = 0,
                },
                new()
                {
                    Date = "20 November to 26 November",
                    Made = 0,
                    Accepted = 0,
                },
                new()
                {
                    Date = "27 November to 3 December",
                    Made = 0,
                    Accepted = 0,
                },
                new()
                {
                    Date = "4 December to 10 December",
                    Made = 0,
                    Accepted = 0,
                },
            }
        };

        ConnectionRequestsBreakdownRequest request = new(DateTime.Parse("2023-12-16"));

        ConnectionRequestsBreakdown result =
            await _getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForAdmin(request);

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public async Task Then_GetFourWeekBreakdownForLa_Should_Return_Results_IfRequestsMade()
    {
        ConnectionRequestsBreakdown expected = new()
        {
            Totals = new ConnectionRequests
            {
                Made = 3,
                Accepted = 3,
            },
            WeeklyReports = new ConnectionRequestsDated[]
            {
                new()
                {
                    Date = "1 January to 7 January",
                    Made = 2,
                    Accepted = 0,
                },
                new()
                {
                    Date = "8 January to 14 January",
                    Made = 0,
                    Accepted = 1,
                },
                new()
                {
                    Date = "15 January to 21 January",
                    Made = 1,
                    Accepted = 0,
                },
                new()
                {
                    Date = "22 January to 28 January",
                    Made = 0,
                    Accepted = 2,
                },
            }
        };

        LaConnectionRequestsBreakdownRequest request = new(DateTime.Parse("2024-01-31"), 10);

        ConnectionRequestsBreakdown result =
            await _getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForLa(request);

        Assert.Equivalent(expected, result);
    }

    [Fact]
    public async Task Then_GetFourWeekBreakdownForLa_Should_Return_Zero_If_NoRequestsMade()
    {
        ConnectionRequestsBreakdown expected = new()
        {
            Totals = new ConnectionRequests
            {
                Made = 0,
                Accepted = 0,
            },
            WeeklyReports = new ConnectionRequestsDated[]
            {
                new()
                {
                    Date = "13 November to 19 November",
                    Made = 0,
                    Accepted = 0,
                },
                new()
                {
                    Date = "20 November to 26 November",
                    Made = 0,
                    Accepted = 0,
                },
                new()
                {
                    Date = "27 November to 3 December",
                    Made = 0,
                    Accepted = 0,
                },
                new()
                {
                    Date = "4 December to 10 December",
                    Made = 0,
                    Accepted = 0,
                },
            }
        };

        LaConnectionRequestsBreakdownRequest request = new(DateTime.Parse("2023-12-16"), 10);

        ConnectionRequestsBreakdown result =
            await _getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForLa(request);

        Assert.Equivalent(expected, result);
    }
}
