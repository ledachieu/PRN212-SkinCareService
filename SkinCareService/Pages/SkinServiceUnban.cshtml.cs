using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkinCareService.Pages
{
    public class SkinServiceUnbanModel : PageModel
    {
        private readonly ServiceDAO _skinServiceDAO;

        public SkinServiceUnbanModel(ServiceDAO skinServiceDAO)
        {
            _skinServiceDAO = skinServiceDAO;
        }

        public IActionResult OnGet(int serviceId)
        {
            bool success = _skinServiceDAO.UnbanService(serviceId);
            
            return RedirectToPage("/SkinServiceList");
            
        }
    }
}
