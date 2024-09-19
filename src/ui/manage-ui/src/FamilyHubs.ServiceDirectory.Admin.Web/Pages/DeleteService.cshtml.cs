using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages;

// TODO: Move into a proper place
public class DeleteService : PageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    private readonly IReferralService _referralServiceClient;

    // TODO: Note while building: 1 has 0 requests, 664 has 1 request
    public long ServiceId { get; set; } = 1; // TODO: This will probably be passed in from the Service Detail page bit
    public string? ServiceName { get; set; } // TODO: Possibly also this ^

    public DeleteService(IServiceDirectoryClient serviceDirectoryClient, IReferralService referralServiceClient)
    {
        _serviceDirectoryClient = serviceDirectoryClient;
        _referralServiceClient = referralServiceClient;
    }

    private async Task<bool> IsOpenConnectionRequests() =>
        await _referralServiceClient.GetReferralsCountByServiceId(ServiceId) > 0;

    public void OnPost()
    {
        // TODO: Check for Open Connection Requests
        // TODO: Redirect to Error Page if True
        // TODO: Delete the Service if False

        throw new NotImplementedException();
    }

    public async Task<IActionResult> OnGetAsync()
    {
        // TODO: Check for Open Connection Requests [ DONE ]
        // TODO: Redirect to Error Page if True
        // TODO: Continue loading Delete Service Page if False
        // TODO: Test ID's in the .cshtml

        if (await IsOpenConnectionRequests()) return RedirectToPage("Welcome"); // TODO: Error Page

        ServiceName = (await _serviceDirectoryClient.GetServiceById(ServiceId)).Name;

        return Page();
    }
}