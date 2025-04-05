using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkinCareService.Pages
{
    public class CancelBookingModel : PageModel
    {
        private readonly BookingDAO _bookingDAO;

        public CancelBookingModel(BookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }

        public IActionResult OnGet(int id)
        {
            _bookingDAO.CancelBooking(id);
            return RedirectToPage("CustomerBooking");
        }
    }
}
