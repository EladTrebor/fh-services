using FamilyHubs.ServiceDirectory.Admin.Core.Services;
using FamilyHubs.ServiceDirectory.Admin.Web.Areas.AccountAdmin.Pages.ManagePermissions;
using NSubstitute;
using Xunit;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests.Areas.AccountAdmin.ManagePermissions
{
    public class DeleteUserConfirmationTests
    {
        
        private readonly ICacheService _mockCacheService;
        public DeleteUserConfirmationTests()
        {
            _mockCacheService = Substitute.For<ICacheService>();
        }
            
        [Fact]
        public async Task OnGet_UserNameRetrivedFromCache()
        {
            //  Arrange            
            const string userName = "TestUser";
            const bool isDeleted = true;
            _mockCacheService.RetrieveString(Arg.Any<string>()).Returns(Task.FromResult(userName));
            var sut = new DeleteUserConfirmationModel( _mockCacheService);

            //  Act
            await sut.OnGet(isDeleted);

            //  Assert
            await _mockCacheService.Received(1).RetrieveString("UserName");
            Assert.Equal(userName, sut.UserName);
        }               
    }
}
