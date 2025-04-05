using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class SkinServiceUpdateModel : PageModel
    {
        private readonly ServiceDAO _skinServiceDAO;

        public SkinServiceUpdateModel(ServiceDAO skinServiceDAO)
        {
            _skinServiceDAO = skinServiceDAO;
        }

        [BindProperty]
        public SkinService Service { get; set; }

        public IActionResult OnGet(int serviceId)
        {
            Service = _skinServiceDAO.GetServiceById(serviceId);
            if (Service == null)
            {
                return RedirectToPage("/SkinServiceList");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                bool success = _skinServiceDAO.UpdateService(Service.ServiceId, Service);
                if (success)
                {
                    return RedirectToPage("/SkinServiceList");
                }
            }
            return Page();
        }
    }
}
