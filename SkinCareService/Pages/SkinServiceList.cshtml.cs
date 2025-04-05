using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class SkinServiceListModel : PageModel
    {
        private readonly ServiceDAO _skinServiceDAO;

        public SkinServiceListModel(ServiceDAO skinServiceDAO)
        {
            _skinServiceDAO = skinServiceDAO;
        }

        public List<SkinService> Services { get; set; }

        public void OnGet()
        {
            Services = _skinServiceDAO.GetAllServices();
        }
    }
}
