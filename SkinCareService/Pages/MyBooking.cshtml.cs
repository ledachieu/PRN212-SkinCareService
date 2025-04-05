using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class MyBookingModel : PageModel
    {
        public List<BookingViewModel> Bookings { get; set; } = new();
        public List<ServiceResult> ServiceResults { get; set; } = new();
        public int? SelectedBookingId { get; set; }

        public void OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login if not logged in
                Response.Redirect("/Login");
                return;
            }

            var dao = new BookingDAO();
            Bookings = dao.GetBookingsByCustomerId(userId.Value);
        }
        public void OnGetResults(int bookingId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login if not logged in
                Response.Redirect("/Login");
                return;
            }

            var dao = new BookingDAO();
            Bookings = dao.GetBookingsByCustomerId(userId.Value);
            SelectedBookingId = bookingId;
            ServiceResults = new ServiceResultDAO().GetResultsByBookingId(bookingId) ?? new List<ServiceResult>();

        }
    }
}
