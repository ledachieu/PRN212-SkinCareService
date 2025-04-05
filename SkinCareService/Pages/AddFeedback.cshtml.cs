using DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkinCareService.Models;

namespace SkinCareService.Pages
{
    public class AddFeedbackModel : PageModel
    {
        private readonly BookingDAO _bookingDAO;
        private readonly FeedbackDAO _feedbackDAO;

        public AddFeedbackModel(BookingDAO bookingDAO, FeedbackDAO feedbackDAO)
        {
            _bookingDAO = bookingDAO;
            _feedbackDAO = feedbackDAO;
        }

        [BindProperty]
        public int BookingId { get; set; }

        [BindProperty]
        public int Rating { get; set; }

        [BindProperty]
        public string Comments { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public IActionResult OnGet(int bookingId)
        {
            var booking = _bookingDAO.GetById(bookingId);
            if (booking == null || booking.Status != "Completed")
            {
                return RedirectToPage("/MyBooking");
            }

            BookingId = bookingId;
            return Page();
        }

        public IActionResult OnPost()
        {
            var booking = _bookingDAO.GetById(BookingId);
            if (booking == null || booking.Status != "Completed")
            {
                ErrorMessage = "Invalid booking.";
                return Page();
            }

            var feedback = new Feedback
            {
                BookingId = BookingId,
                Rating = Rating,
                Comments = Comments,
                CreatedAt = DateTime.Now
            };

            _feedbackDAO.Add(feedback);
            SuccessMessage = "Feedback submitted successfully!";
            return Page();
        }
    }
}
