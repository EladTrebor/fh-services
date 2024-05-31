using FamilyHubs.ServiceDirectory.Admin.Core.DistributedCache;
using FamilyHubs.ServiceDirectory.Admin.Web.Pages.Shared;
using FamilyHubs.SharedKernel.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_locations;

[Authorize(Roles = RoleGroups.AdminRole)]
public class LocationConfirmationModel : HeaderPageModel
{
    private readonly IRequestDistributedCache _cache;

    public LocationConfirmationModel(IRequestDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task OnGetAsync()
    {
        var familyHubsUser = HttpContext.GetFamilyHubsUser();

        await _cache.RemoveAsync<LocationPageModel<object>>(familyHubsUser.Email);
    }
}