using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class CustomerBookingModel : PageModel
    {
        private readonly BookingDAO _bookingDAO;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerBookingModel(BookingDAO bookingDAO, IHttpContextAccessor httpContextAccessor)
        {
            _bookingDAO = bookingDAO;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Booking> Bookings { get; set; }

        public IActionResult OnGet()
        {
            var roleId = _httpContextAccessor.HttpContext.Session.GetInt32("RoleId");
            if (roleId != 4) // Giả sử Staff có RoleId = 4
                return RedirectToPage("/Login");

            Bookings = _bookingDAO.GetAllBookingsForStaff();
            return Page();
        }
    }
}
