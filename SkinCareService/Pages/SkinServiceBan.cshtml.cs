using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkinCareService.Pages
{
    public class SkinServiceBanModel : PageModel
    {
        private readonly ServiceDAO _skinServiceDAO;

        public SkinServiceBanModel(ServiceDAO skinServiceDAO)
        {
            _skinServiceDAO = skinServiceDAO;
        }

        public IActionResult OnGet(int serviceId)
        {
            bool success = _skinServiceDAO.BanService(serviceId);
            
            
                // If the service couldn't be banned, redirect back to the list with an error.
            return RedirectToPage("/SkinServiceList");
            
        }
    }
}
