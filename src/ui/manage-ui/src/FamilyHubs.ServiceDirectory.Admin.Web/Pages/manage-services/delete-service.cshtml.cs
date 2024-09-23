using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

public class DeleteService : PageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    private readonly IReferralService _referralServiceClient;

    // TODO: Note while building: 1 has 0 requests, 664 has 1 request
    public long ServiceId { get; set; }
    public string? ServiceName { get; set; }

    public DeleteService(IServiceDirectoryClient serviceDirectoryClient, IReferralService referralServiceClient)
    {
        _serviceDirectoryClient = serviceDirectoryClient;
        _referralServiceClient = referralServiceClient;
    }

    private async Task<bool> IsOpenConnectionRequests() =>
        await _referralServiceClient.GetReferralsCountByServiceId(ServiceId) > 0;

    public async Task<IActionResult> OnPostAsync()
    {
        // TODO: Redirect to Error Page if True
        // TODO: Delete the Service if False

        if (await IsOpenConnectionRequests())
        {
            return RedirectToPage("Welcome"); // TODO: Open Connection Requests Error Page
        }

        throw new NotImplementedException();
    }

    public async Task<IActionResult> OnGetAsync(long serviceId)
    {
        ServiceId = serviceId;

        if (await IsOpenConnectionRequests())
        {
            return RedirectToPage("Welcome"); // TODO: Open Connection Requests Error Page
        }

        ServiceName = (await _serviceDirectoryClient.GetServiceById(serviceId)).Name;

        return Page();
    }
}