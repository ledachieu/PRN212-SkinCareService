using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class SkinCareDetailModel : PageModel
    {
        private readonly ServiceDAO _dao;

        public SkinCareDetailModel(ServiceDAO dao)
        {
            _dao = dao;
        }

        public SkinService Service { get; set; }
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Service = _dao.GetServiceById(id);

            if (Service == null)
            {
                return NotFound();
            }

            // Lấy tất cả feedback của các booking thuộc dịch vụ này
            Feedbacks = _dao.GetFeedbacks(Service);

            return Page();
        }
    }
}
