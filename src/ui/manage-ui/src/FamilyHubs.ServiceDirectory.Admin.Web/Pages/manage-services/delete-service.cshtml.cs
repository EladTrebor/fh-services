using FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FamilyHubs.ServiceDirectory.Admin.Web.Pages.manage_services;

public class DeleteService : PageModel
{
    private readonly IServiceDirectoryClient _serviceDirectoryClient;
    private readonly IReferralService _referralServiceClient;

    [BindProperty] public long ServiceId { get; set; }
    public string ServiceName { get; set; } = null!;

    [BindProperty] public bool Yes { get; set; }
    [BindProperty] public bool No { get; set; }
    public bool Error { get; set; }

    public DeleteService(IServiceDirectoryClient serviceDirectoryClient, IReferralService referralServiceClient)
    {
        _serviceDirectoryClient = serviceDirectoryClient;
        _referralServiceClient = referralServiceClient;
    }

    private async Task<bool> IsOpenConnectionRequests() =>
        await _referralServiceClient.GetReferralsCountByServiceId(ServiceId) > 0;

    private bool NoOptionSelected() => !Yes && !No;

    public async Task<IActionResult> OnPostAsync()
    {
        // TODO: Redirect to Error Page if True
        // TODO: Delete the Service if False

        if (NoOptionSelected())
        {
            Error = true;
            return await OnGetAsync(ServiceId);
        }

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

        ServiceName = (await _serviceDirectoryClient.GetServiceById(ServiceId)).Name;

        return Page();
    }
}