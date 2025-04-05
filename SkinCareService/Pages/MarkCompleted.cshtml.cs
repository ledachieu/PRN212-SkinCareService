using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkinCareService.Pages
{
    public class MarkCompletedModel : PageModel
    {
        private readonly BookingDAO _bookingDAO;

        public MarkCompletedModel(BookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }

        public IActionResult OnGet(int id)
        {
            _bookingDAO.MarkAsCompleted(id);
            return RedirectToPage("CustomerBooking");
        }
    }
}
