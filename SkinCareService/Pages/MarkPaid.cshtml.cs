using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkinCareService.Pages
{
    public class MarkPaidModel : PageModel
    {
        private readonly BookingDAO _bookingDAO;

        public MarkPaidModel(BookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }

        public IActionResult OnGet(int id)
        {
            _bookingDAO.MarkAsPaid(id);
            return RedirectToPage("CustomerBooking");
        }
    }
}
