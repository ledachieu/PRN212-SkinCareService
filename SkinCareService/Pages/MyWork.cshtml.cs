using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class MyWorkModel : PageModel
    {
        private readonly BookingDAO _bookingDAO;
        private readonly TherapistDAO _therapistDAO;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MyWorkModel(BookingDAO bookingDAO,TherapistDAO therapistDAO, IHttpContextAccessor httpContextAccessor)
        {
            _bookingDAO = bookingDAO;
            _therapistDAO = therapistDAO;
            _httpContextAccessor = httpContextAccessor; 
        }

        public List<Booking> Bookings { get; set; } = new();

        public void OnGet()
        {
            var roleId = _httpContextAccessor.HttpContext.Session.GetInt32("RoleId");
            var userId = HttpContext.Session.GetInt32("UserId");
            if (roleId != 3 || userId == null)
            {
                 RedirectToPage("/Login");
            }

            var therapist = _therapistDAO.GetTherapistByUserId((int)userId);

            if (therapist != null)
            {
                Bookings = _bookingDAO.GetBookingsByTherapistId(therapist.TherapistId);
            }
        }
    }
}
