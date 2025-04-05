using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class SkinServiceCreateModel : PageModel
    {
        private readonly ServiceDAO _skinServiceDAO;

        public SkinServiceCreateModel(ServiceDAO skinServiceDAO)
        {
            _skinServiceDAO = skinServiceDAO;
        }

        [BindProperty]
        public SkinService NewService { get; set; }

        public bool CreateError { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                bool success = _skinServiceDAO.CreateService(NewService);
                if (success)
                {
                    return RedirectToPage("/SkinServiceList");
                }
                else
                {
                    CreateError = true;
                    return Page();
                }
            }
            return Page();
        }
    }
}
