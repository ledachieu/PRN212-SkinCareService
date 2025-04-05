using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class SkinCareListModel : PageModel
    {
        private readonly ServiceDAO _serviceDAO;

        public SkinCareListModel(ServiceDAO serviceDAO)
        {
            _serviceDAO = serviceDAO;
        }

        public List<SkinService> Services { get; set; } = new();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int page = 1)
        {
            int pageSize = 6; // Mỗi trang hiển thị 6 dịch vụ
            (Services, int totalCount) = await _serviceDAO.GetServicesAsync(page, pageSize);

            CurrentPage = page;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        }
    }
}
