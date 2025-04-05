using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class ResultListModel : PageModel
    {
        private readonly ServiceResultDAO _serviceResultDAO;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResultListModel(ServiceResultDAO serviceResultDAO, IHttpContextAccessor httpContextAccessor)
        {
            _serviceResultDAO = serviceResultDAO;
            _httpContextAccessor = httpContextAccessor;
        }

        public int BookingId { get; set; }
        public List<ServiceResult> Results { get; set; } = new();

        public void OnGet(int bookingId)
        {
            var roleId = _httpContextAccessor.HttpContext.Session.GetInt32("RoleId");
            if (roleId != 3)
            {
                RedirectToPage("/Login");
            }
            BookingId = bookingId;
            Results = _serviceResultDAO.GetResultsByBookingId(bookingId);
        }
    }
}
