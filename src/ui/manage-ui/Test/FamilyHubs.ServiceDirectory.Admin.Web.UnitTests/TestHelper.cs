using AutoFixture;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests
{
    internal static class TestHelper
    {
        public static Mock<HttpContext> GetHttpContextMoq(List<Claim> claims)
        {
            var mockUser = new Mock<ClaimsPrincipal>();
            mockUser.SetupGet(x=>x.Claims).Returns(claims);

            var mockCookies = new Mock<IResponseCookies>();

            var mockResponse = new Mock<HttpResponse>();
            mockResponse.SetupGet(x=>x.Cookies).Returns(mockCookies.Object);

            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User).Returns(mockUser.Object);
            mock.SetupGet(x=>x.Response).Returns(mockResponse.Object);

            return mock;
        }
        
        // TODO: replace fully with NSubstitute
        /// <summary>
        /// Uses NSubstitute to create a HttpContext with a user with the given claims
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static HttpContext GetHttpContext(List<Claim> claims)
        {
            var mockUser = Substitute.For<ClaimsPrincipal>();
            mockUser.Claims.Returns(claims);

            var mockCookies = new Mock<IResponseCookies>();

            var mockResponse = new Mock<HttpResponse>();
            mockResponse.SetupGet(x=>x.Cookies).Returns(mockCookies.Object);

            var mock = Substitute.For<HttpContext>();
            mock.User.Returns(mockUser);
            mock.Response.Returns(mockResponse.Object);

            return mock;
        }

        public static OrganisationDto CreateTestOrganisation(long id, long? parentId, OrganisationType organisationType, Fixture fixture)
        {
            var organisation = fixture.Create<OrganisationDto>();
            organisation.Id = id;
            organisation.AssociatedOrganisationId = parentId;
            organisation.OrganisationType = organisationType;
            return organisation;
        }

        public static OrganisationDetailsDto CreateTestOrganisationWithServices(long id, long? parentId, OrganisationType organisationType, Fixture fixture)
        {
            var organisation = fixture.Create<OrganisationDetailsDto>();
            organisation.Id = id;
            organisation.AssociatedOrganisationId = parentId;
            organisation.OrganisationType = organisationType;
            return organisation;
        }
        
        /// <summary>
        /// Uses NSubstitute to capture the argument passed to a method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class ArgumentCaptor<T>
        {
            public T Capture()
            {
                return Arg.Is<T>(t => SaveValue(t));
            }

            private bool SaveValue(T t)
            {
                Value = t;
                return true;
            }

            public T? Value { get; private set; }
        }
    }
}
